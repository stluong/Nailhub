using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace TNTHelper
{
    public static class Mailing
    {
        private static void SendMail(MailMessage mail, string smtpClient)
        {
            //#if !DEBUG
            try
            {
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = smtpClient;
                    smtp.Port = AppSettings.Get<int>("smtpPort");
                    smtp.Credentials = new NetworkCredential(AppSettings.Get<string>("mailerUser"), AppSettings.Get<string>("mailerPassword"));
                    smtp.EnableSsl = AppSettings.Get<bool?>("smtpSSL") ?? false;
                    smtp.Send(mail);
                }
            }
            catch (Exception ex)
            {
                // todo:  LOG SOMEWHERE?
            }
            //#endif
        }

        public static void SendMail(MailMessage msg)
        {
            string smtpClient = AppSettings.Get<string>("smtpClient");

            if (smtpClient == null)
                throw new InvalidOperationException("There is no SmtpClient element in the application settings.");

            SendMail(msg, smtpClient);
        }
        public static void SendMail(string toAddresses, string subject, string body, string fromAddress, bool isHtml, MailPriority priority, Stream[] attachments, string[] attachmentNames, string bccAddresses)
        {
            MailMessage msg = new MailMessage();
            foreach (string s in toAddresses.Replace(";", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                msg.To.Add(s);
            }
            msg.Subject = subject;
            msg.Body = body;
            msg.From = new MailAddress(fromAddress, "MyBrus", System.Text.Encoding.UTF8);
            msg.IsBodyHtml = isHtml;
            msg.Priority = priority;
            if (attachments != null && attachments.Length > 0)
            {
                if (attachments.Length != attachmentNames.Length)
                    throw new ArgumentException("attachments and attachmentNames must have the same length.");
                for (int i = 0; i < attachments.Length; i++)
                {
                    msg.Attachments.Add(new Attachment(attachments[i], attachmentNames[i]));
                }
            }
            if (bccAddresses != null)
            {
                foreach (string s in bccAddresses.Replace(";", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    msg.Bcc.Add(s);
                }
            }
            SendMail(msg);
        }
        public static void SendMail(string toAddresses, string subject, string body, string fromAddress, bool isHtml, MailPriority priority, string[] attachments, string[] attachmentNames, string bccAddresses)
        {
            List<Stream> streams;
            List<String> names;
            if (attachments != null)
            {
                streams = new List<Stream>();
                if (attachmentNames != null)
                    names = new List<string>(attachmentNames);
                else
                    names = new List<string>();
                for (int i = 0; i < attachments.Length; i++)
                {
                    streams.Add(File.Open(attachments[i], FileMode.Open, FileAccess.Read, FileShare.Read));
                    if (names.Count < i + 1)
                    {
                        FileInfo fi = new FileInfo(attachments[i]);
                        names.Add(fi.Name);
                    }
                    else
                    {
                        names.Add(attachmentNames[i]);
                    }
                }
                SendMail(toAddresses, subject, body, fromAddress, isHtml, priority, streams.ToArray(), names.ToArray(), bccAddresses);
            }
            else
            {
                SendMail(toAddresses, subject, body, fromAddress, isHtml, priority, (Stream[])null, (string[])null, bccAddresses);
                return;
            }

            foreach (Stream s in streams)
            {
                try
                {
                    s.Close();
                    s.Dispose();
                }
                catch (Exception)
                {
                    //
                }
            }

        }
        public static void SendMail(string toAddresses, string subject, string body, string fromAddress, bool isHtml, MailPriority priority, string[] attachments, string[] attachmentNames)
        {
            SendMail(toAddresses, subject, body, fromAddress, isHtml, priority, attachments, attachmentNames, null);
        }
        public static void SendMail(string toAddresses, string subject, string body, string fromAddress, bool isHtml, MailPriority priority, string[] attachments)
        {
            SendMail(toAddresses, subject, body, fromAddress, isHtml, priority, attachments, null);
        }
        public static void SendMail(string toAddresses, string subject, string body, string fromAddress, bool isHtml, MailPriority priority)
        {
            SendMail(toAddresses, subject, body, fromAddress, isHtml, priority, null);
        }
        public static void SendMail(string toAddresses, string subject, string body, string fromAddress, bool isHtml)
        {
            SendMail(toAddresses, subject, body, fromAddress, isHtml, MailPriority.Normal);
        }
        public static void SendMail(string toAddresses, string subject, string body, string fromAddress)
        {
            SendMail(toAddresses, subject, body, fromAddress, false);
        }
        public static void SendMail(string toAddresses, string subject, string body)
        {
            SendMail(toAddresses, subject, body, AppSettings.Get<string>("mailerUser"));
        }
        
        public static void SendExceptionMail(string toAddresses, string subject, Exception exception)
        {
            SendExceptionMail(toAddresses, subject, exception, null);
        }
        public static void SendExceptionMail(string toAddresses, string subject, Exception exception, string body)
        {
            StringBuilder sb = new StringBuilder();
            if (!body.IsEmpty())
                sb.AppendFormat("{0}<br/><br/>", body);
            sb.AppendFormat("Message: {0}<br/>", exception.Message);
            sb.AppendFormat("Source: {0}<br/>", exception.Source);
            sb.AppendFormat("Method: {0}<br/>", exception.TargetSite == null ? string.Empty : exception.TargetSite.Name);
            sb.AppendFormat("Stack: {0}<br/>", exception.StackTrace);
            Mailing.SendMail(toAddresses, subject, sb.ToString(), AppSettings.Get<string>("mailerUser"), true);
        }

        #region >> SendException >>

        public static void SendException(Exception ex)
        {
            Mailing.SendException(ex, string.Empty);
        }

        public static void SendException(Exception ex, string applicationName)
        {
            Mailing.SendException(ex, applicationName, string.Empty);
        }

        public static void SendException(Exception ex, string applicationName, string additionalInfo)
        {
            string subject = "Mybruss ERROR [{0}]: {1}";
            string useApplicationName = ex.Source;

            if (!String.IsNullOrEmpty(applicationName ?? string.Empty))
            {
                useApplicationName = applicationName;
            }

            string useMessage = ex.Message;
            if (useMessage.Length > 61)
            {
                useMessage = useMessage.Substring(0, 61) + "...";
            }

            subject = string.Format(subject, useApplicationName, useMessage);

            StringBuilder body = new StringBuilder(1024);
            body.AppendFormat("<p>The application &quot;{0}&quot; threw the following exception:</p>", useApplicationName);

            Exception useEx = ex;
            while (useEx != null)
            {
                body.AppendFormat("<div><strong>Message:</strong><br />{0}</div>", useEx.Message);
                body.AppendFormat("<div><strong>Stack Trace:</strong><br /><pre>{0}</pre></div>", useEx.StackTrace);

                if (useEx.InnerException != null)
                {
                    body.Append("<hr /><div><strong>Inner Exception:</strong></div>");
                }

                useEx = useEx.InnerException;
            }

            if (!string.IsNullOrEmpty(additionalInfo ?? string.Empty))
            {
                body.Append("<hr /><strong>Additional Info:</strong>");
                body.AppendFormat("<p>{0}</p>", additionalInfo.Trim());
            }

            MailAddress devs = new MailAddress(AppSettings.Get<string>("supportUser") ?? AppSettings.Get<string>("mailerUser"), "TNT Support!!");
            MailMessage mm = new MailMessage();
            mm.From = devs;
            mm.To.Add(devs);

            mm.Headers.Add("X-SystemException", "true");

            mm.Subject = subject;
            mm.Body = body.ToString();
            mm.IsBodyHtml = true;

            try
            {
                Mailing.SendMail(mm);
            }
            catch
            {
                //throw it away
            }
        }

        #endregion << SendException <<

        public static void SendFax(string Subject, string Body, string FromAddress, string ToAddresses, string Attachment,
                       bool IsPlainText)
        {
            MailMessage Message = new MailMessage(FromAddress, ToAddresses, Subject, Body);
            if ((Attachment != "") && (Attachment != string.Empty))
            {
                Attachment mail_attachment = new Attachment(Attachment);
                string disposition = string.Format("attachment; filename = {0}", Attachment);
                mail_attachment.ContentDisposition.DispositionType = disposition;
                Message.Attachments.Add(mail_attachment);
            }

            Message.Body = Body;
            Message.Subject = Subject;
            Message.IsBodyHtml = !IsPlainText;
            //Message.ReplyTo = new MailAddress(FromAddress);
            Message.Headers.Add("Content-Transfer-Encoding", "base64");

            SendMail(Message);
        }
    }
}
