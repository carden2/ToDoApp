using System.Collections.ObjectModel;
using ToDoApp.Data;
using ToDoApp.Models;

namespace ToDoApp.Views;

public partial class ListPage : ContentPage
{
    public ObservableCollection<ToDoItem> MyItems { get;set;} = new ObservableCollection<ToDoItem>();
    public ToDoList ListTitle { get;set;} = new ToDoList();
    private DataAccess dataAccess = new DataAccess();
    public ListPage(ToDoList myName)
    {
        InitializeComponent();
    
        if (myName != null)
        {
            ListTitle.listId = myName.listId;
            ListTitle.title = myName.title;
        }

        var loadedList = dataAccess.getToDoListById(ListTitle.listId);
        MyItems.Clear();
        foreach (var item in loadedList)
        {
            MyItems.Add(item);
        }
        
        BindingContext = this;
        
    }
    
    private void AddInputItemTolist(object sender, EventArgs e)
    {
        var myItem = new ToDoItem();
        myItem.listId = ListTitle.listId;
        myItem.title = itemEntry.Text;
        myItem.isComplete = false;
        myItem.toDoId = DateTime.Now.ToString("yyMMddHHmmssff");
        
        MyItems.Add(myItem);
        dataAccess.saveToDoList(myItem.listId, MyItems.ToList());
        
        itemEntry.Text = string.Empty;
    }
    
    private void CheckBox_OnCheckedChanged(object? sender, CheckedChangedEventArgs e)
    {
        CheckBox cb = (CheckBox)sender;
        var myItem = (ToDoItem)cb.BindingContext;
        var item = MyItems.ToList().FirstOrDefault(i => i.toDoId == myItem.toDoId);
        if (item != null)
        {
            item.isComplete = cb.IsChecked;
        }
        dataAccess.saveToDoList(myItem.listId, MyItems.ToList());
        
    }

    private async void DeleteList_OnClicked(object? sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var myItem  = (ToDoItem)btn.BindingContext;
        var title = myItem.title;
        //remove item
        MyItems.Remove(myItem);
        
        //save after removing item
        dataAccess.saveToDoList(myItem.listId, MyItems.ToList());
        
        //confirm deletion
        await DisplayAlert("Delete", "You have deleted " + title, "ok");
    }
}