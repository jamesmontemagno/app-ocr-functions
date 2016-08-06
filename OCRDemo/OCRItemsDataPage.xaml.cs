using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using Acr.UserDialogs;
using Xamarin.Forms.Pages.Azure;
using Xamarin.Forms.Pages;
using System.ComponentModel;

namespace DataPagesDemo
{
	public partial class OCRItemsDataPage : Xamarin.Forms.Pages.ListDataPage
	{
		public OCRItemsDataPage()
		{
			InitializeComponent ();


            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Refresh",
                Command = new Command(() =>
                {
                    if (DataSource.IsLoading)
                        return;

                    ((INotifyPropertyChanged)((BaseDataSource)DataSource)).PropertyChanged -= OCRItemsDataPage_PropertyChanged;

                    var current = (AzureEasyTableSource)((AzureDataSource)DataSource).Source;
                    this.DataSource = new AzureDataSource
                    {
                        Source = new AzureEasyTableSource
                        {
                            TableName = current.TableName,
                            Uri = current.Uri
                        }
                    };
                })
            });
            
            
		}


        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if(propertyName == nameof(DataSource))
            {
                ((INotifyPropertyChanged)((BaseDataSource)DataSource)).PropertyChanged += OCRItemsDataPage_PropertyChanged;
               
            }

            base.OnPropertyChanged(propertyName);

        }

        private void OCRItemsDataPage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(DataSource.IsLoading))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (DataSource.IsLoading)
                        UserDialogs.Instance.ShowLoading("Loading...");
                    else
                        UserDialogs.Instance.HideLoading();
                });
            }
        }
    }
}

