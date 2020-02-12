//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Tasky.Models;

//namespace Tasky.Services
//{
//    public class MockDataStore : IDataStore<Item>
//    {
//        readonly List<Item> items;

//        public MockDataStore()
//        {
//            items = new List<Item>()
//            {
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Electric Bill", Description="This is an Electric Bill description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Water Bill", Description="This is an Water Bill description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Phone Bill", Description="This is an Phone Bill description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Insurance", Description="This is an Insurance description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "School Tax", Description="This is an School Tax description." },
//                new Item { Id = Guid.NewGuid().ToString(), Text = "Garbage Tax", Description="This is an Garbage Tax description." }
//            };
//        }

//        public async Task<bool> AddItemAsync(Item item)
//        {
//            items.Add(item);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> UpdateItemAsync(Item item)
//        {
//            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
//            items.Remove(oldItem);
//            items.Add(item);

//            return await Task.FromResult(true);
//        }

//        public async Task<bool> DeleteItemAsync(string id)
//        {
//            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
//            items.Remove(oldItem);

//            return await Task.FromResult(true);
//        }

//        public async Task<Item> GetItemAsync(string id)
//        {
//            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
//        }

//        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
//        {
//            return await Task.FromResult(items);
//        }
//    }
//}