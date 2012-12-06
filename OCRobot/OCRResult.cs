﻿using OCRobot.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCRobot
{
    public class OCRResult
    {
        /// <summary>
        /// Gets or sets the local path of the file saved on the server.
        /// </summary>
        /// <value>
        /// The local path.
        /// </value>
        public IEnumerable<string> FileNames { get; set; }

        /// <summary>
        /// Gets or sets the submitter as indicated in the HTML form used to upload the data.
        /// </summary>
        /// <value>
        /// The submitter.
        /// </value>
        public IEnumerable<OCRItem> RecognizedTextItems { get; set; }
    }

}