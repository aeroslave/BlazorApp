using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Pop3;
using MailKit.Net.Smtp;
using MimeKit;

namespace BlazorApp.Infrastructure.Adapters;

public class MailAdapter
{
    // Там еще есть всякое: https://github.com/jstedfast/MailKit
    public void SendMail()
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Joey Tribbiani", "joey@friends.com"));
        message.To.Add(new MailboxAddress("Mrs. Chanandler Bong", "chandler@friends.com"));
        message.Subject = "How you doin'?";

        message.Body = new TextPart("plain")
        {
            Text =
                @"Hey Chandler,I just wanted to let you know that Monica and I were going to go play some paintball, you in?-- Joey"
        };

        using var client = new SmtpClient();
        client.Connect("smtp.friends.com", 587, false);

        // Note: only needed if the SMTP server requires authentication
        client.Authenticate("joey", "password");

        client.Send(message);
        client.Disconnect(true);
    }

    public void GetMessageFromPop3Client()
    {
        using var client = new Pop3Client();
        client.Connect("pop.friends.com", 110, false);

        client.Authenticate("joey", "password");

        for (var i = 0; i < client.Count; i++)
        {
            var message = client.GetMessage(i);
            Console.WriteLine("Subject: {0}", message.Subject);
        }

        client.Disconnect(true);
    }

    public void GetMessageFromImapClient()
    {
        using (var client = new ImapClient())
        {
            client.Connect("imap.friends.com", 993, true);

            client.Authenticate("joey", "password");

            // The Inbox folder is always available on all IMAP servers...
            var inbox = client.Inbox;
            inbox.Open(FolderAccess.ReadOnly);

            Console.WriteLine("Total messages: {0}", inbox.Count);
            Console.WriteLine("Recent messages: {0}", inbox.Recent);

            for (var i = 0; i < inbox.Count; i++)
            {
                var message = inbox.GetMessage(i);
                Console.WriteLine("Subject: {0}", message.Subject);
            }

            client.Disconnect(true);
        }
    }
}