using Food_delivery_library;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using System.Threading.Tasks;
using Xamarin.Forms;

namespace Food_Delivery_Client.Authorization.Regestreted
{
   public class ModelView_Reg
    {
        public INavigation Navigation { get; set; }// я хз зачем это надо
        public ObservableCollection<User> Users { get; set; }
        private User_Repository user_repository = new User_Repository();

        public ObservableCollection<Blocked_user> Blocked_Users { get; set; }     
        private Blocked_users_Repository blocked_users_repository = new Blocked_users_Repository();

        #region init
        public ModelView_Reg() { InitializeComponent(); }

        public async void InitializeComponent()
        {
            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Users != null)
                    Users.Clear();
                Users = new ObservableCollection<User>(user_repository.GetColl());
                OnPropertyChanged("Users");
            }));
            await Task.Run(() => App.Current.Dispatcher.BeginInvokeOnMainThread((Action)delegate
            {
                if (Blocked_Users != null)
                    Blocked_Users.Clear();
                Blocked_Users = new ObservableCollection<Blocked_user>(blocked_users_repository.GetColl());
                OnPropertyChanged("Blocked_Users");
            }));

        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged; // ивент обновления
        public void OnPropertyChanged([CallerMemberName] string prop = "")
           => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        #endregion

        internal void Add_new(string Name, string Surname, string Phone, string E_mail, string Card, ContentPage win)
        {
            if (Name == "" || Surname == "" || Phone == "")
            { win.DisplayAlert("Не все поля заполнены", "Ошибка", "Ок"); return; }
            if (Blocked_Users.ToList().Exists(i => i.Blocked_user_Phone == Phone))
            {
                win.DisplayAlert("Извините", "Но вы заблокированы", "ОK");
                return;
            }
            try
            {
                user_repository.Create(new User
                {
                    User_Name = Name,
                    User_Surname = Surname,
                    User_Phone = Phone,
                    User_Email = E_mail,
                    User_Bank_card = Card
                });
                win.DisplayAlert("Успех", "Вы зарегестрировались\nожидайте пароль", "ОK");
                App.Current.MainPage = new View_Authorization();
            }
            catch (Exception)
            {
                win.DisplayAlert("Не все поля заполнены", "Ошибка", "Ок"); return;
            }
           
        }

        #region go to autorization

        public Command go_to_Main;
        public Command Go_to_Main
        {
            get { return go_to_Main ?? (go_to_Main = new Command(() => { App.Current.MainPage = new View_Authorization(); })); }
           
        }

        #endregion
    }
}
