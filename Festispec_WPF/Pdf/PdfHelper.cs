using Festispec_WPF.Pdf;
using RazorEngine;
using System;
using System.Collections.Generic;
using System.IO;

namespace Festispec_WPF.Helpers
{
    public class PdfHelper
    {

        //TODO: parameter of asked servey to have a rapportage generated
        public string GeneratePdf(RapportageInfo parsingModel)
        {
            //stub data
            string htmlCode;
            string toBeParsed;

            //TODO: retrive correct data from database that is linked to correct parameter
           
            using (var reader = new StreamReader(@"pdf/Template.cshtml"))
            {
                toBeParsed = reader.ReadToEnd();
            }

            System.IO.Directory.CreateDirectory("c:\\festispec");
            var saveName = DateTime.Now.ToString("dd-mm-yyyy-hh-mm-ss-fff") + parsingModel.InspectionTitle;
            foreach (char c in System.IO.Path.GetInvalidFileNameChars())
            {
                saveName = saveName.Replace(c, '_');
            }

            var saveLocation = "c:\\festispec\\" + saveName + ".pdf";
            htmlCode = Razor.Parse(toBeParsed, parsingModel);

            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            Renderer.RenderHtmlAsPdf(htmlCode).SaveAs(saveLocation);
            return saveLocation;
        }
    }
}
