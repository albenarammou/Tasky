﻿using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using Tasky.Models;
using Tasky.Views;

namespace Tasky.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Item> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Item>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Item>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Item;
                Items.Add(newItem);
                await DataStore.SaveItemAsync(newItem);
            });
        }


        async Task ExecuteLoadItemsCommand()
        {
            Items.Clear();

            var items = await DataStore.GetItemsNotDoneAsync();
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }


        //    async Task ExecuteLoadItemsCommand()
        //    {
        //        if (IsBusy)
        //            return;

        //        IsBusy = true;

        //        try
        //        {
        //            Items.Clear();
        //            var items = await DataStore.GetItemsNotDoneAsync();
        //            foreach (var item in items)
        //            {
        //                Items.Add(item);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            Debug.WriteLine(ex);
        //        }
        //        finally
        //        {
        //            IsBusy = false;
        //        }
        //    }
        //}
    }
}