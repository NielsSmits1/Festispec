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
        public string GeneratePdf(int selectedInspection)
        {
            //stub data
            string htmlCode;
            string toBeParsed;

            //TODO: retrive correct data from database that is linked to correct parameter
            var parsingModel = new RapportageInfo()
            {
                Advice = "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja." +
                        "Meer bier drinken de man, ja en bier ja.",
                CustomerName = "Beunhaas BV",
                Date = DateTime.Now,
                InspectionTitle = "Is er genoeg bier op het festival?",
                Introduction = "Wij Gaan kijken of er genoeg bier is.",
                SummaryOfInspection = "Er was niet genoeg bier de man.",
                Questions = new List<Question>()
                {
                    new Question()
                    {
                        Id = 1,
                        GivenAwnsers = new List<string>()
                        {
                            "A",
                            "A",
                            "B"
                        },
                        QuestionTitle = "Hoeveel bier was er?"
                    },
                    new Question()
                    {
                        Id = 2,
                        GivenAwnsers = new List<string>()
                        {
                            "A",
                            "B",
                            "C"
                        },
                        QuestionTitle = "Hoeveel bier was er verspilt?"
                    },
                    new Question()
                    {
                        Id = 3,
                        GivenAwnsers = new List<string>()
                        {
                            "A",
                            "B",
                            "C"
                        },
                        QuestionTitle = "Hoeveel bier had je op?"
                    }
                }
            };

            using (var reader = new StreamReader(@"~\Template.cshtml"))
            {
                toBeParsed = reader.ReadToEnd();
            }

            var saveLocation = @"~\poc.pdf";
            htmlCode = Razor.Parse(toBeParsed, parsingModel);

            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            Renderer.RenderHtmlAsPdf(htmlCode).SaveAs(saveLocation);
            return saveLocation;
        }
    }
}
