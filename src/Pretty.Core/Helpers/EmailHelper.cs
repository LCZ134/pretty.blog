using System;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Pretty.Core.Helpers
{
    public class EmailHelper
    {
        private readonly static string SmtpServer = "smtp.qq.com";//smtp服务器
        private readonly static int SmtpServerPort = 25;
        private readonly static bool SmtpEnableSsl = false;
        private readonly static string SmtpUsername = "1029174296@qq.com";//发件人邮箱地址
        private readonly static string SmtpDisplayName = "Pretty Blog";//发件人昵称
        private readonly static string SmtpUserPassword = "zwuuwrswhronbdgd";//授权码

        /// <summary>
        /// 发送邮件到指定收件人
        /// </summary>
        /// <param name="to">收件人地址</param>
        /// <param name="subject">主题</param>
        /// <param name="mailBody">正文内容（支持HTML）</param>
        /// <param name="copyTos">抄送地址列表</param>
        /// <returns>是否发送成功</returns>
        public static bool Send(string to, string subject, string mailBody, params string[] copyTos)
        {
            #region 邮箱
            //实例化一个发送邮件类。
            MailMessage mailMessage = new MailMessage();

            //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
            mailMessage.From = new MailAddress(SmtpUsername, SmtpUsername);

            //收件人邮箱地址。
            mailMessage.To.Add(new MailAddress(to));//可以有多个收件人

            //邮件标题。
            mailMessage.Subject = subject;

            Random rad = new Random();//实例化随机数产生器rad；

            int value = rad.Next(1000, 10000);

            StringBuilder sb = new StringBuilder();

            sb.Append(subject);

            mailMessage.Body = mailBody;//发送的内容

            //实例化一个SmtpClient类。
            SmtpClient client = new SmtpClient();
            //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
            client.Host = "smtp.qq.com";

            //使用安全加密连接。
            client.EnableSsl = true;

            //不和请求一块发送。
            client.UseDefaultCredentials = false;

            //发件人的smtp服务地址
            client.Credentials = new NetworkCredential(SmtpUsername, SmtpUserPassword);//发件人的邮箱和验证码

            client.Send(mailMessage);//排队发送邮件
            return true;
            #endregion
        }

    
    }
}
