namespace ToDoApp.Models;

public class ToDoItem
{
    public string toDoId { get; set; }
    public string listId { get; set; }
    public string title { get; set; }
    public bool isComplete { get; set; }
}