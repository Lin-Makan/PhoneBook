using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace PhoneBook
{
    internal class Database
    {
        public SQLiteConnection myConnection;

        //Constructor
        public Database() {
            myConnection = new SQLiteConnection("Data Source = database.sqlite3" );

            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");

            }

        }

        //Open DB
        public void openDB()
        {
            if(myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
           
        }

        //Close DB
        public void closeDB()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
    }
}
