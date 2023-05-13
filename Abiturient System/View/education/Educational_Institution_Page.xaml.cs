using Abiturient_System.Model;
using Abiturient_System.ViewModel.education;
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

namespace Abiturient_System.View.education
{
    /// <summary>
    /// Логика взаимодействия для Educational_Institution_Page.xaml
    /// </summary>
    public partial class Educational_Institution_Page : Page
    {
        private Educational_InstitutionViewModel educational_InstitutionViewModel;
        public Educational_Institution_Page()
        {
            InitializeComponent();
            TextBlock textBlock =   new TextBlock();
            educational_InstitutionViewModel = new Educational_InstitutionViewModel();

            int column = 1;
            foreach (Educational_Institution educational_Institution in educational_InstitutionViewModel.GetEducational_Institutions())
            {
                StackPanel stackPanel = new StackPanel();

                TextBlock educationName = new TextBlock();
                educationName.FontSize = 30;
                Hyperlink educationNameLink = new Hyperlink();
                educationNameLink.Inlines.Add(new Run(educational_Institution.Name));
                educationNameLink.Click += OpenEducationPage_Click;
                educationNameLink.Tag = educational_Institution;
                educationName.Inlines.Add(educationNameLink);

                TextBlock link = new TextBlock();
                link.FontSize = 15;
                link.Margin = new Thickness(0, 10, 0, 30);
                Hyperlink linkOfEducation = new Hyperlink();
                linkOfEducation.Inlines.Add(new Run(educational_Institution.Link));
                link.Inlines.Add(linkOfEducation);

                stackPanel.Children.Add(educationName);
                stackPanel.Children.Add(link);

                if (column == 1)
                {
                    Col1.Children.Add(stackPanel);
                }
                else
                {
                    Col2.Children.Add(stackPanel);
                    column = 1;
                    continue;
                }
                column++;
            }

            UpdateLayout();
            
        }

        private void OpenEducationPage_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink hyperLink = sender as Hyperlink;
            Educational_Institution educational_Institution = (Educational_Institution) hyperLink.Tag;
            NavigationService.Navigate(new EducationWithFacutliesPage(educational_Institution, educational_InstitutionViewModel));

        }

    }
}
