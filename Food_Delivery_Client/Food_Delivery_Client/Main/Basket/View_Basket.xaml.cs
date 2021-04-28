using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food_Delivery_Client.Main.Basket
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_Basket : ContentPage
    {
        public View_Basket()
        {
            InitializeComponent();
            BindingContext = new ModelView_Basket();
        }

        public void OnItemTapped(object sender, ItemTappedEventArgs e) => (BindingContext as ModelView_Basket).OnItemTapped(sender, e, this);



    }
}