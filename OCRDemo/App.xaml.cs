using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DataPagesDemo
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent ();

			MainPage = new NavigationPage(new OCRItemsDataPage())
            {
                BarBackgroundColor = Color.FromHex("2196F3"),
                BarTextColor = Color.White
            };
		}
	}
}

