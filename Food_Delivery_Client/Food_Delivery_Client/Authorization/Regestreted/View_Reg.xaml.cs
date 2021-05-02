using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Food_Delivery_Client.Authorization.Regestreted
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class View_Reg : ContentPage
    {
        public View_Reg()
        {
            InitializeComponent();
            BindingContext = new ModelView_Reg() { Navigation = this.Navigation }; // нваигатион создать во вьюмодел
        }

        private void Add_Clicked(object sender, EventArgs e) => (BindingContext as ModelView_Reg).Add_new(Name.Text, Surname.Text, Phone.Text, Pass.Text, E_mail.Text, Card.Text,this, activity);


    }
}