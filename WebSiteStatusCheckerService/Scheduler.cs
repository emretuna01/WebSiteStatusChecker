using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WebSiteStatusChecker;
using WebSiteStatusChecker.DatabaseManager;
using WebSiteStatusChecker.RequestManager;
using System.Data.SQLite;
using System.Configuration;
using System.IO;

namespace WebSiteStatusCheckerService
{
    class Scheduler
    {

        Timer oTimer ;

        double interval = 60000*60*2;

        public void start()       {

            //When time is up this bellow code will invoke and call a method oTimer_Elapsed.
            oTimer = new  Timer(interval);
            oTimer.AutoReset = true;
            oTimer.Enabled = true;
            oTimer.Start();
            oTimer.Elapsed += new ElapsedEventHandler(oTimer_Elapsed);

        }

        private void oTimer_Elapsed(object sender, ElapsedEventArgs e)
        {

            string[] filelog = new string[] { DateTime.Now.ToString() };
            File.AppendAllLines("C:\\WebSiteStatusChecker\\WebSiteStatusCheckerService\\bin\\Debug\\log\\requestlog.txt", filelog);

            Request request2 = new Request();
            request2.PrepareData();
            request2.SendRequest();
           
        }

    }
}
