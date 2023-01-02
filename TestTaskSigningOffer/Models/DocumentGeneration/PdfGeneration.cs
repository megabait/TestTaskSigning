using iTextSharp.text;
using iTextSharp.text.pdf;
using Org.BouncyCastle.Utilities;
using QRCoder;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection.Emit;
using static System.Net.Mime.MediaTypeNames;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;

namespace TestTaskSigningOffer.Models.DocumentGeneration
{
    public class PdfGeneration : IDocumentGeneration
    {
        static string pathData = Directory.GetCurrentDirectory() + "\\Data";
        BaseFont bf = BaseFont.CreateFont(pathData + "\\times-new-roman.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        public string CreateDocument(Contract contract, Company company)
        {
            byte[] pdf = new byte[] { };

            using (var mem = new MemoryStream())
            {
                using (var doc = new Document(PageSize.LETTER, 10, 10, 42, 35))
                {
                    
                    Font NameDocument = new Font(bf, 28, Font.BOLD);
                    Font FontText = new Font(bf, 16, Font.NORMAL, BaseColor.RED);

                    PdfWriter wri = PdfWriter.GetInstance(doc, mem);

                    doc.Open();

                    Paragraph paragraph = new Paragraph("Договор", NameDocument);
                    paragraph.Alignment = Element.ALIGN_CENTER;
                    doc.Add(paragraph);

                    doc.Add(new Paragraph("\r\n \r\n Text text text text text  text text text text text text text text text text text text" +
                    " text text text text text text text text text text text text text text text text text text text text text text text text" +
                    " text text text text text text text text text text text text text text text text text text text text text text text text" +
                    " text text text text text text text text text text text text text text text text text text text text text text text text" +
                    " text text text text text text text text text text text text text text text text text text text text text text text text" +
                    " text text text text text text text text text text text text text text text text text text text text text text text text" +
                    " text text text text text text text text text text text text text text text text text text text text text text text text."));

                    if (contract.SignSmsClient == true)
                    {
                        doc.Add(new Paragraph("\r\n \r\n Подпись клиента:", new Font(bf, 16)));

                        var imgQr = QrGeneration("ФИО: " + contract.FullName + "\r\n" + "ИИН: " + contract.Iin + "\r\n" + "Телефон: " + contract.Phone);
                        doc.Add(imgQr);                        
                        imgQr = null;
                    }
                    if (contract.SignSmsCompany == true)
                    {
                        doc.Add(new Paragraph("\r\n \r\n Подпись компании: \r\n", new Font(bf, 16)));

                        var imgQr = QrGeneration("Наименование: " + company.Name + "\r\n" + "БИН: " + company.Bin + "\r\n" + "Юр адрес: " + company.Address);
                        doc.Add(imgQr);
                        imgQr = null;                        
                    }
                    if (contract.SignSmsCompany == true && contract.SignSmsClient == true)
                    {
                        doc.Add(new Paragraph("Документ подписан!!!", FontText));
                    }
                }

                pdf = mem.ToArray();
            }

            return Convert.ToBase64String(pdf);
        }

        private Image QrGeneration(string text)
        {
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            {
                using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
                {
                    using (QRCode qrCode = new QRCode(qrCodeData))
                    {
                        using (Bitmap qrCodeImage = qrCode.GetGraphic(3))
                        {
                            //Image imageQr = Image.GetInstance(pathData + "\\qr.png");
                            return Image.GetInstance(qrCodeImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                    }                    
                }                
            }
        }
    }
}
