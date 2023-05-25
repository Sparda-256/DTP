using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;

namespace DTPRegistrationApp
{
    public partial class MainWindow : Window
    {
        private List<DTPOccurrence> _dtpOccurrences;

        public MainWindow()
        {
            InitializeComponent();
            LoadDTPOccurrences();
            DTPListBox.ItemsSource = _dtpOccurrences.OrderBy(dtp => dtp.Date);
        }
        private void AddDTPButton_Click(object sender, RoutedEventArgs e)
        {
            var addDTPWindow = new AddDTPWindow();
            addDTPWindow.ShowDialog();

            if (addDTPWindow.DialogResult.HasValue && addDTPWindow.DialogResult.Value)
            {
                var newDTPOccurrence = addDTPWindow.DTPOccurrence;

                _dtpOccurrences.Add(newDTPOccurrence);
                DTPListBox.ItemsSource = _dtpOccurrences.OrderBy(dtp => dtp.Date);

                SaveDTPOccurrences();
            }
        }
        private void EditDTPButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDTPOccurrence = DTPListBox.SelectedItem as DTPOccurrence;

            if (selectedDTPOccurrence != null)
            {
                var editDTPWindow = new EditDTPWindow(selectedDTPOccurrence);
                editDTPWindow.ShowDialog();
                if (editDTPWindow.DialogResult.HasValue && editDTPWindow.DialogResult.Value)
                {
                    selectedDTPOccurrence.Date = editDTPWindow.DTPOccurrence.Date;
                    selectedDTPOccurrence.Location = editDTPWindow.DTPOccurrence.Location;
                    selectedDTPOccurrence.Drivers = editDTPWindow.DTPOccurrence.Drivers;
                    selectedDTPOccurrence.LicensePlates = editDTPWindow.DTPOccurrence.LicensePlates;
                    DTPListBox.Items.Refresh();
                    SaveDTPOccurrences();
                }
            }
        }

        private void DeleteDTPButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedDTPOccurrence = DTPListBox.SelectedItem as DTPOccurrence;

            if (selectedDTPOccurrence != null)
            {
                _dtpOccurrences.Remove(selectedDTPOccurrence);
                DTPListBox.ItemsSource = _dtpOccurrences.OrderBy(dtp => dtp.Date);
                SaveDTPOccurrences();
            }
        }
        private void LoadDTPOccurrences()
        {
            try
            {
                var jsonString = File.ReadAllText("dtp.json");
                _dtpOccurrences = JsonSerializer.Deserialize<List<DTPOccurrence>>(jsonString);
            }
            catch (Exception)
            {
                _dtpOccurrences = new List<DTPOccurrence>();
            }
        }

        private void SaveDTPOccurrences()
        {
            var jsonString = JsonSerializer.Serialize(_dtpOccurrences);
            File.WriteAllText("dtp.json", jsonString);
        }
    }

    public class DTPOccurrence
    {
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string Drivers { get; set; }
        public string LicensePlates { get; set; }
    }
}