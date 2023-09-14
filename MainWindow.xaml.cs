using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace myChat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ChatPanel chatPanel;
        List<User> users;
        DBConnect conn;
        public MainWindow()
        {
            InitializeComponent();
            conn = new DBConnect("Mychat");
            users = conn.mychatLoad();            
        }
        private void btn_signIn_Click(object sender, RoutedEventArgs e)
        {

            var login = tb_login.Text;
            var pass = pass_box.Password.GetHashCode();
            User user = new User(login, pass);
            foreach (User use in users)
            {
                if (use.LoginName == user.LoginName && use.Password == user.Password)
                {
                    chatPanel = new ChatPanel(use,users);
                    chatPanel.Show();
                    this.Close();
                    return;
                }
            }
            MessageBox.Show("Неверный логин или пароль");
        }
       
        private bool suitableLogin(string login)
        {
            if (login.Length > 3)
                return true;
            else
                return false;
        }
       
        private void btn_register_Click(object sender, RoutedEventArgs e)
        {
            if (suitableLogin(tb_login.Text) && isValidatePass(pass_box.Password))
            {
                User user;
                if (users.Count == 0)
                {
                    user = new User(tb_login.Text, pass_box.Password.GetHashCode(),1);
                }
                else
                    user = new User(tb_login.Text, pass_box.Password.GetHashCode());
                users.Add(user);
                conn.AddUser(user);
            }
        }
        private bool isValidatePass(string password){
            Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[^\w\s]).{4,}");
            Match match = regex.Match(password);
            if (match.Success) {
                return true;
            }
            else
            {
                MessageBox.Show("Пароль должен содержать:\nБуквы нижнего и верхнего регистра\n" +
                    "цифры и спецсимволы, а так же быть длинне 4 знаков");
                return false; }
        }
    }
}
