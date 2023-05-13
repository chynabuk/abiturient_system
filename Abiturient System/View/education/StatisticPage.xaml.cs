using Abiturient_System.Model;
using Abiturient_System.Util;
using Abiturient_System.ViewModel.registration_login;
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
using static System.Net.Mime.MediaTypeNames;

namespace Abiturient_System.View.education
{
    /// <summary>
    /// Логика взаимодействия для StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        private AbiturientRegistrationPageModel model;
        public StatisticPage()
        {
            model = new AbiturientRegistrationPageModel();
            InitializeComponent();
            initPage();
            UpdateLayout();
        }

        private void initPage()
        {
            List<Abiturient> abiturients = model.TopFiveAbiturients();

            MinScore.Text = model.MinOrtScore().ToString();
            AverateScore.Text = model.AverageOrtScore().ToString();

            int index = 1;
            foreach (Abiturient abiturient in abiturients)
            {
                StackPanel stackPanel = new StackPanel();
                stackPanel.Margin = new Thickness(0, 40, 0, 0);
                stackPanel.Height = 40;

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

                TextBlock id = new TextBlock();
                id.FontSize = 24;
                id.Text = (index++).ToString();
                grid.Children.Add(id);
                Grid.SetColumn(id, 0);


                TextBlock abiturientPhone = new TextBlock();
                abiturientPhone.FontSize = 24;
                abiturientPhone.Text = abiturient.Phone.ToString();
                grid.Children.Add(abiturientPhone);
                Grid.SetColumn(abiturientPhone, 1);

                TextBlock fullName = new TextBlock();
                fullName.FontSize = 24;
                fullName.Text = abiturient.FirstName + " " + abiturient.LastName;
                grid.Children.Add(fullName);
                Grid.SetColumn(fullName, 2);

                TextBlock ort = new TextBlock();
                ort.FontSize = 24;
                ort.Text = abiturient.OrtScore.ToString();
                grid.Children.Add(ort);
                Grid.SetColumn(ort, 3);

                stackPanel.Children.Add(grid);

                Abiturients.Children.Add(stackPanel);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            NavigationService.Refresh();
            NavigationService.GoBack();
        }
    }
}
