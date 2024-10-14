using System.Collections.ObjectModel;
using ToDoApp.Data;
using ToDoApp.Models;
using ToDoApp.Views;

namespace ToDoApp;

public partial class MainPage : ContentPage
{
    public ObservableCollection<ToDoList> MyNames { get;set;} = new ObservableCollection<ToDoList>();
    private DataAccess dataAccess = new DataAccess();
    
    private bool isSelected;

    public MainPage()
    {
        InitializeComponent();
        
        MyNames.Clear();
        var loadedList = dataAccess.getNameLists();
        foreach (var name in loadedList)
        {
            MyNames.Add(name);
        }
        
        listView.SelectedItem = null;
        
        BindingContext = this;
    }

    private async void AddInputItemTolist(object sender, EventArgs e)
    {
        var myName = new ToDoList();
        myName.listId = DateTime.Now.ToString("yyMMddHHmmssff");
        myName.title = entry.Text;
        MyNames.Add(myName);
        //save after adding new name
        dataAccess.saveNewList(MyNames.ToList());
        
        entry.Text = string.Empty;
        
        await Navigation.PushAsync(new ListPage(myName));
    }
    

    private async void ListView_OnItemSelected(object? sender, SelectedItemChangedEventArgs e)
    {
        isSelected = true;
    }
    
    private async void DeleteList_OnClicked(object? sender, EventArgs e)
    {
        Button btn = (Button)sender;
        var myName  = (ToDoList)btn.BindingContext;
        var itemToDelete = MyNames.ToList().Find(item => item.listId==myName.listId);
        MyNames.Remove(itemToDelete);
        
        //save after removing item
        dataAccess.saveNewList(MyNames.ToList());
        
        await DisplayAlert("Delete", "You have deleted " + myName.title, "ok");
    }

    private async void ListView_OnItemTapped(object? sender, ItemTappedEventArgs e)
    {
        if(!isSelected)
        {
            var selectedItem = (ToDoList)e.Item;
            await Navigation.PushAsync(new ListPage(selectedItem));
        }
        isSelected = false;
    }
}