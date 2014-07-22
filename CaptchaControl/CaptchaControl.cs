using CaptchaControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Tavisca.TravelNxt.ControlLoader;

namespace CaptchaControl
{
    [ToolboxData("<{0}:CaptchaControl runat=server></{0}:CaptchaControl>")]
    public class CaptchaControl : EmbeddedControlLoader
    {
        private const string VirtualPathPrefix="CaptchaControl";

        public int[] CaptchaLevel = new int[] { 1, 2, 3 };

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public int Level
        {
            get;
            set;
        }


        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public int Width
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public int Height
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public int Length
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public int GraphicLevel
        {
            get;
            set;
        }

        [Bindable(true)]
        [Category("Behavior")]
        [Localizable(true)]
        public short TabIndex
        {
            get;
            set;
        }

        static CaptchaControl()
        {
            System.Web.Hosting.HostingEnvironment.RegisterVirtualPathProvider(new VPP(VirtualPathPrefix));
        }

        public CaptchaControl()
            : base(VirtualPathPrefix)
        {
            ControlClassName = "Captcha";
            AssemblyName = "CaptchaControl";
            ControlNamespace = "CaptchaControl";

        }

        //protected override void OnPreRender(EventArgs e)
        //{
        //    SetControlSettings();
        //    base.OnPreRender(e);
        //}

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            SetControlSettings();
        }

        private void SetControlSettings()
        {
            var control = ControlInstance as Captcha;
            if (control != null)
            {
                control.Height = Height;
                control.Width = Width;
                control.Level = Level;
                control.Length = Length;
                control.GraphicLevel = GraphicLevel;
            }
            
        }

        public bool VerifyResponse()
        {
            var control = ControlInstance as Captcha;
            if (control != null)
            {
               return control.VerifyResponse();
            }
            return false;
        }
    }
}
