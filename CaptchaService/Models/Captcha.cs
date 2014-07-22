using CaptchaControl;
//using CaptchaService.Models.Encryption;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;

namespace CaptchaService.Models
{
   
    public class CaptchaObject
    {
        public byte[] Image { get; set; }
        public string ImageKey { get; set; }
    }

    public class Captcha
    {
        private string _imageCode;
        private int _width = 200;
        private int _height = 50;
        private int _level;
        private int _graphicLevel;
        private int _length;
        public CaptchaImage cImage;


        public int Level
        {
            set
            {
                _level = (value <1 || value >3)?2:value;
            }
        }

        public int Width
        {
            set
            {
                _width = (value < 120 || value > 240) ? 120 : value;
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
                _height = (value < 50 || value > 100) ? 50 : value;
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

        public int GraphicLevel
        {
            set
            {
                _graphicLevel = (value < 1 || value > 3) ? 2 : value;
            }
        }


        private Dictionary<int, string> _captchaLevel = new Dictionary<int, string>();

        private void CreateDictionary()
        {
            _captchaLevel.Add(1, "123456789");
            _captchaLevel.Add(2, "23456789ABCDEFGHJKLMNOPQRTVWYZ");
            _captchaLevel.Add(3, "23456789ABDEFGHLMNOQRTYabdefghijkmnqrty");
        }

        public string Create()
        {
            CreateDictionary();
            return Reset();
        }

        private string GetMd5Hash(MD5 md5Hash, string input)
        {
            string encryptedText=Encryptor.Encrypt(input);

            //byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            //StringBuilder sBuilder = new StringBuilder();
            //for (int i = 0; i < data.Length; i++)
            //{
            //    sBuilder.Append(data[i].ToString("x2"));
            //}
            return encryptedText;
           // return sBuilder.ToString();
        }

        public bool CheckIfValid(string input, string hash)
        {
            string actualResponse=Encryptor.Decrypt(hash);
            if (string.Equals(actualResponse, input, StringComparison.Ordinal))
                return true;
            return false;
           
        }

        private string Reset()
        {
            _imageCode = GenerateRandomCode();
             UnicodeEncoding ByteConverter = new UnicodeEncoding();
             var imageCode = ByteConverter.GetBytes(_imageCode);

           

             using (MD5 md5Hash = MD5.Create())
             {
                 SetCaptcha();
                 return GetMd5Hash(md5Hash, _imageCode);
             }
        }

        public void SetCaptcha()
        {
            cImage = new CaptchaImage(_imageCode, _width, _height,_graphicLevel);
            var ms = new MemoryStream();

            cImage.Image.Save(ms, ImageFormat.Png);
         
        }

        private string GenerateRandomCode()
        {
            var chars = _captchaLevel[_level];
            var random = new Random();
            return new string(Enumerable.Repeat(chars, _length)
                                  .Select(s => s[random.Next(s.Length)])
                                  .ToArray());

        }
       
    }
}