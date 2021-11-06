using App.Providers.Database.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Providers.Database.Services
{
    public interface ISQLiteService
    {
        Task<List<T>> GetAllItemsAsync<T>() where T : BaseModel, new();
        Task<T> GetItemAsync<T>(int id) where T : BaseModel, new();
        Task<List<T>> FindItemsAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : BaseModel, new();
        Task<T> FindItemAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : BaseModel, new();
        Task<int> SaveItemAsync<T>(T item) where T : BaseModel, new();
        Task<int> DeleteAllItemsAsync<T>() where T : BaseModel, new();
        Task<int> DeleteItemAsync<T>(T item) where T : BaseModel, new();
        Task DeleteItemsAsync<T>(System.Linq.Expressions.Expression<Func<T, bool>> expression) where T : BaseModel, new();
    }
}