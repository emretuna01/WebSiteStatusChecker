using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Security;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebSiteStatusChecker.DatabaseManager;
using WebSiteStatusChecker.RequestManager;
using Topshelf;
using System.Timers;
using System.IO;

namespace WebSiteStatusChecker
{
    public partial class Default : System.Web.UI.Page
    {

        DatabaseConnection databaseConnection = new DatabaseConnection();
        Request request = new Request();


        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dataTable = databaseConnection.ReadAllSitesData();

            gridView.DataSource = dataTable;
            gridView.DataBind();

        
 
                 

        }

        protected void insertButton2_Click(object sender, EventArgs e)
        {
            string coresqlsting = updateText.Text.ToString();
            string[] sqlstringfromclient = new string[3];

            void MakeInsertscript()
            {
                sqlstringfromclient[0] = coresqlsting;
                if (isSendRequest.Checked)
                {
                    sqlstringfromclient[1] = "1";
                }
                else
                {
                    sqlstringfromclient[1] = "0";
                }
                if (isSendMail.Checked)
                {
                    sqlstringfromclient[2] = "1";
                }
                else
                {
                    sqlstringfromclient[2] = "0";
                }
            }
            MakeInsertscript();
            databaseConnection.InsertSiteName(sqlstringfromclient);
            this.Page_Load(sender, e);
        }

        public static void MessageBox(Page page, string strMsg)
        {
            ScriptManager.RegisterClientScriptBlock(page, page.GetType(), "alertMessage", "alert('" + strMsg + "')", true);
        }

        protected void sendRequest_Click(object sender, EventArgs e)
        {
            request.PrepareData();
            request.SendRequest();
        }

        protected void gridView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}