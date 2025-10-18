using System.Text;
using cbtBackend.Dtos.ResponseModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using iTextSharp.text;
using iTextSharp.text.pdf;
namespace cbtBackend.Services.MailService
{
    public class MailMessageService : IMailMessageService

    {
        IConfiguration _config;
        public MailMessageService(IConfiguration configuration)
        {
            _config = configuration;
        }
        public async Task<bool> SendPlainMessage(MessageDto model)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CBT Platform", _config["MailSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress(model.UserName, model.Email));
            message.Subject = "Hi there ";

            message.Body = new TextPart("plain")
            {
                Text = model.Message
            };

            using var client = new SmtpClient();
            try
            {
                var host = _config["MailSettings:SmtpHost"];
                await client.ConnectAsync(_config["MailSettings:SmtpHost"], int.Parse(_config["MailSettings:SmtpPort"]!), SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config["MailSettings:SenderEmail"], _config["MailSettings:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }

         public async Task<bool> SendAprovalMessage(string userName, string email,bool isApproved)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("CBT Platform", _config["MailSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress(userName, email));
            message.Subject = $"Hi there {userName}";

           if (isApproved == true)
           {
             message.Body = new TextPart("plain")
            {
                Text = "Congratulations. Your Account has been Approved. Welcome aboard\n Proceed to login\nhttps://fadhlullah12.github.io/cbt-systemFrontEnd-main/login.html"
            };
           }
           if (isApproved == false)
           {
             message.Body = new TextPart("plain")
            {
                Text = "Sorry. Your Account Registeration was been Declined"
            };
           }

            using var client = new SmtpClient();
            try
            {
                var host = _config["MailSettings:SmtpHost"];
                await client.ConnectAsync(_config["MailSettings:SmtpHost"], int.Parse(_config["MailSettings:SmtpPort"]!), SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config["MailSettings:SenderEmail"], _config["MailSettings:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> SendResult(SendResultDto model)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var message = new MimeMessage();

            model.UserName = "Balogun Fadhlullah";
            model.UserEmail = "adeyiomoola1@gmail.com";

            message.From.Add(new MailboxAddress("CBT Platform", _config["MailSettings:SenderEmail"]));
            message.To.Add(new MailboxAddress(model.UserName, model.UserEmail));
            message.Subject = $"Hi {model.UserName} 👋";

            // Generate PDF and save to public folder
            var fileName = $"Results_{model.UserName.Replace(" ", "_")}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "downloads", fileName);

            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

            var titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);
            var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, BaseColor.BLACK);

            using (var fs = new FileStream(filePath, FileMode.Create))
            {
                var doc = new Document(PageSize.A4, 50, 50, 50, 50);
                PdfWriter.GetInstance(doc, fs);
                doc.Open();

                doc.Add(new Paragraph($"Results for {model.UserName}", titleFont));
                doc.Add(new Paragraph(" ")); // Spacer

                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SetWidths(new float[] { 3f, 1f, 1f, 1f });

                table.AddCell(new PdfPCell(new Phrase("Title", cellFont)));
                table.AddCell(new PdfPCell(new Phrase("Score", cellFont)));
                table.AddCell(new PdfPCell(new Phrase("Questions", cellFont)));
                table.AddCell(new PdfPCell(new Phrase("Percentage", cellFont)));

                foreach (var result in model.Results)
                {
                    table.AddCell(new PdfPCell(new Phrase(result.Title, cellFont)));
                    table.AddCell(new PdfPCell(new Phrase(result.Score.ToString(), cellFont)));
                    table.AddCell(new PdfPCell(new Phrase(result.Questions.ToString(), cellFont)));
                    table.AddCell(new PdfPCell(new Phrase($"{result.Percentage}%", cellFont)));
                }

                doc.Add(table);
                doc.Close();
            }

            // Build HTML email with download link
            var downloadUrl = $"{_config["AppSettings:BaseUrl"]}/downloads/{fileName}";

            var htmlBuilder = new StringBuilder();
            htmlBuilder.AppendLine("<html><body>");
            htmlBuilder.AppendLine($"<p>Dear {model.UserName},</p>");
            htmlBuilder.AppendLine("<p>Here are your detailed results:</p>");
            htmlBuilder.AppendLine("<table style='border-collapse: collapse; width: 100%; font-family: Arial, sans-serif;'>");
            htmlBuilder.AppendLine("<thead><tr>");
            htmlBuilder.AppendLine("<th style='border: 1px solid #ccc; padding: 8px;'>Title</th>");
            htmlBuilder.AppendLine("<th style='border: 1px solid #ccc; padding: 8px;'>Score</th>");
            htmlBuilder.AppendLine("<th style='border: 1px solid #ccc; padding: 8px;'>Questions</th>");
            htmlBuilder.AppendLine("<th style='border: 1px solid #ccc; padding: 8px;'>Percentage</th>");
            htmlBuilder.AppendLine("</tr></thead>");
            htmlBuilder.AppendLine("<tbody>");

            foreach (var result in model.Results)
            {
                htmlBuilder.AppendLine("<tr>");
                htmlBuilder.AppendLine($"<td style='border: 1px solid #ccc; padding: 8px;'>{result.Title}</td>");
                htmlBuilder.AppendLine($"<td style='border: 1px solid #ccc; padding: 8px;'>{result.Score}</td>");
                htmlBuilder.AppendLine($"<td style='border: 1px solid #ccc; padding: 8px;'>{result.Questions}</td>");
                htmlBuilder.AppendLine($"<td style='border: 1px solid #ccc; padding: 8px;'>{result.Percentage}%</td>");
                htmlBuilder.AppendLine("</tr>");
            }

            htmlBuilder.AppendLine("</tbody></table>");
            htmlBuilder.AppendLine($"<p>You can also <a href='{downloadUrl}' target='_blank'>📄 download your results as a PDF</a>.</p>");
            htmlBuilder.AppendLine("<p>Best regards,<br/>CBT Platform Team</p>");
            htmlBuilder.AppendLine("</body></html>");

            var builder = new BodyBuilder
            {
                HtmlBody = htmlBuilder.ToString()
            };

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_config["MailSettings:SmtpHost"], int.Parse(_config["MailSettings:SmtpPort"]!), SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_config["MailSettings:SenderEmail"], _config["MailSettings:Password"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Email sending failed: {ex.Message}");
                return false;
            }
        }


    }
}