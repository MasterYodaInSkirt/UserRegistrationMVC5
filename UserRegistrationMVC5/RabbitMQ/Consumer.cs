using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ
{
    class Consumer
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            var message = "";
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "users",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                    SendEmail(message).Wait();
                };
                channel.BasicConsume(queue: "users",
                                     autoAck: true,
                                     consumer: consumer);






                Console.WriteLine("Consumer is started!");
                Console.WriteLine("Press any key for closing prompt...!");
                Console.ReadKey();
            }
        }

        static async Task SendEmail(string message)
        {
           
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY", EnvironmentVariableTarget.User);
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("functional_account@gmail.com", "New User");
            var subject = "New User Registration";
            var to = new EmailAddress(ConfigurationManager.AppSettings.Get("emailAdmin"), "Admin");
            var plainTextContent = message;
            var htmlContent = "<strong>"+message+"</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
