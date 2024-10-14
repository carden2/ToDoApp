using ToDoApp.Models;
using ToDoApp.Views;

namespace ToDoApp.Data;

public interface IDataAccess
{
    public void saveNewList(List<ToDoList> myName);

    public void saveToDoList(string listId, List<ToDoItem> myItem);
    
    public List<ToDoList> getNameLists();

    public List<ToDoItem> getToDoListById(string listId);
}