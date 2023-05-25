using System;
using System.Windows;
using System.Windows.Controls;
namespace DTPRegistrationApp
{
	public partial class AddDTPWindow : Window
	{
		public DTPOccurrence DTPOccurrence { get; private set; }
		public AddDTPWindow()
		{
			InitializeComponent();
		}
		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			DTPOccurrence = new DTPOccurrence
			{
				Date = DatePicker.SelectedDate ?? DateTime.Now,
				Location = LocationTextBox.Text,
				Drivers = DriversTextBox.Text,
				LicensePlates = LicensePlatesTextBox.Text
			};
			DialogResult = true;
			Close();
		}
		private void CancelButton_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}