using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using FestiSpec.Domain.Model;
using Festispec_WPF.Helpers;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.Pdf;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class RapportageVM : ViewModelBase
    {
        #region properties
        public ICommand GenerateCommand { get; set; }
        public ICommand SelectInspection { get; set; }

        private IUnitOfWork UOW;

        private string _chart;
        //public variables
        public ObservableCollection<Question> questions { get; set; }
        public ObservableCollection<Inspectie> inspecties { get; set; }
        public ObservableCollection<Inspectie_Wel_Ingevuld_Vragenlijst> vragenlijsten { get; set; }

        public ObservableCollection<string> Charts { get; set; }

        //hidden
        private string _showRapportageSelect;
        public string showRapportageSelect
        {
            get { return _showRapportageSelect; }
            set { _showRapportageSelect = value; RaisePropertyChanged(); }
        }

        private string _showVragenlijstSelect;
        public string showVragenlijstSelect
        {
            get { return _showVragenlijstSelect; }
            set { _showVragenlijstSelect = value; RaisePropertyChanged(); }
        }

        private string _showGenerate;
        public string showGenerate
        {
            get { return _showGenerate; }
            set { _showGenerate = value; RaisePropertyChanged(); }
        }

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

        private Inspectie _selectedInspection;
        public Inspectie selectedInspection
        {
            get { return _selectedInspection; }
            set
            {
                _selectedInspection = value;
                showRapportageSelect = "hidden";
                showVragenlijstSelect = "visible";
                vragenlijsten = new ObservableCollection<Inspectie_Wel_Ingevuld_Vragenlijst>(UOW.Inspections.Get(_selectedInspection.Inspectienummer).Inspectie_Wel_Ingevuld_Vragenlijst);
                RaisePropertyChanged();
            }
        }

        private Inspectie _selectedVragenlijst;
        public Inspectie selectedVragenlijst
        {
            get { return _selectedVragenlijst; }
            set
            {
                _selectedVragenlijst = value;
                showVragenlijstSelect = "hidden";
                showGenerate = "visible";
                PrepareGenerate(_selectedVragenlijst.Vragenlijst.First().ID, selectedInspection.Inspectienummer);
                RaisePropertyChanged();
            }
        }

        private RapportageInfo _rapportageInfo;
        public RapportageInfo rapportageInfo
        {
            get { return _rapportageInfo; }
            set { _rapportageInfo = value; RaisePropertyChanged(); }
        }
        #endregion

        public RapportageVM()
        {
            showVragenlijstSelect = "hidden";
            showGenerate = "hidden";

            UOW = ViewModelLocator.UOW;
            GenerateCommand = new RelayCommand(GeneratePdf);

            inspecties = new ObservableCollection<Inspectie>(UOW.Inspections.GetAll().Where(a => a.Voltooid == true));
        }

        public void PrepareGenerate(int vragenlijstId, int inspectionId)
        {
            var inspection = UOW.Inspections.Get(inspectionId).Inspectie_Wel_Ingevuld_Vragenlijst.Where(q => q.Vragenlijst.Stamt_af_van_ID == vragenlijstId).ToList();
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

                    var awns = new List<string>();
                    var head = new List<string>();
                    ins.Vragenlijst.Tabelvraag_vragenlijst.ToList().ForEach(a =>
                    {
                        a.Tabelvraag.Tabelvraag_antwoord.Where(awnser => awnser.Vragenlijst_ID == ins.Vragenlijst_ID).ToList().ForEach(awn => awns.Add(awn.Antwoord));
                        a.Tabelvraag.Tabelvraag_antwoord.Where(awnser => awnser.Vragenlijst_ID == ins.Vragenlijst_ID).ToList().ForEach(awn => head.Add(awn.Situatie));
                        vragen.Add(new Question()
                        {
                            Id = a.Tabelvraag_ID,
                            QuestionTitle = a.Tabelvraag.Vraag,
                            TabelHeadAwnser = a.Tabelvraag.VraagKop,
                            TabelHeadQuestion = a.Tabelvraag.AntwoordKop,
                            GivenAwnsers = new List<string>(awns),
                            TabelHeaders = new List<string>(head),
                            Type = "tabel"
                        });
                        awns = new List<string>();
                        head = new List<string>();
                    });

                    ins.Vragenlijst.Kaartvraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Add(new Question()
                        {
                            Id = a.Kaartvraag_ID,
                            QuestionTitle = a.Kaartvraag.Vraag,
                            GivenAwnsers = new List<string>() { "data:image/png;base64," + Convert.ToBase64String(a.Filebytes) },
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

                    ins.Vragenlijst.Kaartvraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Where(q => q.QuestionTitle == a.Kaartvraag.Vraag).First().GivenAwnsers.Add("data:image/png;base64," + Convert.ToBase64String(a.Filebytes));
                    });

                    var awns = new List<string>();
                    ins.Vragenlijst.Tabelvraag_vragenlijst.ToList().ForEach(a =>
                    {
                        a.Tabelvraag.Tabelvraag_antwoord.Where(awnser => awnser.Vragenlijst_ID == ins.Vragenlijst_ID).ToList().ForEach(awn => awns.Add(awn.Antwoord));
                        vragen.Where(q => q.QuestionTitle == a.Tabelvraag.Vraag).First().GivenAwnsers.AddRange(awns);
                        awns = new List<string>();
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

            rapportageInfo = new RapportageInfo()
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
                CustomerName = inspection.First().Inspectie.Klant.Bedrijfsnaam,
                Date = DateTime.Now,
                InspectionTitle = inspection.First().Inspectie.Titel,
                Introduction = "Wij Gaan kijken of er genoeg bier is.",
                SummaryOfInspection = "Er was niet genoeg bier de man.",
                Questions = vragen
            };

            questions = new ObservableCollection<Question>(rapportageInfo.Questions);
            rapportageInfo.Questions = new List<Question>();
            Chart = "Bar";
        }

        public void GeneratePdf()
        {
            var helper = new PdfHelper();
            rapportageInfo.Questions.AddRange(questions);
            var location = helper.GeneratePdf(rapportageInfo);
        }
    }
}
