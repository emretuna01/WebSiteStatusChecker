using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteStatusCheckMailService
{
    public class Database
    {
        private static string _sqlitefilepath = ConfigurationManager.AppSettings["SqliteFilePath"].ToString();
        public static string ConnectionString { get { return "Data Source=" + _sqlitefilepath + ";"; } }

        private static string _sql1 = ConfigurationManager.AppSettings["Sql1"].ToString();
        public static string Sql1 { get { return _sql1; } }

        public SQLiteConnection myConnection;

        public Database()
        {
            myConnection = new SQLiteConnection(ConnectionString);         
        }

        public void OpenConnection()
        {
            if (myConnection.State !=ConnectionState.Open)
            {
                myConnection.Open();

            }
        }
        public void ClosedConnection()
        {
            if (myConnection.State != ConnectionState.Closed)
            {
                myConnection.Close();

            }
        }


    }


}
