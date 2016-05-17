using System;
using System.Diagnostics;

using Xamarin.Forms;

namespace RealmXamarinSample
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private async void Create_Clicked(object sender, EventArgs e)
		{
			try
			{
				var db = new DbOperation();

				db.Clear();
				db.Create();

				string message;

				db.SelectAll(out message);
				await DisplayAlert("", message, "OK");

				db.SelectXamarin(out message);
				await DisplayAlert("", message, "OK");

				db.SelectSort(out message);
				await DisplayAlert("", message, "OK");

				db.SelectNotEmpty(out message);
				await DisplayAlert("", message, "OK");

				db.SelectGroup(out message);
				await DisplayAlert("", message, "OK");

				db.Update();
				db.SelectSort(out message);
				await DisplayAlert("", message, "OK");

				db.Delete();
				db.SelectXamarin(out message);
				await DisplayAlert("", message, "OK");
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

	}
}
