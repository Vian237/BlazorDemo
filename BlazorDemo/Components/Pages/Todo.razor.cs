using BlazorDemo.Model;

namespace BlazorDemo.Components.Pages
{
    public partial class Todo
    {
        private List<TodoItem> todoItems = new();
        private string newTodo="";

        private void AddTodo()
        {
            if (!string.IsNullOrWhiteSpace(newTodo))
            {
                todoItems.Add(new TodoItem { Title = newTodo });
                newTodo = string.Empty;
            }
        }
    }
}
