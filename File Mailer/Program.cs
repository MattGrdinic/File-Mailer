using System;
using System.IO;
using System.Net;

using System.Net.Mail;

namespace File_Mailer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: \"Folder Location\" Email Address");
                
                Environment.Exit(0);
            }


            if (!Directory.Exists(args[0]))
            {
                Console.WriteLine("Must Supply A Valid Directory.");

                Environment.Exit(0);
            }

            string[] files = Directory.GetFiles(args[0], "*", SearchOption.AllDirectories);

            // Mail

            using (MailMessage mm = new MailMessage(args[1].ToLower(), args[1].ToLower()))
            {
                mm.Subject = "Emailed File";
                mm.Body = "See Attachments";
                mm.IsBodyHtml = false;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "YOUR EMAIL HOST NAME";
                smtp.EnableSsl = true;

                foreach (var f in files)
                {
                    mm.Attachments.Add(new Attachment(f));
                }

                Console.WriteLine("Sending Email......");
                smtp.Send(mm);

                Console.WriteLine("Email Sent.");

                System.Threading.Thread.Sleep(1000); 
            }
        }
    }
}
