using Newtonsoft.Json;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace Reflection
{
    /// <summary>
    /// Interaktionslogik für EntryPage.xaml
    /// </summary>
    public partial class EntryPage : Page
    {
        public EntryPage()
        {
            InitializeComponent();
        }

        public EntryPage(Entry entry)
        {
            InitializeComponent();
            FillInteractionFields(entry);
            DisableEditing();
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

        private void FillInteractionFields(Entry entry)
        {
            textBoxDone.Text = entry.DoneToday;
            textBoxDoneWell.Text = entry.DoneWell;
            textBoxDoneBad.Text = entry.DoneBad;
            textBoxImprovements.Text = entry.Improvements;
            sliderStarScale.Value = entry.StarScale;
        }
        
        private void DisableEditing()
        {
            textBoxDone.IsEnabled = false;
            textBoxDoneWell.IsEnabled = false;
            textBoxDoneBad.IsEnabled = false;
            textBoxImprovements.IsEnabled = false;
            sliderStarScale.IsEnabled = false;
            buttonSaveEntry.IsEnabled = false;
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
