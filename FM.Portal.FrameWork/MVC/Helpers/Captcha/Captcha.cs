using System;
using System.Drawing;
using System.Linq;
using System.Web;

namespace FM.Portal.FrameWork.MVC.Helpers.Captcha
{
   public class Captcha
    {
        public string Generate(string session)
        {
            string captchaBase64;
            Bitmap captchaTemplate;
            string captchaText;
            HttpContext context = HttpContext.Current;
            string[] characters = new string[5];
            PointF[] positions = new PointF[5];

            captchaTemplate = (Bitmap)Image.FromFile(HttpContext.Current.Server.MapPath("~/content/img/captcha.jpg"));
            for (var i = 0; i < 5; i++)
            {
                characters[i] = RandomString(1);
                positions[i] = new PointF(30 + (i * 64), RandomVerticalLocation());

                using (Graphics graphics = Graphics.FromImage(captchaTemplate))
                {
                    using (Font arialFont = new Font("Arial", RandomFontSize()))
                    {
                        graphics.DrawString(characters[i], arialFont, PickBrush(), positions[i]);
                    }
                }
            }
            captchaBase64 = BitmapToBase64(captchaTemplate);
            captchaText = String.Join("", characters);

            context.Session[session] = captchaText;
            return captchaBase64;
        }

        public Random random = new Random();
        public string BitmapToBase64(Bitmap bi)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            bi.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();
            var SigBase64 = Convert.ToBase64String(byteImage); //Get Base64

            return SigBase64;
        }
        public Brush PickBrush()
        {
            Brush[] brushes = new Brush[] {
                Brushes.BlueViolet,
                Brushes.Blue,
                Brushes.Red,
                Brushes.Black,
                Brushes.Brown,
                Brushes.Chocolate,
                Brushes.Crimson,
                Brushes.DarkBlue,
                Brushes.DarkGreen,
                Brushes.DarkRed,
                Brushes.Firebrick,
                Brushes.Indigo,
                Brushes.MidnightBlue,
                Brushes.Maroon,
                Brushes.MediumBlue,
                Brushes.Navy,
                Brushes.Sienna,
                Brushes.PaleVioletRed,
                Brushes.DodgerBlue
            };
            Brush brush = brushes[random.Next(brushes.Length)];

            return brush;
        }
        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHJKMNPQRSTUVWXYZ123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public float RandomVerticalLocation()
        {
            return random.Next(8, 13);
        }
        public int RandomFontSize()
        {
            return random.Next(25, 28);
        }
    }
}
