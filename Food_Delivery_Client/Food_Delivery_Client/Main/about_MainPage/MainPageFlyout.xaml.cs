using Food_Delivery_Client.Authorization.Regestreted;
using Food_Delivery_Client.Main.Basket;
using Food_Delivery_Client.Main.MyProfile;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food_Delivery_Client.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageFlyout : ContentPage
    {
        public ListView ListView;

        public MainPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MainPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MainPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MainPageFlyoutMenuItem> MenuItems { get; set; }

            public MainPageFlyoutViewModel()
            {
                MenuItems = new ObservableCollection<MainPageFlyoutMenuItem>(new[]
                {
                    new MainPageFlyoutMenuItem { Id = 0, Title = "Мой профиль ", TargetType = typeof(View_MyProfile) },
                    new MainPageFlyoutMenuItem { Id = 1, Title = "Меню вкусняшек", TargetType = typeof(View_Product) },
                    new MainPageFlyoutMenuItem { Id = 2, Title = "История покупок"  },
                    new MainPageFlyoutMenuItem { Id = 3, Title = "Корзина",TargetType = typeof(View_Basket) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}