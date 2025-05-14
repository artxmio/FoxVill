using FoxVill.Model;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using QRCoder;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace FoxVill.MainServices.ReceiptGenerator;

public class ReceiptGenerator
{
    public static async void GenerateReceipt(int price, string email)
    {
        string storeName = "FoxVill";
        decimal totalPrice = price;
        string qrData = $"Магазин: {storeName}\nСумма: {totalPrice} руб.";

        // Создание PDF-документа
        PdfDocument document = new PdfDocument();
        document.Info.Title = "Чек покупки";
        PdfPage page = document.AddPage();
        XGraphics gfx = XGraphics.FromPdfPage(page);
        XFont font = new XFont("Arial", 14, XFontStyleEx.Bold);

        // Отображение названия магазина
        gfx.DrawString(storeName, font, XBrushes.Black, new XPoint(50, 50));

        // Отображение общей стоимости
        gfx.DrawString($"Общая стоимость: {totalPrice} руб.", font, XBrushes.Black, new XPoint(50, 100));

        // Генерация QR-кода
        Bitmap qrCodeImage = GenerateQrCode(qrData);
        MemoryStream stream = new MemoryStream();
        qrCodeImage.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
        XImage qrImage = XImage.FromStream(stream);

        // Отображение QR-кода
        gfx.DrawImage(qrImage, 50, 150, 150, 150);

        // Сохранение PDF
        string filename = "Receipt.pdf";
        document.Save(filename);

        var emailService = new EmailService.EmailService();

        await emailService.SendEmailAsync(email);
    }

    private static Bitmap GenerateQrCode(string data)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        return qrCode.GetGraphic(10);
    }
}
