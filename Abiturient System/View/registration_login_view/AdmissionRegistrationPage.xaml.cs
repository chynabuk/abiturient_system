using Abiturient_System.Model;
using Abiturient_System.Repository;
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
    /// Логика взаимодействия для AdmissionRegistrationPage.xaml
    /// </summary>
    public partial class AdmissionRegistrationPage : Page
    {
        private UserRepository userRepository;

        public AdmissionRegistrationPage()
        {
            userRepository = new UserRepository();
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            String phone = Phone.Text;
            String firtsName = FirstName.Text;
            String lastName = LastName.Text;
            String password = Password.Text;
            String email = Email.Text;
            long facultyId = long.Parse(FacultyId.Text);
            String role = "Приемная комиссия";

            Admission admission = new Admission()
            {
                Phone = phone,
                FirstName = firtsName,
                LastName = lastName,
                Password = password,
                Email = email,
                FacultyId = facultyId,
                Role = role
            };

            userRepository.Register(admission);

            NavigationService.GoBack();
        }
    }


}
