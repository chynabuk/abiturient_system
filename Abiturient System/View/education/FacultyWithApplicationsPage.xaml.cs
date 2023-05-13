using Abiturient_System.Model;
using Abiturient_System.Repository;
using Abiturient_System.Util;
using Abiturient_System.ViewModel.education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

namespace Abiturient_System.View.education
{
    /// <summary>
    /// Логика взаимодействия для FacultyWithApplicationsPage.xaml
    /// </summary>
    public partial class FacultyWithApplicationsPage : Page
    {
        private Faculty faculty;
        private Educational_InstitutionViewModel educational;
        private UserRepository userRepository;

        public FacultyWithApplicationsPage(Faculty faculty, Educational_InstitutionViewModel educational)
        {
            this.faculty = faculty;
            this.educational = educational;
            userRepository = new UserRepository();
            InitializeComponent();
            initPage();
            UpdateLayout();
        }

        private void initPage()
        {
            int placesAmount = faculty.PlaceAmount;
            FacultyName.Text = faculty.Name;

            if (Authentication.User.Role.Equals("Приемная комиссия"))
            {
                SentApplication.Visibility = Visibility.Hidden;
                SentApplication.IsEnabled = false;
            }
            DataAmount.Text = "Количество мест для отбора: " + faculty.PlaceAmount.ToString();

            List<ApplicationForm> applications = educational.GetApplicationFormsByFacultySortedByOrtScore(faculty);

            int index = 1;
            foreach (ApplicationForm application in applications)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Margin = new Thickness(0, 40, 0, 0);
                stackPanel.Height = 40;

                if(index <= placesAmount)
                {
                    stackPanel.Background = new SolidColorBrush(Color.FromRgb(109, 199, 121));
                }
                Grid grid = new Grid();

                ColumnDefinition columnDefinition = new ColumnDefinition();
                columnDefinition.Width = new GridLength(50, GridUnitType.Pixel);
                grid.ColumnDefinitions.Add(columnDefinition);

                ColumnDefinition columnDefinition2 = new ColumnDefinition();
                columnDefinition2.Width = new GridLength(200, GridUnitType.Pixel);
                grid.ColumnDefinitions.Add(columnDefinition2);

                ColumnDefinition columnDefinition3 = new ColumnDefinition();
                columnDefinition3.Width = new GridLength(450, GridUnitType.Pixel);
                grid.ColumnDefinitions.Add(columnDefinition3);

                ColumnDefinition columnDefinition4 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columnDefinition4);

                ColumnDefinition columnDefinition5 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columnDefinition5);

                ColumnDefinition columnDefinition6 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columnDefinition6);

                ColumnDefinition columnDefinition7 = new ColumnDefinition();
                grid.ColumnDefinitions.Add(columnDefinition7);

                TextBlock applicationId = new TextBlock();
                applicationId.FontSize = 24;
                applicationId.Text = (index++).ToString();
                grid.Children.Add(applicationId);
                Grid.SetColumn(applicationId, 0);


                TextBlock abiturientPhone = new TextBlock();
                abiturientPhone.FontSize = 24;
                abiturientPhone.Text = application.AbiturientId.ToString();
                grid.Children.Add(abiturientPhone);
                Grid.SetColumn(abiturientPhone, 1);

                TextBlock fullName = new TextBlock();
                fullName.FontSize = 24;
                fullName.Text = application.FirstName + " " + application.LastName;
                grid.Children.Add(fullName);
                Grid.SetColumn(fullName, 2);

                TextBlock ort = new TextBlock();
                ort.FontSize = 24;
                ort.Text = application.OrtScore.ToString();
                grid.Children.Add(ort);
                Grid.SetColumn(ort, 3);

                TextBlock status = new TextBlock();
                status.FontSize = 24;
                status.Text = application.Status.ToString();
                grid.Children.Add(status);
                Grid.SetColumn(status, 4);

                if (Authentication.User.Role.Equals("Приемная комиссия") && faculty.Id == ((Admission)Authentication.User).FacultyId)
                {
                    Button accept = new Button();
                    accept.Content = "Принять";
                    accept.Width = 150;
                    grid.Children.Add(accept);
                    accept.Click += Confirm_Click;
                    accept.Tag = application;
                    Grid.SetColumn(accept, 5);

                    Button reject = new Button();
                    reject.Content = "Отклонить";
                    reject.Width = 150;
                    grid.Children.Add(reject);
                    reject.Click += Reject_Click;
                    reject.Tag = application;
                    Grid.SetColumn(reject, 6);
                }
              


                stackPanel.Children.Add(grid);

                Applications.Children.Add(stackPanel);
            }
        }

        private void SentApplication_Click(object sender, RoutedEventArgs e)
        {
            if (((Abiturient)Authentication.User).ApplicationAvailable > 0)
            {
                List<ApplicationForm> applications = educational.GetApplicationForms();

                ApplicationForm application = new ApplicationForm()
                {
                    Id = applications.Count + 1,
                    AbiturientId = Authentication.User.Phone,
                    FacultyId = faculty.Id,

                };
                try
                {
                    educational.sentApplicationForm(application);
                    ((Abiturient)Authentication.User).ApplicationAvailable = ((Abiturient)Authentication.User).ApplicationAvailable - 1;
                    back();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Вы не сможете отправить заявку, так как у вас не усталось талона");
            }

        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ApplicationForm application = (ApplicationForm)button.Tag;
            Abiturient abiturient = userRepository.GetAbiturient(application.AbiturientId);

            application.abiturient = abiturient;
            educational.ConfirmApplicationForm(application);
            back();
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            ApplicationForm application = (ApplicationForm)button.Tag;
            educational.RejectApplicationForm(application);
            back();
            
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            back();
        }

        private void back()
        {
            NavigationService.Refresh();
            NavigationService.GoBack();
        }

    }
}
