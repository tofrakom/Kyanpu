using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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
        public static MainWindow AppWindow;
        public MainWindow()
        {
            InitializeComponent();
            AppWindow = this;
        }
        DatabaseHandler dbHandler = new DatabaseHandler();

        private void loadData()
        {
            if (dbHandler.checkConnection())
            {
                Person p;
                if (txtID.Text == "")
                {
                    p = dbHandler.loadData();
                }
                else
                {
                    p = dbHandler.loadData(Convert.ToInt32(txtID.Text));
                }

                //Overall properties
                txtID.Text = p.ID.ToString();
                txtName.Text = p.Name;
                txtParent1.Text = p.Parent1;
                txtParent2.Text = p.Parent2;
                txtBirthday.Text = p.Birthdate;
                txtBirthplace.Text = p.Birthplace;

                //Important properties
                txtHealth.Text = p.Insurance;
                txtEmergency.Text = p.Number;

                //Activity properties (0 = Yes / 1 = No / 2 = Others)
                if (p.Participation == 0)
                {
                    checkActivitiesY.IsChecked = true;
                    checkActivitiesN.IsChecked = false;
                }
                else
                {
                    checkActivitiesN.IsChecked = true;
                    checkActivitiesY.IsChecked = false;
                }

                if (p.CanSwim == 0)
                {
                    checkCanSwimY.IsChecked = true;
                    checkCanSwimN.IsChecked = false;
                }
                else
                {
                    checkCanSwimN.IsChecked = true;
                    checkCanSwimY.IsChecked = false;
                }

                if (p.PermSwim == 0)
                {
                    checkPermSwimY.IsChecked = true;
                    checkPermSwimN.IsChecked = false;
                    checkPermSwimL.IsChecked = false;
                }
                else if (p.PermSwim == 1)
                {
                    checkPermSwimN.IsChecked = true;
                    checkPermSwimY.IsChecked = false;
                    checkPermSwimL.IsChecked = false;
                }
                else
                {
                    checkPermSwimL.IsChecked = true;
                    checkPermSwimY.IsChecked = false;
                    checkPermSwimN.IsChecked = false;
                }

                if (p.Riding == 0)
                {
                    checkRideY.IsChecked = true;
                    checkRideN.IsChecked = false;
                    checkRideS.IsChecked = false;
                }
                else if (p.Riding == 1)
                {
                    checkRideN.IsChecked = true;
                    checkRideY.IsChecked = false;
                    checkRideS.IsChecked = false;
                }
                else
                {
                    checkRideS.IsChecked = true;
                    checkRideY.IsChecked = false;
                    checkRideN.IsChecked = false;
                }

                //Health properties
                lstDiseases.Items.Clear();
                lstDiseases.Items.Add(p.Diseases);

                lstMedication.Items.Clear();
                lstMedication.Items.Add(p.Medication);

                lstAllergies.Items.Clear();
                lstAllergies.Items.Add(p.Allergies);
            }
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            dbHandler.openConnection();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            loadData();
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            int id;
            try
            {
                id = Convert.ToInt32(txtID.Text) + 1;
            }
            catch (Exception)
            {
                id = 1;
            }
            
            txtID.Text = id.ToString();
            loadData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            int id;
            try
            {
                id = ((Convert.ToInt32(txtID.Text) - 1) == 0) ? 1 : (Convert.ToInt32(txtID.Text)-1);
            }
            catch (Exception)
            {
                id = 1;
            }

            txtID.Text = id.ToString();
            loadData();
        }
    }
}
