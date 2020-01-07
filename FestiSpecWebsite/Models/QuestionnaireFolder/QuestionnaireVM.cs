using FestiSpec.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FestiSpecWebsite.Models.QuestionnaireFolder
{
    public class QuestionnaireVM
    {
        public int inspectionId {get;set;}
        private Vragenlijst _questionnaire;
        public int refreshcounter { get; set; }
        public string Title { get => _questionnaire.Titel; set { _questionnaire.Titel = value; } }
        [Key]
        public int ID { get { return _questionnaire.ID; } set { _questionnaire.ID = value; } }
        public string Version { get { return _questionnaire.Versie; } set { _questionnaire.Versie = value; } }
        private FestiSpecEntities db = new FestiSpecEntities();
        public List<String> questiontypes { get; set; }

        public List<int> OpenQuestionPosition { get; set; }
        public List<OpenQuestionVM> openQuestionsList { get; set; }
        public List<int> MultipleChoiceQuestionPosition { get; set; }
        public List<MultipleChoiceQuestionVM> multipleChoiceQuestionsList { get; set; }
        public List<int> TableQuestionPosition { get; set; }
        public List<TableQuestionVM> TableQuestionsList { get; set; }
        public List<int> AppendixQuestionPosition { get; set; }
        public List<AppendixQuestionVM> appendixQuestionsList { get; set; }
        public List<int> MapQuestionPosition { get; set; }
        public List<MapQuestionVM> mapQuestionsList { get; set; }
        public QuestionnaireVM()
        {
            _questionnaire = new Vragenlijst();
        }
        public QuestionnaireVM(Vragenlijst questionnaire)
        {
            MapQuestionPosition = new List<int>();
            mapQuestionsList = new List<MapQuestionVM>();
            TableQuestionPosition = new List<int>();
            TableQuestionsList = new List<TableQuestionVM>();
            AppendixQuestionPosition = new List<int>();
            appendixQuestionsList = new List<AppendixQuestionVM>(); 
            OpenQuestionPosition = new List<int>();
            MultipleChoiceQuestionPosition = new List<int>();
            openQuestionsList = new List<OpenQuestionVM>();
            multipleChoiceQuestionsList = new List<MultipleChoiceQuestionVM>();
            questiontypes = new List<string>();
            _questionnaire = questionnaire;
        }
        public void loadQuestions()
        {
            
            Dictionary<int, IQuestion> questionDictionary = new Dictionary<int, IQuestion>();
            LoadOpenQuestions(questionDictionary);
            LoadMapQuestions(questionDictionary);
            LoadTableQuestions(questionDictionary);
            LoadMultilpleChoiceQuestions(questionDictionary);
            LoadAppendixQuestions(questionDictionary);
            for (int i = 0; i < questionDictionary.Count; i++)
            {
                questiontypes.Add(questionDictionary[i].Type);
            }
        }
        private void LoadOpenQuestions(Dictionary<int, IQuestion> d)
        {
            var OpenQuestionsConnections = db.Openvraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Openvraag_vragenlijst ov in OpenQuestionsConnections)
            {
                int id = ov.Openvraag_ID;
                OpenQuestionVM question = new OpenQuestionVM(db.Openvraag.Find(id));
                question.ListPosition = openQuestionsList.Count();
                d.Add(ov.Positie, question);
                OpenQuestionPosition.Add(ov.Positie);
                openQuestionsList.Add(question);
            }
        }
        private void LoadMapQuestions(Dictionary<int, IQuestion> d)
        {
            var MapQuestionsConnections = db.Kaartvraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Kaartvraag_vragenlijst ov in MapQuestionsConnections)
            {
                int id = ov.Kaartvraag_ID;
                MapQuestionVM question = new MapQuestionVM(db.Kaartvraag.Find(id));
                question.ListPosition = mapQuestionsList.Count();
                d.Add(ov.Positie, question);
                MapQuestionPosition.Add(ov.Positie);
                mapQuestionsList.Add(question);

            }
        }
        private void LoadTableQuestions(Dictionary<int, IQuestion> d)
        {
            var TableQuestionsConnections = db.Tabelvraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Tabelvraag_vragenlijst ov in TableQuestionsConnections)
            {
                int id = ov.Tabelvraag_ID;
                TableQuestionVM question = new TableQuestionVM(db.Tabelvraag.Find(id));
                question.ListPosition = TableQuestionsList.Count();
                d.Add(ov.Positie, question);
                TableQuestionPosition.Add(ov.Positie);
                TableQuestionsList.Add(question);
            }

        }
        private void LoadMultilpleChoiceQuestions(Dictionary<int, IQuestion> d)
        {
            var MultipleChoiceQuestionsConnections = db.Meerkeuzevraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Meerkeuzevraag_vragenlijst ov in MultipleChoiceQuestionsConnections)
            {
                int id = ov.Meerkeuzevraag_ID;
                MultipleChoiceQuestionVM question = new MultipleChoiceQuestionVM(db.Meerkeuzevraag.Find(id));
                question.ListPosition = multipleChoiceQuestionsList.Count;
                d.Add(ov.Positie, question);
                MultipleChoiceQuestionPosition.Add(ov.Positie);
                multipleChoiceQuestionsList.Add(question);
            }

        }
        private void LoadAppendixQuestions(Dictionary<int, IQuestion> d)
        {
            var AppendixQuestionsConnections = db.Bijlagevraag_vragenlijst.Where(q => q.Vragenlijst_ID == ID).ToList();
            foreach (Bijlagevraag_vragenlijst ov in AppendixQuestionsConnections)
            {
                int id = ov.Bijlagevraag_ID;
                AppendixQuestionVM question = new AppendixQuestionVM(db.Bijlagevraag.Find(id));
                question.ListPosition = appendixQuestionsList.Count();
                d.Add(ov.Positie, question);
                AppendixQuestionPosition.Add(ov.Positie);
                appendixQuestionsList.Add(question);
            }

        }
    }
}