using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            UOW = new ViewModelLocator().UOW;
            GenerateCommand = new RelayCommand(GeneratePdf);
            Charts = new ObservableCollection<string>()
            {
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
