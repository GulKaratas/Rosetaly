using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Rentaly.BusinessLayer.Abstract;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.Globalization;

namespace Rentaly.BusinessLayer.Concrete
{
    public class EmailManager : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendReservationApprovedEmailAsync(
            string email,
            string customerName,
            string carName,
            DateTime pickupDate,
            DateTime returnDate,
            decimal totalPrice,
            string discountCode)
        {
            var settings = _configuration.GetSection("EmailSettings");

            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                settings["SenderName"] ?? "Rentaly",
                settings["SenderEmail"]!));

            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = "Rentaly | Rezervasyonunuz Onaylandı";

            // PNG kupon görselini bellekte oluştur.
            byte[] couponImage = CreateCouponImage(discountCode);

            var bodyBuilder = new BodyBuilder();

            var linkedImage = bodyBuilder.LinkedResources.Add(
                "rentaly-coupon.png",
                new MemoryStream(couponImage));

            linkedImage.ContentId = "rentaly-coupon";
           

            string safeCustomerName =
                System.Net.WebUtility.HtmlEncode(customerName);

            string safeCarName =
                System.Net.WebUtility.HtmlEncode(carName);

            bodyBuilder.HtmlBody = $@"
<!DOCTYPE html>
<html lang=""tr"">
<body style=""margin:0;padding:24px;background:#f2f4f7;
             font-family:Arial,sans-serif;color:#202938;"">
  <div style=""max-width:620px;margin:auto;background:#ffffff;
              border-radius:12px;overflow:hidden;"">
    <div style=""background:#111827;color:#ffffff;
                text-align:center;padding:28px;"">
      <h1 style=""margin:0;letter-spacing:3px;"">RENTALY</h1>
      <p style=""margin-bottom:0;"">Rezervasyonunuz onaylandı!</p>
    </div>

    <div style=""padding:28px;"">
      <h2>Merhaba {safeCustomerName},</h2>
      <p>Rezervasyonunuz başarıyla onaylandı. Detaylarınız aşağıdadır.</p>

      <div style=""background:#f8fafc;padding:18px;border-radius:8px;"">
        <p><strong>Araç:</strong> {safeCarName}</p>
        <p><strong>Alış:</strong> {pickupDate:dd.MM.yyyy HH:mm}</p>
        <p><strong>İade:</strong> {returnDate:dd.MM.yyyy HH:mm}</p>
        <p><strong>Toplam:</strong>
          {totalPrice.ToString("N2", CultureInfo.GetCultureInfo("tr-TR"))} TL</p>
      </div>

      <h3 style=""margin-top:28px;"">Size özel kupon</h3>
      <p>Bir sonraki ziyaretiniz için hazırladığımız kupon görseliniz:</p>

      <div style=""text-align:center;margin:20px 0;"">
        <img src=""cid:rentaly-coupon""
             alt=""Rentaly indirim kuponu""
             style=""display:block;width:100%;max-width:540px;height:auto;
                    margin:auto;border:0;"" />
      </div>

      <p>Güvenli ve keyifli yolculuklar dileriz.</p>
      <p>Saygılarımızla,<br><strong>Rentaly Ekibi</strong></p>
    </div>

    <div style=""background:#f2f4f7;padding:18px;text-align:center;
                font-size:12px;color:#667085;"">
      Bu e-posta rezervasyon bilgilendirmesi amacıyla gönderilmiştir.<br>
      © Rentaly - Araç Kiralama
    </div>
  </div>
</body>
</html>";

            message.Body = bodyBuilder.ToMessageBody();

            using var smtp = new SmtpClient();

            await smtp.ConnectAsync(
                settings["SmtpServer"]!,
                int.Parse(settings["Port"]!),
                SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(
                settings["Username"]!,
                settings["Password"]!);

            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }


        private static byte[] CreateCouponImage(string discountCode)
        {
            const int width = 1000;
            const int height = 420;

            using var image = new Image<Rgba32>(width, height);

            var background = Color.ParseHex("#111827");
            var gold = Color.ParseHex("#D8B46A");
            var white = Color.ParseHex("#FFFFFF");
            var muted = Color.ParseHex("#D1D5DB");

            var titleFont = SystemFonts.CreateFont("Arial", 46, FontStyle.Bold);
            var subtitleFont = SystemFonts.CreateFont("Arial", 25, FontStyle.Regular);
            var codeFont = SystemFonts.CreateFont("Arial", 46, FontStyle.Bold);
            var footerFont = SystemFonts.CreateFont("Arial", 21, FontStyle.Regular);

            var frame = new RectangleF(18, 18, width - 36, height - 36);
            var codePanel = new RectangleF(70, 168, width - 140, 120);
            float centerX = width / 2f;

            image.Mutate(ctx =>
            {
                ctx.BackgroundColor(background);
                ctx.Draw(gold, 5, frame);

                ctx.DrawText(
                    new RichTextOptions(titleFont)
                    {
                        Origin = new PointF(centerX, 48),
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    "RENTALY",
                    gold);

                ctx.DrawText(
                    new RichTextOptions(subtitleFont)
                    {
                        Origin = new PointF(centerX, 112),
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    "Sonraki rezervasyonunuza ozel",
                    muted);

                ctx.Fill(Color.ParseHex("#0B1220"), codePanel);
                ctx.Draw(gold, 3, codePanel);

                ctx.DrawText(
                    new RichTextOptions(codeFont)
                    {
                        Origin = new PointF(
                            codePanel.Left + codePanel.Width / 2f,
                            codePanel.Top + codePanel.Height / 2f),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    },
                    discountCode,
                    white);

                ctx.DrawText(
                    new RichTextOptions(footerFont)
                    {
                        Origin = new PointF(centerX, 330),
                        HorizontalAlignment = HorizontalAlignment.Center
                    },
                    "Bir sonraki yolculugunuzda kullanin",
                    muted);
            });

            using var stream = new MemoryStream();
            image.SaveAsPng(stream);

            return stream.ToArray();
        }

    }
}
