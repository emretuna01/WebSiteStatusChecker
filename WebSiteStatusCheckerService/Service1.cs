using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WebSiteStatusCheckerService
{
    public partial class WebSiteCheckerService : ServiceBase
    {
        public WebSiteCheckerService()
        {
            InitializeComponent();
            IntializeScheduler();
        }

        private void IntializeScheduler() 
        { 
            Scheduler schedular = new Scheduler(); 
            schedular.start(); 
        }
        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }
    }
}
