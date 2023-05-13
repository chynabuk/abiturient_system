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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
        }

        private void SwitchToAbiturientRegistrationPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AbiturientRegistrationPage());
        }

        private void SwitchToAdmissionRegistrationPage_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AdmissionRegistrationPage());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
