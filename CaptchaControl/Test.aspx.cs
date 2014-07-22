using System;

namespace CaptchaControl
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CaptchaControl1.VerifyResponse())
            {

            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CaptchaControl2.VerifyResponse())
            {
                //Do something
            }
        }
    }
}