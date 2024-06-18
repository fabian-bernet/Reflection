using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Newtonsoft.Json;

namespace Reflection
{
    /// <summary>
    /// Interaktionslogik für MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public List<Entry> Entries { get; set; } = new List<Entry> { };

        public MainPage()
        {
            InitializeComponent();
            DataContext = this;
            LoadEntries();
            if (IsEntryForTodayExistent()) DisableCreateEntryButton();
        }

        private void LoadEntries()
        {
            List<Entry>? entriesFromJsonFile = new List<Entry> { };

            try
            {
                using (StreamReader streamReader = new StreamReader("./Resources/Data/entries.json"))
                {
                    string entryJsonFile = streamReader.ReadToEnd();
                    entriesFromJsonFile = JsonConvert.DeserializeObject<List<Entry>>(entryJsonFile);
                }
            }
            catch (Exception exception)
            {
                ShowMessage("Es ist ein Fehler aufgetreten:\n" + exception.Message);
            }

            if (entriesFromJsonFile is not null)
            {
                this.Entries = entriesFromJsonFile.OrderByDescending(entry => entry.Date).ToList();
                entriesListView.ItemsSource = Entries;
            }
            else
            {
                ShowMessage("Beim Lesen von \"entries.json\" ist ein Fehler aufgetreten.");
            }        
        }

        private void ShowEntryInputPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EntryInputPage());
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private bool IsEntryForTodayExistent()
        {
            string date = DateOnly.FromDateTime(DateTime.Now).ToString(CultureInfo.CurrentCulture);
            return Entries.Any(entry => entry.Date == date);
        }

        private void DisableCreateEntryButton()
        {
            buttonCreateEntry.IsEnabled = false;
            buttonCreateEntry.ToolTip = "Der heutige Eintrag ist bereits erfasst.";
        }
    }
}

