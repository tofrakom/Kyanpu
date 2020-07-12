using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Microsoft.Data.SqlClient;

namespace Kyanpu
{
    public class DatabaseHandler
    {
        //SQL Connection
        SqlConnection conn = new SqlConnection(
            "Server=linux;" +
            "Database=testDB;" +
            "User ID=testUser;" +
            "Password =testPassword123");

        //Establish the SQL Connection
        public void openConnection()
        {
            if (conn.State == ConnectionState.Closed)
            {
                SolidColorBrush colorBrush = new SolidColorBrush();
                try
                {
                    //Manipulate Status-Display
                    colorBrush.Color = Color.FromRgb(240, 255, 130);
                    MainWindow.AppWindow.circleStatus.Fill = colorBrush;
                    MainWindow.AppWindow.lblStatus.Content = "Verbinden...";

                    conn.Open();

                    colorBrush.Color = Color.FromRgb(85, 205, 85);
                    MainWindow.AppWindow.circleStatus.Fill = colorBrush;
                    MainWindow.AppWindow.lblStatus.Content = "Verbunden";
                    }
                catch (Exception)
                {
                    colorBrush.Color = Color.FromRgb(255, 55, 55);
                    MainWindow.AppWindow.circleStatus.Fill = colorBrush;
                    MainWindow.AppWindow.lblStatus.Content = "Fehlschlag";
                    var result = MessageBox.Show("Es konnte keine Verbindung hergestellt werden\nErneut versuchen?", "Fehler", MessageBoxButton.YesNo, MessageBoxImage.Error);
                    if (result == MessageBoxResult.Yes) openConnection();
                }
            }

        }

        //State of connection
        public bool checkConnection()
        {
            if (conn.State == ConnectionState.Open)
            {
                return true;
            }
            else
            {
                var result = MessageBox.Show("Keine Verbindung zur Datenbank!\nWollen Sie eine Verbindung aufbauen?", "Fehler", MessageBoxButton.YesNo, MessageBoxImage.Error);
                if (result == MessageBoxResult.Yes) openConnection();
                return false;
            }
        }

        //Load data
        public Person loadData(int id=1)
        {
            string SqlSelect = String.Format("SELECT * FROM Participants WHERE ID={0}", id); //SQL Command to SELECT all data for the participant with the given ID
            SqlCommand cmd = new SqlCommand(SqlSelect, conn);

            string name, parent1, parent2, birthdate, birthplace, insurance, number, diseases, medication, allergies;
            int partID, participation, canSwim, permSwim, riding;

            try {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    //Getting all the data
                    while (reader.Read())
                    {
                        partID = Convert.ToInt32(reader["ID"]);
                        name = reader["Name"].ToString();
                        parent1 = reader["Parent1"].ToString();
                        parent2 = reader["Parent2"].ToString();
                        birthdate = reader["Birthdate"].ToString();
                        birthplace = reader["Birthplace"].ToString();
                        insurance = reader["Insurance"].ToString();
                        number = reader["Number"].ToString();
                        participation = Convert.ToInt32(reader["Participation"]);
                        canSwim = Convert.ToInt32(reader["CanSwim"]);
                        permSwim = Convert.ToInt32(reader["PermSwim"]);
                        riding = Convert.ToInt32(reader["Riding"]);
                        diseases = reader["Diseases"].ToString();
                        medication = reader["Medication"].ToString();
                        allergies = reader["Allergies"].ToString();
                        //Setting the data
                        Person p = new Person(name, parent1, parent2, birthdate, birthplace, insurance, number, participation, canSwim, permSwim, riding, diseases, medication, allergies, partID);
                        return p;
                    }
                    reader.Close();
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Daten konnten nich geladen werden!", "Fehler");
                throw ex;
            }
        }
    }
}
