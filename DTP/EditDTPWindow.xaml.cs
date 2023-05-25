using System;
using System.Windows;
using System.Windows.Controls;

namespace DTPRegistrationApp
{
	public partial class EditDTPWindow : Window
	{
		public DTPOccurrence DTPOccurrence { get; private set; }

		public EditDTPWindow(DTPOccurrence dtpOccurrence)
		{
			InitializeComponent();

			DatePicker.SelectedDate = dtpOccurrence.Date;
			LocationTextBox.Text = dtpOccurrence.Location;
			DriversTextBox.Text = dtpOccurrence.Drivers;
			LicensePlatesTextBox.Text = dtpOccurrence.LicensePlates;
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			DTPOccurrence = new DTPOccurrence
			{
				Date = DatePicker.SelectedDate ?? DateTime.Now,
				Location = LocationTextBox.Text,
				Drivers = DriversTextBox.Text,
				LicensePlates = LicensePlatesTextBox.Text
			};

			DialogResult = true;

			// Закрываем окно
			Close();
		}

		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}