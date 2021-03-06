﻿using System;
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
                try
                {
                    txtID.Text = p.ID.ToString();
                }
                catch (Exception)
                {
                    MessageBox.Show("Die ID konnte nicht gefunden werden.");
                    return;
                    throw;
                }
                    
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

                    //Health properties (WIP)
                    lstDiseases.Items.Clear();
                    string[] diseases = p.Diseases.Split('+');
                    foreach (var disease in diseases)
                    {
                        lstDiseases.Items.Add(disease);
                    }

                    lstMedication.Items.Clear();
                    string[] medication = p.Medication.Split('+');
                    foreach (var med in medication)
                    {
                        lstMedication.Items.Add(med);
                    }

                    lstAllergies.Items.Clear();
                    string[] allergies = p.Allergies.Split('+');
                    foreach (var allergy in allergies)
                    {
                        lstAllergies.Items.Add(allergy);
                    }
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

        private void btnDelDisease_Click(object sender, RoutedEventArgs e)
        {
            lstDiseases.Items.Remove(lstDiseases.SelectedItem);
        }

        private void btnDelMedication_Click(object sender, RoutedEventArgs e)
        {
            lstMedication.Items.Remove(lstMedication.SelectedItem);
        }

        private void btnDelAllergy_Click(object sender, RoutedEventArgs e)
        {
            lstAllergies.Items.Remove(lstAllergies.SelectedItem);
        }

        private void btnAddDisease_Click(object sender, RoutedEventArgs e)
        {
            string item = txtDisease.Text;
            lstDiseases.Items.Add(item);
            txtDisease.Clear();
        }

        private void btnAddMedication_Click(object sender, RoutedEventArgs e)
        {
            string item = txtMedication.Text;
            lstMedication.Items.Add(item);
            txtMedication.Clear();
        }

        private void btnAddAllergy_Click(object sender, RoutedEventArgs e)
        {
            string item = txtAllergy.Text;
            lstAllergies.Items.Add(item);
            txtAllergy.Clear();
        }

        private void btnDataNew_Click(object sender, RoutedEventArgs e)
        {
            //Clearing Text-Fields
            txtName.Clear();
            txtParent1.Clear();
            txtParent2.Clear();
            txtBirthday.Clear();
            txtBirthplace.Clear();
            txtHealth.Clear();
            txtEmergency.Clear();

            //Clearing Check-Boxes
            checkActivitiesY.IsChecked = false;
            checkActivitiesN.IsChecked = false;
            checkCanSwimY.IsChecked = false;
            checkCanSwimN.IsChecked = false;
            checkPermSwimY.IsChecked = false;
            checkPermSwimN.IsChecked = false;
            checkPermSwimL.IsChecked = false;
            checkRideY.IsChecked = false;
            checkRideN.IsChecked = false;
            checkRideS.IsChecked = false;

            //Clearing List-Boxes
            lstDiseases.Items.Clear();
            lstMedication.Items.Clear();
            lstAllergies.Items.Clear();

            //Change the ID
            txtID.Text = dbHandler.getNewID().ToString();

            //Activate the Add-Button
            btnDataAdd.IsEnabled = true;
        }

        private void btnDataDel_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Wollen Sie den Datensatz wirklich löschen?", "Löschen?", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                dbHandler.deleteData(Convert.ToInt32(txtID.Text));
                loadData();
            }
        }

        private void btnDataAdd_Click(object sender, RoutedEventArgs e)
        {
            //Overall properties
            string name = txtName.Text;
            string parent1 = txtParent1.Text;
            string parent2 = txtParent2.Text;
            string birthdate = txtBirthday.Text;
            string birthplace = txtBirthplace.Text;
            string insurance = txtHealth.Text;
            string number = txtEmergency.Text;
            int participation;
            int canSwim;
            int permSwim;
            int riding;
            string diseases;
            string medication;
            string allergies;
            int id = dbHandler.getNewID();
            
            //Activities
            if (checkActivitiesY.IsChecked == true)
            {
                participation = 0;
            }
            else
            {
                participation = 1;
            }

            if (checkCanSwimY.IsChecked == true)
            {
                canSwim = 0;
            }
            else
            {
                canSwim = 1;
            }

            if (checkPermSwimY.IsChecked == true)
            {
                permSwim = 0;
            }
            else if (checkPermSwimN.IsChecked == true)
            {
                permSwim = 1;
            }
            else
            {
                permSwim = 2;
            }

            if (checkRideY.IsChecked == true)
            {
                riding = 0;
            }
            else if (checkRideN.IsChecked == true)
            {
                riding = 1;
            }
            else
            {
                riding = 2;
            }

            //Health
            List<string> disease = new List<string>();
            int length = lstDiseases.Items.Count;
            for (int i = 0; i < length; i++)
            {
                disease.Add(lstDiseases.Items[i].ToString());
            }
            diseases = String.Join("+", disease);
            

            List<string> meds = new List<string>();
            length = lstMedication.Items.Count;
            for (int i = 0; i < length; i++)
            {
                meds.Add(lstMedication.Items[i].ToString());
            }
            medication = String.Join("+", meds);


            List<string> allergy = new List<string>();
            length = lstAllergies.Items.Count;
            for (int i = 0; i < length; i++)
            {
                allergy.Add(lstAllergies.Items[i].ToString());
            }
            allergies = String.Join("+", allergy);

            Person person = new Person(name, parent1, parent2, birthdate, birthplace, insurance, number, participation, canSwim, permSwim, riding, diseases, medication, allergies, id);
            dbHandler.newData(person);
        }
    }
}
