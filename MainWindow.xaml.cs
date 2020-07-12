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

namespace Kyanpu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        DatabaseHandler dbHandler = new DatabaseHandler();

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            SolidColorBrush colorBrush = new SolidColorBrush();

            colorBrush.Color = Color.FromRgb(240, 255, 130);
            circleStatus.Fill = colorBrush;
            lblStatus.Content = "Verbinden...";

            dbHandler.openConnection();

            if (dbHandler.checkConnection())
            {
                colorBrush.Color = Color.FromRgb(85,205,85);
                circleStatus.Fill = colorBrush;
                lblStatus.Content = "Verbunden";
            }
            else
            {
                colorBrush.Color = Color.FromRgb(205, 0, 0);
                circleStatus.Fill = colorBrush;
                lblStatus.Content = "Fehlschlag";
            }
        }
    }
}
