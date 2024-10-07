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
            CheckTodaysEntry();
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
                this.Entries = entriesFromJsonFile.OrderByDescending(entry => DateTime.Parse(entry.Date)).ToList();
                entriesListView.ItemsSource = Entries;
            }
            else
            {
                ShowMessage("Beim Lesen von \"entries.json\" ist ein Fehler aufgetreten.");
            }
        }

        private void CheckSelectEntry(object sender, SelectionChangedEventArgs e)
        {
            Entry selectedEntry = (Entry) entriesListView.SelectedItem;
            buttonShowEntry.IsEnabled = (selectedEntry != null);
        }

        private void ShowEntryPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EntryPage());
        }

        private void ShowEntryPageWithEntry(object sender, RoutedEventArgs e)
        {
            Entry selectedEntry = (Entry) entriesListView.SelectedItem;
            NavigationService.Navigate(new EntryPage(selectedEntry));
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        private void CheckTodaysEntry()
        {
            string date = DateOnly.FromDateTime(DateTime.Now).ToString(CultureInfo.CurrentCulture);
            if (Entries.Any(entry => entry.Date == date))
            {
                buttonCreateEntry.IsEnabled = false;
                buttonCreateEntry.ToolTip = "Der heutige Eintrag ist bereits erfasst.";
            }
        }
    }
}

