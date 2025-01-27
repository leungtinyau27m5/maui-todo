using TODO.Data;
using TODO.Models;

namespace TODO.Views;

public partial class TodoListPage : ContentPage
{
    public TodoListPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var database = await TodoItemDatabase.Instance;
        listView.ItemsSource = await database.GetItemsAsync();
    }

    async void OnAddItem(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new TodoItemPage
        {
            BindingContext = new TodoItem()
        });
    }

    async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem != null)
        {
            await Navigation.PushAsync(new TodoItemPage
            {
                BindingContext = e.SelectedItem as TodoItem
            });
        }
    }
}