using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace OCRobot.Controllers
{
    internal class StreamProvider : MultipartFormDataStreamProvider
    {
        public StreamProvider(string rootpath)
            : base(rootpath)
        {
        }
        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            // Check that we have a content type
            if (headers != null && headers.ContentType != null)
            {
                MediaTypeHeaderValue contentType = headers.ContentType;
                return base.GetLocalFileName(headers);
            }
            else
            {
                // Default to base behavior
                return base.GetLocalFileName(headers);
            }
        }
    }
}