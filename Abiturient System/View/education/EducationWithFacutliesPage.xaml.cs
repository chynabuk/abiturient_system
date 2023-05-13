using Abiturient_System.Model;
using Abiturient_System.Repository;
using Abiturient_System.ViewModel.education;
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
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace Abiturient_System.View.education
{
    /// <summary>
    /// Логика взаимодействия для EducationWithFacutliesPage.xaml
    /// </summary>
    public partial class EducationWithFacutliesPage : Page
    {
        private Educational_Institution education;
        private Educational_InstitutionViewModel educational_InstitutionViewModel;

        public EducationWithFacutliesPage(Educational_Institution education, Educational_InstitutionViewModel educational_InstitutionViewModel)
        {
            this.education = education;
            this.educational_InstitutionViewModel = educational_InstitutionViewModel;
            InitializeComponent();
            initPage();
        }

        private void initPage()
        {
            List<Faculty> faculties = educational_InstitutionViewModel.GetFaculties(education.Id);

            foreach(Faculty faculty in faculties)
            {
                StackPanel stackPanel = new StackPanel();

                TextBlock facultyName = new TextBlock();
                facultyName.FontSize = 30;
                Hyperlink educationNameLink = new Hyperlink();
                educationNameLink.Inlines.Add(new Run(faculty.Name));
                educationNameLink.Click += OpenPlacesPage_Click;
                educationNameLink.Tag = faculty;
                facultyName.Inlines.Add(educationNameLink);

                TextBlock link = new TextBlock();
                link.FontSize = 15;
                link.Margin = new Thickness(0, 10, 0, 30);
                Hyperlink linkOfEducation = new Hyperlink();
                linkOfEducation.Inlines.Add(new Run(faculty.Link));
                link.Inlines.Add(linkOfEducation);

                stackPanel.Children.Add(facultyName);
                stackPanel.Children.Add(link);

                Faculties.Children.Add(stackPanel);

            }

            UpdateLayout();

            
        }
        private void OpenPlacesPage_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = sender as Hyperlink;
            Faculty faculty = (Faculty)hyperLink.Tag;
            NavigationService.Navigate(new FacultyWithApplicationsPage(faculty, educational_InstitutionViewModel));
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
