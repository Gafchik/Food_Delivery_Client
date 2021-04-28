using Food_Delivery_Client.Authorization.Regestreted;
using Food_delivery_library;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Food_Delivery_Client.Authorization
{
    public class ModelView_Authorization : INotifyPropertyChanged
    {
        public INavigation Navigation { get; set; }// я хз зачем это надо

        public ObservableCollection<User> Users { get; set; }
        private User_Repository user_Repository = new User_Repository();

        public ObservableCollection<Blocked_user> Blocked_Users { get; set; }
        private Blocked_users_Repository blocked_Users_Repository = new Blocked_users_Repository();

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion

        #region init
        public ModelView_Authorization() { InitializeComponent(); }

        public async void InitializeComponent()
        {
            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Users != null)
                    Users.Clear();
                Users = new ObservableCollection<User>(user_Repository.GetColl());
                OnPropertyChanged("Users");
            }));
            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Blocked_Users != null)
                    Blocked_Users.Clear();
                Blocked_Users = new ObservableCollection<Blocked_user>(blocked_Users_Repository.GetColl());
                OnPropertyChanged("Blocked_Users");
            }));
        }
        #endregion

        public static User current_user;
        public User Current_User
        {
            get { return current_user; }
            set{ current_user = value; OnPropertyChanged("Current_User"); }
        }

        public string current_phone;

        public string Current_Phone
        {
            get { return current_phone; }
            set { current_phone = value; OnPropertyChanged("Current_Phone"); }
        }

        public string current_pass;

        public string Autor
        {
            get { return current_pass; }
            set { current_pass = value; OnPropertyChanged("Current_Pass"); }
        }


        public Command sing_in; // Вход 
        public Command Sing_In              
         {
             get
             {
                 return sing_in ?? (sing_in = new Command((act) =>
                 {
                     if(Blocked_Users.ToList().Exists(i=>i.Blocked_user_Phone == Current_Phone))
                     {
                       (act as View_Authorization).DisplayAlert("Извините", "Но вы заблокированы", "ОK");
                         return;
                     }
                     if (!Users.ToList().Exists(i => i.User_Phone == Current_Phone))
                     {
                         (act as View_Authorization).DisplayAlert("Извините", "Нужно зарегестрироватся", "ОK");
                         return;
                     }
                     else
                     {
                         Current_User = Users.ToList().Find(i => i.User_Phone == Current_Phone);
                         (act as View_Authorization).DisplayAlert("Урра", "Вы вошли", "ОK");

                     }
                 }));
             }
         }


        public Command reg;
        public Command Reg
        {
            get { return reg ?? (reg = new Command( () => { App.Current.MainPage = new View_Reg(); })); }
        }
          




    }
}