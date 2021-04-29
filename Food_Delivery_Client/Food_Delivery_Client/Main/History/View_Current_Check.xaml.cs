using Food_delivery_library.About_orders;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Food_Delivery_Client.Main.History.ModeView_History;

namespace Food_Delivery_Client.Main.History
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_Current_Check : ContentPage
    {

        public ObservableCollection<Order> orders;
        public ObservableCollection<Order> My_Orders
        {
            get { return orders; }
            set { orders = value; }
        }
       
        

        public View_Current_Check(ObservableCollection<Order> My_Orders)
        {
            InitializeComponent();
            this.My_Orders = new ObservableCollection<Order>(My_Orders);
            BindingContext = this;


        }

        private void Button_Clicked(object sender, EventArgs e) => Navigation.PopAsync();
        
    }
}