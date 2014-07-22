using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;

namespace CaptchaControl
{

    public class CaptchaImage
    {

        private string _text;
        private int _width;
        private int _height;
        private int _level;
        private Bitmap _image;

        private FontStyle[] _fontStyles;
        private HatchStyle[] _hatchStyles;
        private string[] _fontNames;
        private int[] _fontEmSizes;

        public Bitmap Image
        {
            get { return this._image; }
        }


        public CaptchaImage(string imageCode, int width, int height, int level)
        {
            this._text = imageCode;
            this._level = level;
            this.SetDimensions(width, height);
            this.GenerateImage();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                this._image.Dispose();
        }

        private void SetDimensions(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException("width", width,
                    "Argument out of range, must be greater than zero.");
            if (height <= 0)
                throw new ArgumentOutOfRangeException("height", height,
                    "Argument out of range, must be greater than zero.");
            this._width = width;
            this._height = height;
        }

        private void SetFontsAndHatchStyles()
        {
            _fontStyles = new FontStyle[] { FontStyle.Bold, FontStyle.Italic, FontStyle.Regular };
            _hatchStyles = Enum.GetValues(typeof(HatchStyle)).Cast<HatchStyle>().ToArray();
            _fontNames = new string[] { "Comic Sans MS", "Arial", "Times New Roman", "Georgia", "Verdana", "Geneva" };
            _fontEmSizes = new int[] { 12, 15, 20, 25 };
        }

        private void GenerateImage()
        {
            SetFontsAndHatchStyles();

            string sCaptchaText = this._text;
            var oRandom = new Random();
            var range = 3;
            this._image = new Bitmap(this._width, this._height, PixelFormat.Format32bppArgb);
            Graphics graphicImage = Graphics.FromImage(this._image);
            Brush brush = new SolidBrush(Color.FromArgb((oRandom.Next(100, 255)), (oRandom.Next(100, 255)), (oRandom.Next(100, 255))));
            RectangleF captchaRectangle = new RectangleF(0, 0, this._width, this._height);

            graphicImage.FillRectangle(brush, captchaRectangle);
            graphicImage.TextRenderingHint = TextRenderingHint.AntiAlias;

            if (this._width <= 120 && this._text.Length >= 6)
                range = 1;
            if (_level > 1)
            {
                brush = new HatchBrush(_hatchStyles[oRandom.Next(_hatchStyles.Length - 1)],
                                        Color.FromArgb((oRandom.Next(120, 255)), (oRandom.Next(120, 255)), (oRandom.Next(120, 255))),
                                        Color.White);

                graphicImage.FillRectangle(brush, captchaRectangle);
            }


            var fontName = _fontNames[oRandom.Next(_fontNames.Length - 1)];
            var fontSize = _fontEmSizes[oRandom.Next(range)];
            var fontStyle = _fontStyles[oRandom.Next(_fontStyles.Length - 1)];
            var foreColor = Color.FromArgb(oRandom.Next(0, 100), oRandom.Next(0, 100), oRandom.Next(0, 100));
            var yPosition = this._height / 2 - 15;

            for (var i = 0; i <= sCaptchaText.Length - 1; i++)
            {
                var imageMatrix = new Matrix();
                imageMatrix.Reset();

                int iChars = sCaptchaText.Length;
                int xPosition = this._width / (iChars + 1) * i;


                if (_level > 1)
                {
                    fontName = _fontNames[oRandom.Next(_fontNames.Length - 1)];
                    fontStyle = _fontStyles[oRandom.Next(_fontStyles.Length - 1)];

                    if (_level == 3)
                    {
                        yPosition = oRandom.Next(5, this._height / 2 - 10);
                        fontSize = _fontEmSizes[oRandom.Next(range)];
                    }

                    foreColor = _level == 2 ? Color.FromArgb(oRandom.Next(0, 100), oRandom.Next(0, 100), oRandom.Next(0, 100))
                                                                      : Color.FromArgb(oRandom.Next(90, 120), oRandom.Next(90, 120), oRandom.Next(90, 120));
                }

                graphicImage.DrawString
                (
                      sCaptchaText.Substring(i, 1),
                      new Font(fontName, fontSize, fontStyle),
                      new SolidBrush(foreColor),
                      xPosition,
                      yPosition
                );

                graphicImage.ResetTransform();
            }
        }


    }
}
