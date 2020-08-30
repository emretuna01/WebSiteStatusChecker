using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SQLite;
using System.Data;
using System.Configuration;
using System.Runtime.Remoting.Messaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace WebSiteStatusChecker.DatabaseManager
{
    public  class Database
    {
        // Derleme Yapılacak V2de
        /*
        private static string _sqlitefilepath = ConfigurationManager.AppSettings["SqliteFilePath"].ToString();
        public static string ConnectionString { get { return "Data Source=" + _sqlitefilepath + ";"; } }

        private static string _sql1 = ConfigurationManager.AppSettings["Sql1"].ToString();
        public static string Sql1 { get { return _sql1; } }

        private static string _sql2 = ConfigurationManager.AppSettings["Sql2"].ToString();
        public static string Sql2 { get { return _sql2; } }

        private static string _sql3 = ConfigurationManager.AppSettings["Sql3"].ToString();
        public static string Sql3 { get { return _sql3; } }
        */
        private static string _sqlitefilepath = "C:\\WebSiteStatusChecker\\DatabaseManager\\WebStatusChecker.sqlite";//ConfigurationManager.AppSettings["SqliteFilePath"].ToString();
        public static string ConnectionString { get { return "Data Source=" + _sqlitefilepath + ";"; } }

        private static string _sql1 = "select * from sites";        
        public static string Sql1 { get { return _sql1; } }

        private static string _sql2 = "select MAX(id) as id from sites";
        public static string Sql2 { get { return _sql2; } }

        private static string _sql3 = "select MAX(id) as id from sites_status";
        public static string Sql3 { get { return _sql3; } }
        


    }

    public class DatabaseConnection
    {

         
            SQLiteConnection connection = new SQLiteConnection(Database.ConnectionString);      


        public void SQLiteConnectionOpen()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();

            }
        }
        public void SQLiteConnectionClosed()
        {
            connection.Close();
        }

        public int FindMaxValueOfSiteTable()
        {
            DataTable dataTable2 = new DataTable();
            SQLiteConnectionOpen();
            SQLiteCommand command2 = new SQLiteCommand(Database.Sql2, connection);
            SQLiteDataReader reader2= command2.ExecuteReader();
            dataTable2.Load(reader2);           
            int maxid =Convert.ToInt32(dataTable2.Rows[0]["id"]);
            SQLiteConnectionClosed();
            return maxid;           
        }
        public int FindMaxValueOfSiteStatusTable()
        {
            DataTable dataTable3 = new DataTable();
            SQLiteConnectionOpen();
            SQLiteCommand command3 = new SQLiteCommand(Database.Sql3, connection);
            SQLiteDataReader reader3 = command3.ExecuteReader();
            dataTable3.Load(reader3);
            int max3id = Convert.ToInt32(dataTable3.Rows[0]["id"]);
            SQLiteConnectionClosed();
            return max3id;
        }

        public void InsertSiteName(string[] sql)
        {
            int maxid = FindMaxValueOfSiteTable() + 1;
            if (maxid<1){maxid = 1;}
            string formattedsqlstring = "INSERT INTO sites(id,sitename, status, sendmail)VALUES("+ maxid+",'"+sql[0]+"'," + sql[1] + "," + sql[2]+");";
            SQLiteConnectionOpen();
            SQLiteCommand command = new SQLiteCommand(formattedsqlstring, connection);
            command.ExecuteReader();
            SQLiteConnectionClosed();
        }

        public void InsertSiteStatus(string  sql)
        {            
            int maxid = FindMaxValueOfSiteStatusTable() + 1;
            if (maxid < 1) { maxid = 1; }
            string formattedsqlstring = "INSERT INTO sites_status(site_name, site_status, time, site_id) VALUES("+ sql+"); ";
            SQLiteConnectionOpen();
            SQLiteCommand command = new SQLiteCommand(formattedsqlstring, connection);
            command.ExecuteReader();
            SQLiteConnectionClosed();
        }

        public DataTable ReadAllSitesData()
        {
            SQLiteConnectionOpen();
           
            SQLiteCommand command = new SQLiteCommand(Database.Sql1, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            DataTable dataTable = new DataTable();
            dataTable.Load(reader);

            SQLiteConnectionClosed();

            return dataTable;

        }

    }

}