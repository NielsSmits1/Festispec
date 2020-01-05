using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Festispec_WPF.Helpers;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.Pdf;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class RapportageVM : ViewModelBase
    {
        public ICommand GenerateCommand { get; set; }
        private IUnitOfWork UOW;

        private string _chart;
        //public variables
        public ObservableCollection<Question> questions { get; set; }
        public ObservableCollection<string> Charts { get; set; }

        public string Chart
        {
            get
            {
                return _chart;
            }
            set
            {
                _chart = value; RaisePropertyChanged(() => Chart);
            }
        }

        private RapportageInfo _selectedInspection;
        public RapportageInfo selectedInspection
        {
            get { return _selectedInspection; }
            set { _selectedInspection = value; RaisePropertyChanged(); }
        }

        public RapportageVM()
        {
            UOW = ViewModelLocator.UOW;
            GenerateCommand = new RelayCommand(GeneratePdf);

            var id = UOW.Inspections.Get(11).Vragenlijst.First().ID;
            var inspection = UOW.Inspections.Get(11).Inspectie_Wel_Ingevuld_Vragenlijst.Where(q=> q.Vragenlijst.Stamt_af_van_ID == id).ToList();
            inspection.Add(inspection.First());
            var vragen = new List<Question>();

            var first = true;
            foreach (var ins in inspection)
            {
                if (first)
                {
                    ins.Vragenlijst.Openvraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Add(new Question()
                        {
                            Id = a.Openvraag_ID,
                            QuestionTitle = a.Openvraag.Vraag,
                            GivenAwnsers = new List<string>() { a.Antwoord }
                        });
                    });

                    ins.Vragenlijst.Meerkeuzevraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Add(new Question()
                        {
                            Id = a.Meerkeuzevraag_ID,
                            QuestionTitle = a.Meerkeuzevraag.Vraag,
                            GivenAwnsers = new List<string>() { a.Antwoord }
                        });
                    });

                    ins.Vragenlijst.Bijlagevraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Add(new Question()
                        {
                            Id = a.Bijlagevraag_ID,
                            QuestionTitle = a.Bijlagevraag.Vraag,
                            GivenAwnsers = new List<string>() { "data:image/png;base64," + Convert.ToBase64String(a.FileBytes) },
                            Type = "img"
                        });
                    });
                }
                else
                {
                    ins.Vragenlijst.Openvraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Where(q => q.QuestionTitle == a.Openvraag.Vraag).First().GivenAwnsers.Add(a.Antwoord);
                    });

                    ins.Vragenlijst.Meerkeuzevraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Where(q => q.QuestionTitle == a.Meerkeuzevraag.Vraag).First().GivenAwnsers.Add(a.Antwoord);
                    });

                    ins.Vragenlijst.Bijlagevraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Where(q => q.QuestionTitle == a.Bijlagevraag.Vraag).First().GivenAwnsers.Add("data:image/png;base64," + Convert.ToBase64String(a.FileBytes));
                    });
                }

                first = false;
            }

            Charts = new ObservableCollection<string>()
            {
                "",
                "Bar",
                "Row",
                "Pie"
            };

            selectedInspection = new RapportageInfo()
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
                Questions = vragen
            };

            questions = new ObservableCollection<Question>(selectedInspection.Questions);
            selectedInspection.Questions = new List<Question>();
            Chart = "Bar";
        }

        public void GeneratePdf()
        {
            var helper = new PdfHelper();
            selectedInspection.Questions.AddRange(questions);
            var location = helper.GeneratePdf(selectedInspection);
        }
    }
}
