using Abiturient_System.Model;
using Abiturient_System.ViewModel.registration_login;
using Microsoft.Win32;
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
    /// Логика взаимодействия для AbiturientRegistrationPage.xaml
    /// </summary>
    public partial class AbiturientRegistrationPage : Page
    {
        private String passportImage;
        private String diplomaImage;
        private String ortCertificateImage;
        private String registrationCertificateImage;
        private AbiturientRegistrationPageModel abiturientRegistrationPageModel;

        public AbiturientRegistrationPage()
        {
            InitializeComponent();
            abiturientRegistrationPageModel = new AbiturientRegistrationPageModel();
        }

        private void AttachPassportImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = CreateOpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                passportImage = dlg.FileName;
                passportImg.Source = InitBitmap(passportImage);

            }

        }

        private void AttachDiplomaImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = CreateOpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                diplomaImage = dlg.FileName;
                diplomaImg.Source = InitBitmap(diplomaImage);

            }
        }

        private void AttachOrtCertificateImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = CreateOpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                ortCertificateImage = dlg.FileName;
                ortCeritificateImg.Source = InitBitmap(ortCertificateImage);

            }
        }

        private void AttachRegistrationCertificateImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = CreateOpenFileDialog();

            if (dlg.ShowDialog() == true)
            {
                registrationCertificateImage = dlg.FileName;
                registrationCertificateImg.Source = InitBitmap(registrationCertificateImage);

            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            String phone = Phone.Text;
            String firtsName = FirstName.Text;
            String lastName = LastName.Text;
            String password = Password.Text;
            String role = "Абитуриент";

            if(phone == null || phone == "")
            {
                throw new Exception("Поле 'Телефон' не должно быть пустым");
            }
            if (firtsName == null || firtsName == "")
            {
                throw new Exception("Поле 'Имя' не должно быть пустым");
            }
            if (lastName == null || lastName == "")
            {
                throw new Exception("Поле 'Фамилия' не должно быть пустым");
            }
            if (password == null || password == "")
            {
                throw new Exception("Поле 'Пароль' не должно быть пустым");
            }
            if (passportImage == null || passportImage == "")
            {
                throw new Exception("Не прикреплена фотография для паспорта");
            }
            if (diplomaImage == null || diplomaImage == "")
            {
                throw new Exception("Не прикреплена фотография для аттестата");
            }
            if (ortCertificateImage == null || ortCertificateImage == "")
            {
                throw new Exception("Не прикреплена фотография для ОРТ Сертификата");
            }
            Abiturient abiturient = new Abiturient()
            {
                Phone = phone,
                FirstName = firtsName,
                LastName = lastName,
                Role = role,
                PassportImage = passportImage,
                OrtCertificateImage = ortCertificateImage,
                DiplomaImage = diplomaImage,
                RegistrationCertificateImage = registrationCertificateImage
            };

            abiturientRegistrationPageModel.register(abiturient);

            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new LoginPage());
        }

        private OpenFileDialog CreateOpenFileDialog()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            return dlg;
        }

        private BitmapImage InitBitmap(String file)
        {
            return new BitmapImage(new Uri(file));
        }
    }
}
