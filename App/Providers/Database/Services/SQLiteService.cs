using App.Features.Accessories.Models;
using App.Features.SpareParts.Models;
using App.Providers.Database.Constants;
using App.Providers.Database.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace App.Providers.Database.Services
{
    public class SQLiteService : ISQLiteService
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(DbConstants.DatabasePath, DbConstants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;

        public SQLiteService()
        {
            InitializeAsync().Wait();
        }

        public async Task InitializeAsync()
        {
            await Database.CreateTablesAsync(CreateFlags.None, typeof(Accessory), typeof(SparePart)).ConfigureAwait(false);
        }

        public Task<List<T>> GetAllItemsAsync<T>() where T : BaseModel, new()
        {
            return Database.Table<T>().ToListAsync();
        }

        public Task<T> GetItemAsync<T>(int id) where T : BaseModel, new()
        {
            return Database.Table<T>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<T>> FindItemsAsync<T>(Expression<Func<T, bool>> expression) where T : BaseModel, new()
        {
            return await Database.Table<T>().Where(expression).ToListAsync();
        }

        public async Task<T> FindItemAsync<T>(Expression<Func<T, bool>> expression) where T : BaseModel, new()
        {
            return await Database.Table<T>().Where(expression).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync<T>(T item) where T : BaseModel, new()
        {
            if (item.Id != 0)
            {
                return Database.UpdateAsync(item);
            }
            else
            {
                return Database.InsertAsync(item);
            }
        }

        public Task<int> DeleteAllItemsAsync<T>() where T : BaseModel, new()
        {
            return Database.DeleteAllAsync<T>();
        }

        public Task<int> DeleteItemAsync<T>(T item) where T : BaseModel, new()
        {
            return Database.DeleteAsync(item);
        }

        public async Task DeleteItemsAsync<T>(Expression<Func<T, bool>> expression) where T : BaseModel, new()
        {
            var items = await Database.Table<T>().Where(expression).ToListAsync();
            foreach (var item in items)
            {
                await Database.DeleteAsync(item);
            }
        }
    }
}
