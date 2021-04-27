using Food_Delivery_Client.Authorization;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food_Delivery_Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new View_Authorization();
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
