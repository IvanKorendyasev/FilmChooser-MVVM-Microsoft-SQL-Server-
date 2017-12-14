using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;


namespace FilmChooser
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public List<string> Firm { get; set; }
        public List<string> Type { get; set; }
        public List<int> ISO { get; set; }
        private string _selectedFirm;
        public string SelectedFirm
        {
            get { return _selectedFirm; }
            set
            {
                _selectedFirm = value;
            }
        }

        private string _selectedType;
        public string SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
            }
        }

        private int _selectedISO;
        public int SelectedISO
        {
            get { return _selectedISO; }
            set
            {
                _selectedISO = value;
            }
        }
        public int MinCost { get; set; }
        public int MaxCost { get; set; }
        public BL.FileProcessing FP = new BL.FileProcessing();
        public bool _canExecute = true;
        public List<BL.Film> AllFilms;
        public List<string> stringsToView { get; set; }
        public float Temperature { get; set; }
        public string CityName { get; set; }
        NLogProject.LogMaker Logger = new NLogProject.LogMaker();

        public MainWindowViewModel()
        {
            SelectedFirm = "Не выбрано";
            SelectedType = "Не выбрано";

            this.Firm = new List<string>();
            Firm.Add("Не выбрано");
            Firm.Add("Ilford");
            Firm.Add("Kodak");
            Firm.Add("Fujifilm");
            Firm.Add("Foma");

            this.ISO = new List<int>();
            ISO.Add(0);
            ISO.Add(50);
            ISO.Add(100);
            ISO.Add(200);
            ISO.Add(400);
            ISO.Add(800);

            this.Type = new List<string>();
            Type.Add("Не выбрано");
            Type.Add("Ч/б");
            Type.Add("Цветная");

            AllFilms = FP.FilmsFromDB;
            stringsToView = MakeStrings(AllFilms);

            Temperature = FP.Temperature;
            CityName = FP.City;

            Logger.LogInfo("Программа запущена");

        }


        public List<string> MakeStrings(List<BL.Film>Films) //создает список строк с пленками для вывода на экран
        {
            stringsToView = new List<string>();
            foreach (var item in Films)
            {
                stringsToView.Add(item.Firm + " " + item.Name + " " + item.ISO + " "+ "Цена: " + item.Cost + " Рублей");
            }
            return stringsToView;
        }



        public void ApplyFilters() //фильтровалка по введенным параметрам
        {

            IEnumerable<BL.Film> query = AllFilms;
            if (SelectedFirm != "Не выбрано")
            {
                query = query.Where(film => film.Firm == SelectedFirm);
            }
            if (SelectedType != "Не выбрано")
            {
                query = query.Where(film => film.Type == SelectedType);
            }
            if (SelectedISO != 0)
            {
                query = query.Where(film => film.ISO == SelectedISO);
            }
            if (MaxCost != 0)
            {
                query = query.Where(film => film.Cost <= MaxCost);
            }
            if (MinCost != 0)
            {
                query = query.Where(film => film.Cost >= MinCost);
            }
            stringsToView = MakeStrings(query.ToList()); 
            DoPropertyChanged("stringsToView");
            Logger.LogInfo("Всё ок, просто фильтры применил");
        }




        public event PropertyChangedEventHandler PropertyChanged;
        public void DoPropertyChanged(String Name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(Name));
            }
        }



        private ICommand _clickCommand1;
        public ICommand ClickCommand1 //команда кнопки, которая открывает браузер с сайтом магазина
        {
            set
            {
                FP.GoToSite();
            }
            get
            {

                return _clickCommand1 ?? (_clickCommand1 = new CommandHandler(() => FP.GoToSite(), _canExecute));
            }
        }



        private ICommand _clickCommand2;
        public ICommand ClickCommand2 //комманда кнопки, которая применяет фильтры
        {
            set
            {
                ApplyFilters();
            }
            get
            {

                return _clickCommand2 ?? (_clickCommand2 = new CommandHandler(() => ApplyFilters(), _canExecute));
            }
        }



        public class CommandHandler : ICommand
       {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }

      }


    }
}
