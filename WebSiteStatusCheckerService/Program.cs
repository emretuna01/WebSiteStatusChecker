using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WebSiteStatusCheckerService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
           

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WebSiteCheckerService()
            };
            ServiceBase.Run(ServicesToRun);

            
            
        }
    }
}
