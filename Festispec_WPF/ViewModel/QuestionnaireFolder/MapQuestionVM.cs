using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Festispec_WPF.Model.UnitOfWork;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace Festispec_WPF.ViewModel.QuestionnaireFolder
{
    public class MapQuestionVM :ViewModelBase, IQuestion
    {
        private UnitOfWork UOW;
        private Kaartvraag mapQuestionModel;
        private int position;
        private string questiontype;
        //constructor
        public MapQuestionVM()
        {
            UOW = new ViewModelLocator().UOW;
            mapQuestionModel = new Kaartvraag();
            questiontype = "Kaartvraag";
        }
        public string QuestionType { get => questiontype; set => questiontype = value; }
        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
                RaisePropertyChanged();
            }
        }
        public string Question { get => mapQuestionModel.Vraag; set => mapQuestionModel.Vraag = value; }
        
        public BitmapImage Picture
        {
            get
            {
                if (mapQuestionModel.FileBytes != null)
                {

                    using (var ms = new MemoryStream(mapQuestionModel.FileBytes))
                    {
                        var image = new BitmapImage();
                        image.BeginInit();
                        image.CacheOption = BitmapCacheOption.OnLoad; // here
                        image.StreamSource = ms;
                        image.EndInit();
                        return image;
                    }
                }
                return null;

            }
            set
            {
                byte[] data;
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                if (value != null)
                {
                    encoder.Frames.Add(BitmapFrame.Create(value));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);
                        data = ms.ToArray();
                        mapQuestionModel.FileBytes = data;
                        RaisePropertyChanged("Picture");
                    }
                }


            }
        }

        public void toDatabase(int questionnaireId)
        {
            UOW.Context.Kaartvraag.Add(mapQuestionModel);
            Kaartvraag_vragenlijst Link = new Kaartvraag_vragenlijst();
            Link.Kaartvraag_ID = mapQuestionModel.ID;
            Link.Vragenlijst_ID = questionnaireId;
            Link.Positie = Position;
            UOW.Context.Kaartvraag_vragenlijst.Add(Link);
            try
            {
                UOW.Complete();
            }
            catch
            {
                MessageBox.Show("Er is iets fout gegaan", "Fout bij invoeren velden",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
