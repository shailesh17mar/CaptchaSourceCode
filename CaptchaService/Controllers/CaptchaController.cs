using CaptchaService.Models;
using Newtonsoft.Json;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace CaptchaService.Controllers
{

    public class CaptchaController : ApiController
    {

        [HttpGet]
        [ActionName("challenge")]
        public HttpResponseMessage Create(string callback, [FromUri] string customizationParams)
        {
            var httpResponse = new HttpResponseMessage();

            var response = new CaptchaResponse();

            var _captcha = new Captcha();
            if(customizationParams!="{}")
            JsonConvert.PopulateObject(customizationParams, _captcha);

            response.Key = _captcha.Create();

            var image = _captcha.cImage.Image;
            var memoryStream = new MemoryStream();
            image.Save(memoryStream, ImageFormat.Jpeg);
            response.Referer = Request.RequestUri.ToString();
            response.Image = memoryStream.ToArray();

            var jsonResponse=JsonConvert.SerializeObject(response);
            return Request.CreateResponse(HttpStatusCode.OK, jsonResponse);
        }

        [HttpGet]
        [ActionName("Submit")]
        public HttpResponseMessage GetResponse(string callback,[FromUri] string answer)
        {
            var response = new CaptchaAnswer();
            JsonConvert.PopulateObject(answer, response);
            var _captcha = new Captcha();
            var result= _captcha.CheckIfValid(response.Answer,response.Key);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }

}