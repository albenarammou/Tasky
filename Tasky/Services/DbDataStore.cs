﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tasky.Models;

namespace Tasky.Services
{
    public class DbDataStore : IDataStore<Item>
    {
        public static string DatabasePath { get; set; }
        public DbDataStore()
        {
            Database = new SQLiteAsyncConnection(DatabasePath,
                SQLiteOpenFlags.Create |
                SQLiteOpenFlags.FullMutex |
                SQLiteOpenFlags.ReadWrite);
            InitializeAsync().SafeFireAndForget(false);
        }
        public Task<Item> GetItemAsync(int id)
        {
            return Database.Table<Item>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }
        public Task<int> SaveItemAsync(Item item)
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
        public Task<int> DeleteItemAsync(Item item)
        {
            return Database.DeleteAsync(item);
        }

        public Task<List<Item>> GetItemsNotDoneAsync()
        {
            // SQL queries are also possible
            //return Database.QueryAsync<Item>("SELECT * FROM [Items] WHERE [Done] = 0");
            return Database.QueryAsync<Item>("SELECT * FROM [Items]");
        }

        private async Task InitializeAsync()
        {
            if (initialized) return;
            
            await Database.CreateTablesAsync(CreateFlags.None, typeof(Item)).ConfigureAwait(false);

            initialized = true;
        }

        private SQLiteAsyncConnection Database;
        private static bool initialized = false;
    }

    public static class TaskExtensions
    {
        // NOTE: Async void is intentional here. This provides a way
        // to call an async method from the constructor while
        // communicating intent to fire and forget, and allow
        // handling of exceptions
        public static async void SafeFireAndForget(this Task task,
            bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }

            // if the provided action is not null, catch and
            // pass the thrown exception
            catch (Exception ex) when (onException != null)
            {
                onException(ex);
            }
        }
    }
}
