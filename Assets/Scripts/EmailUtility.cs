using System.ComponentModel;
using System.Net.Mail;
using UnityEngine;

public class EmailUtility : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Command-line argument must be the SMTP host.
        SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
        client.Credentials = new System.Net.NetworkCredential(
            "co.thinland@gmail.com",
            "betabots");
        client.EnableSsl = true;
        // Specify the email sender.
        // Create a mailing address that includes a UTF8 character
        // in the display name.
        MailAddress from = new MailAddress(
            "miguel.frazao.santos@gmail.com",
            "Miguel Frazão Santos",
            System.Text.Encoding.UTF8);
        // Set destinations for the email message.
        MailAddress to = new MailAddress("miguel_ffrs@hotmail.com");
        // Specify the message content.
        MailMessage message = new MailMessage(from, to);
        message.Body = " the body . This is a test email message sent by an application. ";
        message.BodyEncoding = System.Text.Encoding.UTF8;
        message.Subject = "test message 1 subject";
        message.SubjectEncoding = System.Text.Encoding.UTF8;
        // Set the method that is called back when the send operation ends.
        client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
        // The userState can be any object that allows your callback
        // method to identify this send operation.
        // For this example, the userToken is a string constant.
        string userState = "test message1 user state";
        client.SendAsync(message, userState);
    }

    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        string token = (string)e.UserState;

        if (e.Cancelled)
        {
            Debug.Log("Send canceled " + token);
        }
        if (e.Error != null)
        {
            Debug.Log("[ " + token + " ] " + " " + e.Error.ToString());
        }
        else
        {
            Debug.Log("Message sent.");
        }
    }
}