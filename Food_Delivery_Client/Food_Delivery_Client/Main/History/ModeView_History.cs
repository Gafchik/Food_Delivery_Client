using Food_Delivery_Client.Authorization;
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

namespace Food_Delivery_Client.Main.History
{
    public class ModeView_History : INotifyPropertyChanged
    {

        #region Class Completed Check View

        public class Completed_Cheсk_View : INotifyPropertyChanged
        {           
            public int Check_Id { get; set; }
            public string Check_Admin { get; set; }          
            public DateTime Check_Date { get; set; }
            public float Check_Final_Price { get; set; }

            public ObservableCollection<Order> My_Orders { get; set; }



            #region PropertyChanged
            public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
            public void OnPropertyChanged([CallerMemberName] string prop = "")
               => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            #endregion

        }

        #endregion


        #region init

        public ObservableCollection<Completed_Cheсk> Completed_Cheсks { get; set; }
        private Completed_Chek_Repository completed_CH_repository = new Completed_Chek_Repository();

        public ObservableCollection<Order> Completed_Orders { get; set; }
        private Completed_Orders_Repository completed_order_repository = new Completed_Orders_Repository();


        //public ObservableCollection<Completed_Cheсk> My_Cheks { get; set; }
        public ObservableCollection<Completed_Cheсk_View> My_Completed_Cheсks { get; set; }

        public INavigation Navigation { get; set; }

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion
        public ModeView_History() { InitializeComponent(); }

        public void InitializeComponent()
        {

            if (My_Completed_Cheсks != null)
                My_Completed_Cheсks.Clear();
            My_Completed_Cheсks = new ObservableCollection<Completed_Cheсk_View>();

            if (Completed_Cheсks != null)
                Completed_Cheсks.Clear();
            Completed_Cheсks = new ObservableCollection<Completed_Cheсk>(completed_CH_repository.GetColl()
                .ToList().FindAll(i => i.Check_User_Phone == Current_User.User_Phone));
            OnPropertyChanged("Completed_Cheсks");

            if (Completed_Orders != null)
                Completed_Orders.Clear();
            Completed_Orders = new ObservableCollection<Order>(completed_order_repository.GetColl());

            List<Order> temp_list = new List<Order>();
             Completed_Cheсks.ToList().ForEach(i => Completed_Orders.ToList().FindAll(j => j.Order_Chek_Id == i.Check_Id)
           .ForEach(q => temp_list.Add(q)));
            GC.Collect(GC.GetGeneration(Completed_Orders));
            Completed_Orders = new ObservableCollection<Order>(temp_list);
            GC.Collect(GC.GetGeneration(temp_list));


           foreach (var i in Completed_Cheсks.ToList())
           {
                foreach (var j in Completed_Orders)
                {
                    if (My_Completed_Cheсks.ToList().Exists(q => q.Check_Id == i.Check_Id))
                        if (j.Order_Chek_Id == i.Check_Id)
                            My_Completed_Cheсks.ToList().Find(q => q.Check_Id == i.Check_Id).My_Orders.Add(j);
                        else { }
                    else
                    {
                        My_Completed_Cheсks.Add(new Completed_Cheсk_View
                        {
                            Check_Id = i.Check_Id,
                            Check_Admin = i.Check_Admin,
                            Check_Date = i.Check_Date,
                            Check_Final_Price = i.Check_Final_Price,
                            My_Orders = new ObservableCollection<Order>()
                        });
                        if (j.Order_Chek_Id == i.Check_Id)
                            My_Completed_Cheсks.ToList().Find(q => q.Check_Id == i.Check_Id).My_Orders.Add(j);
                    }
                }
           }
           OnPropertyChanged("Completed_Orders");
 

        }


        #endregion

        public User Current_User
        {
            get { return ModelView_Authorization.current_user; }
            set { ModelView_Authorization.current_user = value; }
        }

        internal async void OnItemTapped(object sender, ItemTappedEventArgs e, ContentPage view_Basket) // ивент клика
        {
            Selected_Item = e.Item as Completed_Cheсk_View;
            if (Selected_Item != null)
            {
                if (!(await view_Basket.DisplayAlert("Посмотреть", "заказаные продукты?", "Да", "Нет")))
                    return;
                else
                    await Navigation.PushAsync(new View_Current_Check(Selected_Item.My_Orders));
                
            }
        }

        private Completed_Cheсk_View selected_item;
        public Completed_Cheсk_View Selected_Item
        {
            get { return selected_item; }
            set { selected_item = value;  OnPropertyChanged("Selected_Item"); }
        }


    }
}

