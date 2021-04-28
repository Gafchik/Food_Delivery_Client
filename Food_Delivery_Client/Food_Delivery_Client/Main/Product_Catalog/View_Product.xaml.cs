using Food_Delivery_Client.Main.Product_Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food_Delivery_Client.Main
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_Product : ContentPage
    {
        public View_Product()
        {
            InitializeComponent();
            BindingContext = new ModeView_Product();
        }

        public  void OnItemTapped(object sender, ItemTappedEventArgs e) => (BindingContext as ModeView_Product).OnItemTapped(sender, e, this);               
        
    }
}