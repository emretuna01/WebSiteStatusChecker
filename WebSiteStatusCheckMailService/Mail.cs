using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Data;


namespace WebSiteStatusCheckMailService
{

    class Mail
    {
        private string _smtpadress = ConfigurationManager.AppSettings["smtpadress"].ToString();
        public string Smtpadress { get { return _smtpadress; } }


        private string _smtpport = ConfigurationManager.AppSettings["smtpport"].ToString();
        public string Smtpport { get { return _smtpport; } }

        private string _username = ConfigurationManager.AppSettings["username"].ToString();
        public string Username { get { return _username; } }

        private string _password = ConfigurationManager.AppSettings["password"].ToString();
        public string Password { get { return _password; } }

        private string _tomaillist = ConfigurationManager.AppSettings["tomaillist"].ToString();
        public string Tomaillist { get { return _tomaillist; } }

        public List<string> Tomail = new List<string>();

        public List<int> GetSubstringLocations(string text, string searchsequence)
        {
            try
            {
                List<int> foundIndexes = new List<int> { };
                int i = 0;
                while (i < text.Length)
                {
                    int cindex = text.IndexOf(searchsequence, i);
                    if (cindex >= 0)
                    {
                        foundIndexes.Add(cindex);
                        i = cindex;
                    }
                    i++;
                }
                return foundIndexes;
            }
            catch (Exception ex) { }
            return new List<int> { };
        }

        public void PrepareToMailListFromConfig()
        {

            string adresslist = Tomaillist;
            string searchindex = ";";
            List<int> foundedIndexes = new List<int> { };
            foundedIndexes = GetSubstringLocations(adresslist, searchindex);
            int z = 0;
            foreach (var item in foundedIndexes)
            {
                int x = item - z;
                var test2 = adresslist.Substring(z, x);
                Tomail.Add(test2.ToString());
                z = item + 1;
            }

        }





    }

    public class MailManager
    {

        DataManagement dataManagement = new DataManagement();
        DataTable myDataTable;
        int dataCount;
        string emailHtmlDesign = @"<p>Merhabalar,</p><p>Aşağıdaki tabloda son 1.5 saat içinde gönderilen isteğe yanıt vermeyen siteler listelenmistir.</p><p>Listelenen siteler çalışmıyor olabilir, Kontrol ediniz</p><p>İyi çalışmalar</p></br><table style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "" > <thead style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; ""> <tr style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; ""> <th style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "">site_id</th> <th style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "">count_error</th> <th style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "">site_name</th> <th style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "">time</th> <th style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "">formatted_time</th> <th style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "">status</th> </tr> </thead> <tbody style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "">";
        public string[] emailHtmlDesignData;
        public string oneEmailHtmlDesignData;
        public string footerEmailHtmlDesignData = "</tbody></ table >";
        public List<string[]> PreparedData;
        public void GetDataCount()
        {
            myDataTable = dataManagement.ReadAllData();
            dataCount = myDataTable.Rows.Count;
        }
        public void GetAllData()
        {
            PreparedData = new List<string[]>();

            if (dataCount > 0)
            {
                for (int i = 0; i < dataCount; i++)
                {
                    PreparedData.Add(myDataTable.Rows[i].ItemArray.Select(j => j == null ? string.Empty : j.ToString()).ToArray());
                }
            }

        }
        public void GetHtmlData()
        {
            emailHtmlDesignData = new string[dataCount];
            if (dataCount > 0)
            {
                for (int i = 0; i < dataCount; i++)
                {
                    emailHtmlDesignData[i] = @"<tr style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; ""> <td style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "">" + PreparedData[i][0].ToString() + "</td>" + @"<td style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left;  "">" + PreparedData[i][1].ToString() + "</td>" + @"<td style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left;  "">" + PreparedData[i][2].ToString() + "</td>" + @"<td style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left; "">" + PreparedData[i][3].ToString() + "</td>" + @"<td style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left;  "">" + PreparedData[i][4].ToString() + "</td>" + @"<td style=""border: 1px solid black; border-collapse: collapse; padding: 5px; text-align: left;  "">" + PreparedData[i][5].ToString() + "</td> </tr>";
                }

            }
            oneEmailHtmlDesignData = string.Join("", emailHtmlDesignData);
        }

        public void SenderMail()
        {
            Mail mail = new Mail();
            mail.PrepareToMailListFromConfig();

            MailMessage mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(mail.Username);
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = emailHtmlDesign + oneEmailHtmlDesignData + footerEmailHtmlDesignData;
            foreach (var item in mail.Tomail)
            {
                mailMessage.To.Add(new MailAddress(item));
            }
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = mail.Smtpadress;
            smtpClient.Port = Convert.ToInt32(mail.Smtpport);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            NetworkCredential networkCredential = new NetworkCredential(mail.Username, mail.Password);
            smtpClient.Credentials = networkCredential;
            if (dataCount > 0)
            {
                mailMessage.Subject = @"''" + PreparedData[0][2].ToString() + "''" + " Adresi Çalışmıyor!";
                smtpClient.Send(mailMessage);
            }



        }

    }

}
