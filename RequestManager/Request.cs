using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Data;
using System.Data.SQLite;
using WebSiteStatusChecker.DatabaseManager;
using System.Runtime.Remoting;

namespace WebSiteStatusChecker.RequestManager
{

    public class Request
    {
        DatabaseConnection databaseConnection = new DatabaseConnection();
        DataTable getAllData = new DataTable();
        public List<string[]> PreparedData = new List<string[]>();

        public void PrepareData()
        {

            getAllData = databaseConnection.ReadAllSitesData();

            for (int i = 0; i < getAllData.Rows.Count; i++)
            {

                PreparedData.Add(getAllData.Rows[i].ItemArray.Select(z => z == null ? string.Empty : z.ToString()).ToArray());
            }

        }


        public void SendRequest()
        {
            int RequestCount = PreparedData.Count();

            for (int i = 0; i < RequestCount; i++)
            {
                var request = (HttpWebRequest)WebRequest.Create(PreparedData[i][1]);
                request.Method = "GET";
                request.Timeout = 20000;

                HttpStatusCode sitestatuscode;
                string sitestatus="";

                if (PreparedData[i][2] == "True")
                {

                    try
                    {
                        using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                        {
                            sitestatuscode = response.StatusCode;
                            sitestatus = sitestatuscode.ToString();
                        }

                    }
                    catch (Exception z)
                    {

                        if (z.Message.Contains("İşlem zaman aşımı"))
                        {
                            sitestatus = "OK " + z.Message.ToString();
                        }
                        else
                        {
                            sitestatus = "NOK " + z.Message.ToString();
                        }                 
                       
                    }


                }
                string sendedsqlstring = "'" + PreparedData[i][1] + "','" + sitestatus.Replace("'", "-") + "','" + DateTime.Now + "'," + PreparedData[i][0];
                databaseConnection.InsertSiteStatus(sendedsqlstring);


            }





        }
    }
}