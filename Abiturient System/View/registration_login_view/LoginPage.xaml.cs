using Abiturient_System.Repository;
using Abiturient_System.Util;
using Abiturient_System.View.education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Abiturient_System.View.registration_login_view
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private UserRepository userRepository;

        public LoginPage()
        {
            InitializeComponent();
            userRepository = new UserRepository();
        }

        private void SwitchToRegistrationPageClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }

        private void SignIn_Click(object sender, RoutedEventArgs e)
        {
            String phone = Phone.Text;
            String password = Password.Text;

            try
            {
                Authentication.User = userRepository.Login(phone, password);

                NavigationService.Navigate(new Educational_Institution_Page());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

    }
}
