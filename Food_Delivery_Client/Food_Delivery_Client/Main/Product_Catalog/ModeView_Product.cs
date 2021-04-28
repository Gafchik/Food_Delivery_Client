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

namespace Food_Delivery_Client.Main.Product_Catalog
{
    public class ModeView_Product : INotifyPropertyChanged
    {

        public ObservableCollection<Product> Products { get; set; }
        private Products_Repository products_Repository = new Products_Repository();

        public ObservableCollection<Product_Categories> Product_categories { get; set; }
        private Product_Categories_Repository poduct_Categories_Repository = new Product_Categories_Repository();

        public ObservableCollection<Order> Current_Orders { get; set; }
        private Current_Orders_Repository current_order_repository = new Current_Orders_Repository();

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion

        #region init
        public ModeView_Product() { InitializeComponent(); }

        public async void InitializeComponent()
        {
          
                if (Product_categories != null)
                    GC.Collect(GC.GetGeneration(Product_categories));
                Product_categories = new ObservableCollection<Product_Categories>(poduct_Categories_Repository.GetColl());
                OnPropertyChanged("Product_Categories");

                if (Products != null)
                    GC.Collect(GC.GetGeneration(Products));
                Products = new ObservableCollection<Product>(products_Repository.GetColl());
                Products.ToList().ForEach(i => i.Product_category = Product_categories.ToList().Find(j => j.Product_category_Id == i.Product_category_Id));         

            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Current_Orders != null)
                    GC.Collect(GC.GetGeneration(Current_Orders));
                Current_Orders = new ObservableCollection<Order>(current_order_repository.GetColl());
                OnPropertyChanged("Current_Orders");
            }));

        }
        #endregion

        private Product_Categories selected_item_categories; //выбраная категория

        public Product_Categories Selected_Item_Categories
        {
            get { return selected_item_categories; }
            set
            {
                selected_item_categories = value;
                OnPropertyChanged("Selected_Item_Categories");
                if (Products != null)
                    GC.Collect(GC.GetGeneration(Products));
                Products = new ObservableCollection<Product>(products_Repository.GetColl());
                Products.ToList().ForEach(i => i.Product_category = Product_categories.ToList().Find(j => j.Product_category_Id == i.Product_category_Id));
                if (Selected_Item_Categories != null)
                {
                    Products = new ObservableCollection<Product>(products_Repository.GetColl().ToList()
                        .FindAll(i => i.Product_category_Id == Selected_Item_Categories.Product_category_Id));                       
                }
               OnPropertyChanged("Products");
            }

        }

        private Product selected_item; // выбраный товар

        public Product Selected_Item
        {
            get { return selected_item; }
            set
            {
                selected_item = value; OnPropertyChanged("Selected_Item");
            }
        }

        public static ObservableCollection<Order> Basket = new ObservableCollection<Order>(); 

        internal async void OnItemTapped(object sender, ItemTappedEventArgs e, View_Product view_Product)
        {
            Selected_Item  = e.Item as Product;
            if (Selected_Item != null)
            {
               
         
                if (!(await view_Product.DisplayAlert($"Выбраный товар {Selected_Item.Product_Name}", "Добавить в корзину?", "Да", "Нет")))
                    return;
                else
                {
                    Basket.Add(new Order
                    {
                        Order_Products_Name = Selected_Item.Product_Name,
                        Order_Discount = (float)Selected_Item.Product_Discount,
                        Order_Price = (float)Selected_Item.Product_Price,
                        Order_Final_Price = (float)Selected_Item.Product_Price -
                            (((float)Selected_Item.Product_Price / 100) * (float)Selected_Item.Product_Discount)
                    });
                   await  view_Product.DisplayAlert($"Выбраный товар {Selected_Item.Product_Name}", "Добавлен в корзину", "Ок") ;
                }
            }
        }


       

        

    }
}

