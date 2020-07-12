using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Data.SqlClient;

namespace Kyanpu
{
    public class DatabaseHandler
    {
        //SQL Connection
        SqlConnection conn = new SqlConnection(
            "Server=linux;"+
            "Database=testDB;"+
            "User ID=testUser;"+
            "Password =testPassword123");

        //Establish the SQL Connection
        public void openConnection()
        {
            if(checkConnection() == false)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception)
                {
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
                return false;
            }
        }
    }
}
