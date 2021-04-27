using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food_Delivery_Client.Authorization
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_Authorization : ContentPage
    {      
        public View_Authorization()
        {
          
            InitializeComponent();
            BindingContext = new ModelView_Authorization() { Navigation = this.Navigation }; // нваигатион создать во вьюмодел
        }
    }
}