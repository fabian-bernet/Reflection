using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
            }
            else
            {
                ShowMessage("Beim Lesen von \"entries.json\" ist ein Fehler aufgetreten.");
            }        
        }
        
        private void ShowMessage(string message)
        {
            MessageBox.Show(message);
        }
    }
}

