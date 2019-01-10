using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Globalization;

namespace ComboBoxWithSearch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ViewModel()
        {
            var dict = new Dictionary<string, string>();
            AllCountries = FilteredCountries = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(o => new RegionInfo(o.Name).EnglishName).Distinct().ToList();
        }
        private List<string> allCountries;
        public List<string> AllCountries
        {
            get
            {
                return allCountries;
            }
            set
            {
                allCountries = value;
                OnPropertyChanged("AllCountries");
            }
        }
        private List<string> filteredCountries;
        public List<string> FilteredCountries
        {
            get
            {
                return filteredCountries;
            }
            set
            {
                filteredCountries = value;
                filteredCountries.Sort();
                OnPropertyChanged("FilteredCountries");
            }
        }
        private string filterString;
        public string FilterString
        {
            get
            {
                return filterString;
            }
            set
            {
                filterString = value;
                FilteredCountries = AllCountries.Where(x => x.ToLower().StartsWith(filterString.ToLower())).ToList();
                OnPropertyChanged("FilterString");
            }
        }
        private string selectedCountry;
        public string SelectedCountry
        {
            get
            {
                return selectedCountry;
            }
            set
            {
                selectedCountry = value;
                OnPropertyChanged("SelectedCountry");
            }
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SearchTargetProperty =
        DependencyProperty.RegisterAttached("SearchTarget", typeof(string), typeof(MainWindow), new PropertyMetadata(default(string)));
    }
}
