using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace OCRobot.Controllers
{
    public class OCRController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        static readonly string ServerUploadFolder = Path.GetTempPath();

        [HttpPost]
        public async Task<OCRResult> UploadFile()
        {
            // Verify that this is an HTML Form file upload request
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.UnsupportedMediaType));
            }

            // Create a stream provider for setting up output streams
            MultipartFormDataStreamProvider streamProvider = new MultipartFormDataStreamProvider(ServerUploadFolder);

            // Read the MIME multipart asynchronously content using the stream provider we just created.
            await Request.Content.ReadAsMultipartAsync(streamProvider);

            Bitmap image = new Bitmap(streamProvider.FileData.Select(e => e.LocalFileName).First());
            tessnet2.Tesseract ocr = new tessnet2.Tesseract();
            //ocr.SetVariable("tessedit_char_whitelist", "0123456789"); // If digit only
            try
            {
                string path = System.Web.HttpContext.Current.Server.MapPath("~/tessdata"); 
   
                ocr.Init(path, "eng", false); // To use correct tessdata
                List<tessnet2.Word> result = ocr.DoOCR(image, Rectangle.Empty);
                OCRResult res = new OCRResult();
                res.FileNames = streamProvider.FileData.Select(entry => entry.LocalFileName);
                res.RecognizedTextItems = new List<OCRItem>();
                foreach (tessnet2.Word word in result)
                {
                    (res.RecognizedTextItems as List<OCRItem>).Add(
                        new OCRItem()
                        {
                            Text = word.Text,
                            Confidence = word.Confidence
                        }
                    );
                }
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
         
        }
    }


}