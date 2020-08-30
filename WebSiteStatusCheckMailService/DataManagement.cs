using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteStatusCheckMailService
{
    class DataManagement
    {
        Database databaseObject = new Database();
        

        public DataTable ReadAllData()
        {
            DataTable dataTable = new DataTable();
           
            SQLiteCommand myCommand = SendCommand(Database.Sql1, databaseObject.myConnection);           
            databaseObject.OpenConnection();
            SQLiteDataReader sqlDataReader = myCommand.ExecuteReader();            
            dataTable.Load(sqlDataReader);
            databaseObject.ClosedConnection();
            return dataTable;
        }

        public SQLiteCommand SendCommand(String text, SQLiteConnection connecetion)
        {
            SQLiteCommand command = new SQLiteCommand(text, connecetion);
            return command;

        }







    }
}
