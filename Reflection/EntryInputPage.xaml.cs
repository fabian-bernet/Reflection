using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Reflection
{
    /// <summary>
    /// Interaktionslogik für EntryInputPage.xaml
    /// </summary>
    public partial class EntryInputPage : Page
    {
        public EntryInputPage()
        {
            InitializeComponent();
        }

        private void SaveEntry(object sender, RoutedEventArgs e)
        {
            Entry entryToCreate = new Entry();
            entryToCreate.DoneToday = textBoxDone.Text;
            entryToCreate.DoneWell = textBoxDoneWell.Text;
            entryToCreate.DoneBad = textBoxDoneBad.Text;
            entryToCreate.Improvements = textBoxImprovements.Text;
            entryToCreate.StarScale = (int) sliderStarScale.Value;

            List<Entry>? entriesFromJsonFile = new List<Entry> { };
            try
            {
                string entryJsonFile = File.ReadAllText("./Resources/Data/entries.json");
                entriesFromJsonFile = JsonConvert.DeserializeObject<List<Entry>>(entryJsonFile);

                if (entriesFromJsonFile is not null)
                {
                    entriesFromJsonFile.Add(entryToCreate);
                    string convertedJsonEntries = JsonConvert.SerializeObject(entriesFromJsonFile, Formatting.Indented);
                    File.WriteAllText("./Resources/Data/entries.json", convertedJsonEntries);
                    ShowMessage("Der Eintrag wurde erfolgreich gespeichert!");
                }
                else
                {
                    ShowMessage("Beim Lesen von \"entries.json\" ist ein Fehler aufgetreten.");
                }
            }
            catch (Exception exception)
            {
                ShowMessage("Es ist ein Fehler aufgetreten:\n" + exception.Message);
            }

            ShowMainPage(sender, e);
        }

        private void ShowMainPage(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainPage());
            NavigationService.RemoveBackEntry();
        }

        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}
