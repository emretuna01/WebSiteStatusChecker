using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace WebSiteStatusCheckMailService
{
    class Scheduler
    {
        Timer oTimer;
        double interval = 60000 * 60*24;       
        


        public void start()
        {            
            oTimer = new Timer(interval);
            oTimer.AutoReset = true;
            oTimer.Enabled = true;
            oTimer.Start();
            oTimer.Elapsed += new ElapsedEventHandler(oTimer_Elapsed);

        }

        private void oTimer_Elapsed(object sender, ElapsedEventArgs e)
        {

            MailManager mailManager = new MailManager();
            mailManager.GetDataCount();
            mailManager.GetAllData();
            mailManager.GetHtmlData();
            mailManager.SenderMail();

            string[] filelog = new string[] { DateTime.Now.ToString() };
            File.AppendAllLines(ConfigurationManager.AppSettings["LogFilePath"].ToString(), filelog);

        }

    }
}
