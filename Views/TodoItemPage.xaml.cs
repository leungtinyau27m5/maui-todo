using TODO.Data;
using TODO.Models;

namespace TODO.Views;

public partial class TodoItemPage : ContentPage
{
    public TodoItemPage()
    {
        InitializeComponent();
    }

    async void OnSave(object sender, EventArgs e)
    {
        var todoItem = (TodoItem)BindingContext;
        var database = await TodoItemDatabase.Instance;
        await database.SaveItemAsync(todoItem);
        await Navigation.PopAsync();
    }

    async void OnDelete(object sender, EventArgs e)
    {
        var todoItem = (TodoItem)BindingContext;
        var database = await TodoItemDatabase.Instance;

        await database.DeleteItemAsync(todoItem);
        await Navigation.PopAsync();
    }

    async void OnCancel(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}