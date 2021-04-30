using Food_Delivery_Client.Authorization;
using Food_Delivery_Client.Main.Product_Catalog;
using Food_delivery_library;
using Food_delivery_library.About_orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Food_Delivery_Client.Main.Basket
{
    public class ModelView_Basket : INotifyPropertyChanged
    {
        #region init
        public ObservableCollection<Order> Current_Orders { get; set; }
        private Current_Orders_Repository current_order_repository = new Current_Orders_Repository();

        public ObservableCollection<Current_Cheсk> Current_Cheсks { get; set; }
        private Current_Chek_Repository current_CH_repository = new Current_Chek_Repository();

        public ObservableCollection<Completed_Cheсk> Completed_Cheсks { get; set; }
        private Completed_Chek_Repository completed_CH_repository = new Completed_Chek_Repository();

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
        public ModelView_Basket() { InitializeComponent(); }

        public async void InitializeComponent()
        {
            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {

                if (Completed_Cheсks != null)
                    Completed_Cheсks.Clear();
                Completed_Cheсks = new ObservableCollection<Completed_Cheсk>(completed_CH_repository.GetColl());
                OnPropertyChanged("Completed_Cheсks");

            }));

            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Current_Cheсks != null)
                    Current_Cheсks.Clear();
                Current_Cheсks = new ObservableCollection<Current_Cheсk>(current_CH_repository.GetColl());
                OnPropertyChanged("Current_Cheсks");
            }));

            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Current_Orders != null)
                    Current_Orders.Clear();
                Current_Orders = new ObservableCollection<Order>(current_order_repository.GetColl());
                OnPropertyChanged("Current_Orders");
            }));

            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                Full_Price_Basket = 0;
                Basket.ToList().ForEach(i=> Full_Price_Basket += i.Order_Final_Price);
            }));
        }
        #endregion

        private Order selected_item; // выбраный товар
        public Order Selected_Item
        {
            get { return selected_item; }
            set
            {
                selected_item = value; OnPropertyChanged("Selected_Item");
            }
        }


       

        public User Current_User
        {
            get { return ModelView_Authorization.current_user; }          
        }


        public Command buy;
        public Command Buy
        {
            get { return buy ?? (buy = new Command(async (act) =>
            {
                var window = (act as ContentPage);
                try
                {
                    if(Basket.Count ==0 || Basket == null)
                    {
                        await window.DisplayAlert("Корзина пуста !", "Вы ничего не выбрали =(", "Ок");
                        return;
                    }
                    if (!(await window.DisplayAlert("Подтвержнеие", "Вы подтверждаете заказ?", "Да", "Нет")))
                        return;
                    List<int> q = new List<int>();
                    current_CH_repository.GetColl().ToList().ForEach(i => q.Add(i.Check_Id));
                    completed_CH_repository.GetColl().ToList().ForEach(i => q.Add(i.Check_Id));
                    int temp;
                    if (q.Count > 0)
                        temp = q.Max() + 1;
                    else
                        temp = 1;
                    GC.Collect(GC.GetGeneration(q));
                    current_CH_repository.Create(new Current_Cheсk
                    {

                        Check_Id = temp,
                        Check_Admin = "Через приложение",
                        Check_Date = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                                        DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second),
                        Check_Final_Price = Full_Price_Basket,
                        Check_User_Phone = Current_User.User_Phone

                    });
                }
                catch (Exception)
                {

                    await window.DisplayAlert("Ой", "Что-то пошло не так", "Ок");
                    return;
                }

                Basket.ToList().ForEach(i =>
                {
                    current_order_repository.Create(new Order
                    {
                        Order_Discount = i.Order_Discount,
                        Order_Final_Price = i.Order_Final_Price,
                        Order_Price = i.Order_Price,
                        Order_Products_Name = i.Order_Products_Name,
                        Order_Chek_Id = current_CH_repository.GetColl().ToList()
                        .Find(j => j.Check_User_Phone == Current_User.User_Phone &&
                        j.Check_Final_Price == Full_Price_Basket).Check_Id
                    });
                });
                await window.DisplayAlert("Успех", "Ваш заказ принят", "Ок");
                Basket.Clear();
                Full_Price_Basket = 0;
                Basket.ToList().ForEach(i => Full_Price_Basket += i.Order_Final_Price);

            })); }

        }
        

        public ObservableCollection<Order> Basket  // проп для биндинга статической корзины
        {
            get { return ModeView_Product.Basket; }
            set { ModeView_Product.Basket = value; OnPropertyChanged("Basket");  }
        }

        private float ful_price_basket;

        public float Full_Price_Basket
        {
            get { return ful_price_basket; }
            set { ful_price_basket = value; OnPropertyChanged("Full_Price_Basket"); }
        }


        internal async void OnItemTapped(object sender, ItemTappedEventArgs e, View_Basket view_Basket) // ивент клика
        {
            Selected_Item = e.Item as Order;
            if (Selected_Item != null)
            {


                if (!(await view_Basket.DisplayAlert($"Выбраный товар {Selected_Item.Order_Products_Name}", "Удалить с корзины?", "Да", "Нет")))
                    return;
                else
                {
                    Basket.Remove(Selected_Item);                 
                    Full_Price_Basket = 0;
                    Basket.ToList().ForEach(i => Full_Price_Basket += i.Order_Final_Price);
                    await view_Basket.DisplayAlert($"Выбраный товар {Selected_Item.Order_Products_Name}", "Удален с корзины", "Ок");
                }
            }
        }


    }
}
