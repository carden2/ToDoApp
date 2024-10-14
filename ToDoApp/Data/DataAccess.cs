using System.Text.Json;
using ToDoApp.Models;
using ToDoApp.Views;

namespace ToDoApp.Data;

public class DataAccess : IDataAccess
{
    private const string KeyAllNames = "AllMyNames";
    private const string KeyToDoList = "SingleToDoList";
    
    /*
     *  adds a single new to do record 
     */
    public void saveToDoList(string listId, List<ToDoItem> myItems)
    {
        //save toDos list
        string jsonStringToSave = JsonSerializer.Serialize(myItems);
        Preferences.Set(KeyToDoList + "-" + listId, jsonStringToSave);   
    }
    
    
    /*
     *  adds a single new record with a unique id and a title
     */
    public void saveNewList(List<ToDoList> myNames)
    {
        //save myNames list
        string jsonStringToSave = JsonSerializer.Serialize(myNames);
        Preferences.Set(KeyAllNames, jsonStringToSave);
    }

    /*
     * Returns all the to do lists that have been saved by the user
     *
     */
    public List<ToDoList> getNameLists()
    {
        List<ToDoList> myLists = new List<ToDoList>();
        string jsonStringOfLists = Preferences.Get(KeyAllNames, "");
        if (jsonStringOfLists != "")
        {
            myLists = JsonSerializer.Deserialize<List<ToDoList>>(jsonStringOfLists);
        }
        return myLists;
    }
    
    /*
     * Returns a specific to do list based on @id 
     */
    public List<ToDoItem> getToDoListById(string listId)
    {
        List<ToDoItem> myItems = new List<ToDoItem>();
        string items = Preferences.Get(KeyToDoList + "-" + listId, "");
        if (items != "")
        {
            myItems = JsonSerializer.Deserialize<List<ToDoItem>>(items);
        }  
        return myItems;
    }
}