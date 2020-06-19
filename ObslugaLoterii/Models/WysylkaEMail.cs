using System.Net.Mail;

namespace ObslugaLoterii.Models
{
    /// <summary>
    /// Klasa do wysyłki wiadomości e-mail
    /// </summary>
    class WysylkaEMail
    {
        /// <summary>
        /// The mail
        /// </summary>
        MailMessage mail;
        /// <summary>
        /// The client
        /// </summary>
        SmtpClient client = new SmtpClient
        {
            Host = "smtp.gmail.com",
            Port = 587,
            Credentials = new System.Net.NetworkCredential("loteriaPO@gmail.com", "227986wt11"),
            EnableSsl = true
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="WysylkaEMail"/> class.
        /// </summary>
        public WysylkaEMail()
        {
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="nadawca">Od.</param>
        /// <param name="adresat">Do.</param>
        /// <param name="temat">Temat.</param>
        /// <param name="zawartosc">Zawartosc.</param>
        public void WyslijEmail(string nadawca, string adresat, string temat, string zawartosc)
        {
            mail = new MailMessage(nadawca, "loteriaPO@gmail.com");
            //   mail = new MailMessage(nadawca, adresat);
            mail.Subject = temat;
            mail.Body = zawartosc;
            client.Send(mail);
        }


    }
}
