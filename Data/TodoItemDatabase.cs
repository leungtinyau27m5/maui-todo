using SQLite;
using TODO.Models;

namespace TODO.Data;

public class TodoItemDatabase
{
    static SQLiteAsyncConnection DatabaseConn;

    public static readonly AsyncLazy<TodoItemDatabase> Instance = new AsyncLazy<TodoItemDatabase>(async () =>
    {
        var instance = new TodoItemDatabase();
        try
        {
            CreateTableResult result = await DatabaseConn.CreateTableAsync<TodoItem>();
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return instance;
    });

    public TodoItemDatabase()
    {
        DatabaseConn = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
    }

    public Task<List<TodoItem>> GetItemsAsync() => DatabaseConn.Table<TodoItem>().ToListAsync();

    public Task<List<TodoItem>> GetItemsNotDoneAsync() => DatabaseConn.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");

    public Task<TodoItem> GetItemAsync(int id) => DatabaseConn.Table<TodoItem>().Where(e => e.ID == id).FirstOrDefaultAsync();

    public Task<int> SaveItemAsync(TodoItem item)
    {
        if (item.ID != 0) return DatabaseConn.UpdateAsync(item);
        return DatabaseConn.InsertAsync(item);
    }

    public Task<int> DeleteItemAsync(TodoItem item) => DatabaseConn.DeleteAsync(item);
}
