using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LoginControl.Utilities;
using System.Web.SessionState;

namespace LoginControl
{
    /// <summary>
    /// Summary description for CaptchaImage
    /// </summary>
    public class CaptchaImage : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Session["CaptchaImageText"] = GenerateRandomCode();
            // Create a CAPTCHA image using the text stored in the Session object.
            RandomImage ci = new RandomImage(context.Session["CaptchaImageText"].ToString(), 300, 75);
            // Change the response headers to output a JPEG image.
            context.Response.Clear();
            context.Response.ContentType = "image/jpeg";
            // Write the image to the response stream in JPEG format.
            ci.Image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            // Dispose of the CAPTCHA image object.
            ci.Dispose();
        }

        private string GenerateRandomCode()
        {
            Random r = new Random();
            string s = "";
            for (int j = 0; j < 5; j++)
            {
                int i = r.Next(3);
                int ch;
                switch (i)
                {
                    case 1:
                        ch = r.Next(0, 9);
                        s = s + ch.ToString();
                        break;
                    case 2:
                        ch = r.Next(65, 90);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    case 3:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                    default:
                        ch = r.Next(97, 122);
                        s = s + Convert.ToChar(ch).ToString();
                        break;
                }
                r.NextDouble();
                r.Next(100, 1999);
            }
            return s;
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}