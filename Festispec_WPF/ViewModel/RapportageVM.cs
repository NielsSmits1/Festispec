using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using FestiSpec.Domain.Model;
using Festispec_WPF.Helpers;
using Festispec_WPF.Model.UnitOfWork;
using Festispec_WPF.Pdf;
using Festispec_WPF.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec_WPF.ViewModel
{
    public class RapportageVM : ViewModelBase
    {
        public ICommand GenerateCommand { get; set; }
        public ICommand SelectVragenlijst { get; set; }
        public ICommand OpenPdfButton { get; set; }

        public ICommand BackToStart { get; set; }


        private IUnitOfWork UOW;

        private string _chart;
        //public variables
        public ObservableCollection<Question> questions { get; set; }
        public ObservableCollection<Vragenlijst> vragenlijsten { get; set; }
        public ObservableCollection<string> Charts { get; set; }

        public bool loaded
        {
            set
            {
                OnLoad();
            }
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
        private RapportageInfo _rapportageInfo;
        public RapportageInfo rapportageInfo
        {
            get { return _rapportageInfo; }
            set { _rapportageInfo = value; RaisePropertyChanged(); }
        }

        private Inspectie _selectedInspection;
        public Inspectie selectedInspection
        {
            get { return _selectedInspection; }
            set
            {
                _selectedInspection = value;
                RaisePropertyChanged();
            }
        }

        private string _AllowTyping;
        public string AllowTyping
        {
            get { return _AllowTyping; }
            set
            {
                _AllowTyping = value;
                RaisePropertyChanged();
            }
        }

        private Vragenlijst _selectedVragenlijst;
        public Vragenlijst selectedVragenlijst
        {
            get { return _selectedVragenlijst; }
            set
            {
                _selectedVragenlijst = value;
                RaisePropertyChanged();
            }
        }

        private string _location;
        public string location
        {
            get { return _location; }
            set
            {
                _location = value;
                RaisePropertyChanged();
            }
        }

        private string _advice;
        public string advice
        {
            get { return _advice; }
            set
            {
                _advice = value;
                RaisePropertyChanged();
            }
        }

        private string _introductie;
        public string introductie
        {
            get { return _introductie; }
            set
            {
                _introductie = value;
                RaisePropertyChanged();
            }
        }

        private string _samenvatting;
        public string samenvatting
        {
            get { return _samenvatting; }
            set
            {
                _samenvatting = value;
                RaisePropertyChanged();
            }
        }

        private string _showGenerate;
        public string showGenerate
        {
            get { return _showGenerate; }
            set
            {
                _showGenerate = value;
                RaisePropertyChanged();
            }
        }

        private string _showSucces;
        public string showSucces
        {
            get { return _showSucces; }
            set
            {
                _showSucces = value;
                RaisePropertyChanged();
            }
        }

        private string _showVragenlijst;
        public string showVragenlijst
        {
            get { return _showVragenlijst; }
            set
            {
                _showVragenlijst = value;
                RaisePropertyChanged();
            }
        }

        public RapportageVM()
        {
            UOW = ViewModelLocator.UOW;
            GenerateCommand = new RelayCommand(GeneratePdf);
            SelectVragenlijst = new RelayCommand(SetVragenlijst);
            OpenPdfButton = new RelayCommand(OpenPdf);
            BackToStart = new RelayCommand(RapportageOverViewOpen);
            showVragenlijst = "Visible";
            showGenerate = "Hidden";
            showSucces = "Hidden";
            AllowTyping = "True";
        }

        void OnLoad()
        {
            var _vragenlijst = new List<Vragenlijst>();
            UOW.Inspections.Get(selectedInspection.Inspectienummer).Inspectie_Wel_Ingevuld_Vragenlijst.ToList().ForEach(a => _vragenlijst.Add(a.Vragenlijst));
            vragenlijsten = new ObservableCollection<Vragenlijst>(_vragenlijst);
            RaisePropertyChanged("vragenlijsten");
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
                            GivenAwnsers = new List<string>() { a.Antwoord },
                            Type = "Open vraag",
                            Visible = "Hidden"
                        });
                    });

                    ins.Vragenlijst.Meerkeuzevraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Add(new Question()
                        {
                            Id = a.Meerkeuzevraag_ID,
                            QuestionTitle = a.Meerkeuzevraag.Vraag,
                            GivenAwnsers = new List<string>() { a.Antwoord },
                            Type = "Meerkeuze vraag",
                            Visible = "Visible"
                        });
                    });

                    ins.Vragenlijst.Bijlagevraag_vragenlijst.ToList().ForEach(a =>
                    {
                        vragen.Add(new Question()
                        {
                            Id = a.Bijlagevraag_ID,
                            QuestionTitle = a.Bijlagevraag.Vraag,
                            GivenAwnsers = new List<string>() { "data:image/png;base64," + Convert.ToBase64String(a.FileBytes) },
                            Type = "Bijlage vraag",
                            Visible = "Hidden"
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
                            Type = "Tabel vraag",
                            Visible = "Hidden"
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
                            Type = "Kaart vraag",
                            Visible = "Hidden"
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

            _rapportageInfo = new RapportageInfo()
            {
                Advice = advice,
                CustomerName = inspection.First().Inspectie.Klant.Bedrijfsnaam,
                Date = DateTime.Now,
                InspectionTitle = inspection.First().Inspectie.Titel,
                Introduction = introductie,
                SummaryOfInspection = samenvatting,
                Questions = vragen
            };

            questions = new ObservableCollection<Question>(_rapportageInfo.Questions);
            _rapportageInfo.Questions = new List<Question>();
            RaisePropertyChanged("questions");
        }

        public void GeneratePdf()
        {
            _rapportageInfo.Advice = advice;
            _rapportageInfo.Introduction = introductie;
            _rapportageInfo.SummaryOfInspection = samenvatting;

            var helper = new PdfHelper();
            _rapportageInfo.Questions.AddRange(questions);
            location = helper.GeneratePdf(_rapportageInfo);
            AllowTyping = "False";
            showSucces = "Visible";
            showGenerate = "Hidden";
        }

        public void OpenPdf()
        {
            if (File.Exists(location))
            {
                Process.Start(location);
            }
        }

        public void SetVragenlijst()
        {
            showVragenlijst = "Hidden";
            showGenerate = "Visible";
            PrepareGenerate((int)selectedVragenlijst.Stamt_af_van_ID, selectedInspection.Inspectienummer);
        }

        public void RapportageOverViewOpen()
        {
            showVragenlijst = "Visible";
            showGenerate = "Hidden";
            showSucces = "Hidden";
            AllowTyping = "True";
            selectedInspection = null;
            selectedVragenlijst = null;
            vragenlijsten = null;
            questions = null;
            location = null;
            advice = "";
            introductie = "";
            samenvatting = "";

            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new InspectionOverview();
            window.Show();
            currentWindow.Close();
        }
    }
}
