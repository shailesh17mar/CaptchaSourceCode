using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.UI;

namespace CaptchaControl
{
    public partial class Captcha : System.Web.UI.UserControl
    {
        private string _imageCode;
        private int _width;
        private int _height;
        private int _level;
        private int _graphicLevel;
        private int _length;
        private short _tabIndex;



        CaptchaImage cImage;

        public int Level
        {
            set
            {
                _level = value <= 0 ? 2 : (value > 3) ? 3 : value;
            }
        }

        public int GraphicLevel
        {
            set
            {
                _graphicLevel = value <= 0 ? 2 : (value > 3) ? 3 : value;
            }
        }

        public int Width
        {
            set
            {
                _width = (value > 240 || value < 120) ? 120 : value;
            }
            get
            {
                return _width;
            }
        }

        public int Height
        {
            set
            {
                _height = (value > 100 || value < 50) ? 50 : value;
            }

            get
            {
                return _height;
            }
        }

        public int Length
        {
            set
            {
                _length = (value < 4 || value > 8) ? 6 : value;
            }
            get
            {
                return _length;
            }
        }

        public short TabIndex
        {
            get
            {
                return _tabIndex;
            }
            set
            {
                _tabIndex = value;
            }
        }

        private string _id;

        private readonly Dictionary<int, string> _captchaLevel = new Dictionary<int, string>();

        private void SetStyle()
        {
            captcha.ControlStyle.Width = Width + 16;
            captchaImage.ControlStyle.Width = Width;
            captchaImage.ControlStyle.Height = Height;
            refreshContainer.ControlStyle.Height = Height;
            txtImgcode.ControlStyle.Width = Width;
            captcha.ControlStyle.Height = Height + 40;
            txtImgcode.TabIndex = _tabIndex;
        }

        protected override void OnPreRender(EventArgs e)
        {
            
            SetStyle();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _id = ((Captcha)sender).ClientID;
            CreateDictionary();
            if (!IsPostBack)
                Reset();
        }

        protected void refresh_Click(object sender, EventArgs e)
        {
            SetPreLoader();
            Reset();
        }

        private void SetPreLoader()
        {
            captchaImage.ImageUrl ="data:image/gif;base64,R0lGODlhgAAPAPEBAC9rwP///8PU7AAAACH/C05FVFNDQVBFMi4wAwEAAAAh/hpDcmVhdGVkIHdpdGggYWpheGxvYWQuaW5mbwAh+QQECgD/ACwAAAAAgAAPAAACo5QvoYG33NKKUtF3Z8RbN/55CEiNonMaJGp1bfiaMQvBtXzTpZuradUDZmY+opA3DK6KwaQTCbU9pVHc1LrDUrfarq765Ya9u+VRzLyO12lwG10yy39zY11Jz9t/6jf5/HfXB8hGWKaHt6eYyDgo6BaH6CgJ+QhnmWWoiVnI6ddJmbkZGkgKujhplNpYafr5OooqGst66Uq7OpjbKmvbW/p7UAAAIfkEBQoAAQAsAAAAAAcADwAAAgiEj6nL7Q+jLAAh+QQFCgABACwLAAAABwAPAAACCISPqcvtD6MsACH5BAUKAAEALBYAAAAHAA8AAAIIhI+py+0PoywAIfkEBQoAAQAsIQAAAAcADwAAAgiEj6nL7Q+jLAAh+QQFCgABACwsAAAABwAPAAACCISPqcvtD6MsACH5BAUKAAEALDcAAAAHAA8AAAIIhI+py+0PoywAIfkEBQoAAQAsQgAAAAcADwAAAgiEj6nL7Q+jLAAh+QQFCgABACxNAAAABwAPAAACCISPqcvtD6MsACH5BAUKAAEALFgAAAAHAA8AAAIIhI+py+0PoywAIfkEBQoAAQAsYwAAAAcADwAAAgiEj6nL7Q+jLAAh+QQFCgABACxuAAAABwAPAAACCISPqcvtD6MsACH5BAUKAAEALHkAAAAHAA8AAAIIhI+py+0PoywAOw==";
        }

        private void CreateDictionary()
        {
            _captchaLevel.Add(1, "123456789");
            _captchaLevel.Add(2, "23456789ABCDEFGHJKLMNOPQRTVWYZ");
            _captchaLevel.Add(3, "23456789ABDEFGHLMNOQRTYabdefghijkmnqrty");
        }

        private void Reset()
        {
            _imageCode = GenerateRandomCode();
            this.Session["CaptchaImageCode" + _id] = _imageCode;
        
            txtImgcode.Text = "";
            SetCaptcha();
        }

        protected void SetCaptcha()
        {
            cImage = new CaptchaImage(this.Session["CaptchaImageCode" + _id].ToString(), _width, _height, _graphicLevel);

            var ms = new MemoryStream();

            cImage.Image.Save(ms, ImageFormat.Jpeg);
            byte[] byteImage = ms.ToArray();

            Convert.ToBase64String(byteImage);
            //captchaImage.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            captchaImage.Attributes["src"] = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            cImage.Dispose();
        }

        private string GenerateRandomCode()
        {
            var chars = _captchaLevel[_level];
            var random = new Random();
            return new string(Enumerable.Repeat(chars, _length)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());

        }

        public bool VerifyResponse()
        {
            if (string.Equals(txtImgcode.Text, this.Session["CaptchaImageCode" + _id].ToString(), StringComparison.Ordinal))
            {
                return true;
            }
            Reset();
            return false;
        }

    }
}