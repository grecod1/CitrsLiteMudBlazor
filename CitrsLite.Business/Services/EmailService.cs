﻿using iText.Html2pdf;
using iText.Kernel.Pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CitrsLite.Business.Services
{
    public class EmailService
    {
        private ParticipantService _participantService;
        public EmailService(ParticipantService participantService)
        {
            _participantService = participantService;
        }

        public async Task EmailAsync(int participantId, string path)
        {
            try
            {
                var template = await _participantService.GetTemplateAsync(participantId, path);



                MailMessage mailMessage = new MailMessage();
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = template;
                mailMessage.To.Add(new MailAddress("Daniel.Greco@fdacs.gov"));
                mailMessage.From = (new MailAddress("Citrs@fdacs.gov", "Citrs"));
                mailMessage.Subject = "Testing Attachment";
                

                using (MemoryStream stream = new MemoryStream())
                {
                    
                    await Task.Run(() =>
                    {
                        using (PdfWriter pdfWriter = new PdfWriter(stream))
                        {
                            stream.Position = 0;
                            pdfWriter.IsCloseStream();
                            HtmlConverter.ConvertToPdf(template, pdfWriter);
                            pdfWriter.SetCloseStream(false);


                                                        
                            mailMessage.Attachments.Add(new Attachment(stream, "participant.pdf"));



                            


                        }

                    });
                    SmtpClient client = new SmtpClient("relay.freshfromflorida.com", 25);
                    await client.SendMailAsync(mailMessage);
                }                                         

                



            }
            catch (Exception ex)
            {
                throw;
            }

        }

        
    }
}
