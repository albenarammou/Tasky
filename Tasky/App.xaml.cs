using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Tasky.Services;
using Tasky.Views;
using System.IO;

namespace Tasky
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            //DependencyService.Register<MockDataStore>();
            var basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DbDataStore.DatabasePath = Path.Combine(basePath, "tasky.db3");

            DependencyService.Register<DbDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
