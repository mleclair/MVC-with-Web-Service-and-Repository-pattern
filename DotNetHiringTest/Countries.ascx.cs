using DotNetHiringTest.CountryService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetHiringTest.Controllers;

namespace DotNetHiringTest
{
	public partial class Countries : System.Web.UI.UserControl
	{
		protected void Page_Load( object sender , EventArgs e )
		{
			if ( !IsPostBack)
			{
				List<Country> countries = CountryController.GetCountries ( );

				if ( countries != null && countries.Count ( ) > 0 )
				{
					CountryDropDownLabel.Visible = true;

					CountryDropDownList.Visible = true;

					CountryDropDownList.Items.Add ( new ListItem ( string.Empty , ( -1 ).ToString ( ) ) );

					foreach ( Country country in countries )
					{
						CountryDropDownList.Items.Add (
							new ListItem (
								country.Name ,
								country.Alpha3Code
							)
						);
					}
				}
			}
		}

		protected void CountrySelectionChange( object sender , EventArgs e )
		{
			if ( CountryDropDownList.SelectedIndex > 0
					&& !string.IsNullOrWhiteSpace ( CountryDropDownList.SelectedValue ) )
			{
				DisplayLanguageDropDownList.Items.Clear ( );

				DisplayLanguageDropDownList.Items.Add( string.Empty );

				//int countryId;

				//if ( Int32.TryParse ( CountryDropDownList.SelectedValue , out countryId ) )
				//{
					List<Country> countries = CountryController.GetCountries ( );

					//Country selectedCountry = client.GetCountryById ( countryId );

					Country selectedCountry = CountryController.GetCountryByAlpha3Code ( CountryDropDownList.SelectedValue );

					// Display selected country details
					if ( selectedCountry != null )
					{
						DisplayLanguageLabel.Visible = true;

						DisplayLanguageDropDownList.Visible = true;

						List<Translation> translations
								= CountryController.GetTranslations ( CountryDropDownList.SelectedValue );

						foreach ( Translation translation in translations )
						{
							var cultureInfo = new CultureInfo ( translation.LanguageCode );

							DisplayLanguageDropDownList.Items.Add (
								new ListItem ( cultureInfo.DisplayName , translation.LanguageCode )
							);
						}

						this.AddTableRows ( selectedCountry , countries );
					}
				//}
			}
		}

		protected void DisplayLanguageSelectionChange ( object sender , EventArgs e )
		{
			if ( DisplayLanguageDropDownList.SelectedIndex > 0
					&& !string.IsNullOrWhiteSpace ( DisplayLanguageDropDownList.SelectedValue )
						&& DisplayLanguageDropDownList.SelectedValue != "all"
							&& DisplayLanguageDropDownList.SelectedValue != "en" )
			{
				//int countryId;

				//if ( Int32.TryParse ( CountryDropDownList.SelectedValue , out countryId ) )
				//{
					List<Country> countries = CountryController.GetCountries ( );

					Country selectedCountry = CountryController.GetCountryByAlpha3Code ( CountryDropDownList.SelectedValue );

					for ( int i = SelectedCountryTable.Rows.Count - 1 ; i > -1 ; i-- )
					{
						SelectedCountryTable.Rows.Remove ( SelectedCountryTable.Rows [ i ] );
					}

					this.AddTableRows ( selectedCountry , countries );
				//}
			}
		}

		protected void AddTableRows ( Country country , List<Country> countries )
		{
			string language = string.Empty;

			if ( DisplayLanguageDropDownList != null && DisplayLanguageDropDownList.SelectedIndex > 0 )
			{
				language = DisplayLanguageDropDownList.SelectedValue;
			}

			if ( country != null )
			{
				TableRow tr = new TableRow ( );

				SelectedCountryTable.Rows.Add ( tr );

				tr.Controls.Add ( new TableCell ( ) { Text = "Country Name:" , Width = 160 } );

				tr.Controls.Add ( new TableCell ( ) { Text = country.Name } );

				// Country name in selected language
				List<Translation> translations
							= CountryController.GetTranslations ( CountryDropDownList.SelectedValue );

				Translation translation = translations.Where ( t => t.LanguageCode == language )
													  .FirstOrDefault ( );

				if ( translation != null && !string.IsNullOrWhiteSpace ( translation.Value ) )
				{
					tr = new TableRow ( );

					SelectedCountryTable.Rows.Add ( tr );

					tr.Controls.Add ( new TableCell ( ) { Text = "Translated Country Name:" } );

					tr.Controls.Add ( new TableCell ( ) { Text = translation.Value } );
				}

				// Capital name in English
				tr = new TableRow ( );

				SelectedCountryTable.Rows.Add ( tr );

				tr.Controls.Add ( new TableCell ( ) { Text = "Capital:" } );

				tr.Controls.Add ( new TableCell ( ) { Text = country.Capital } );

				// Spoken Languages
				tr = new TableRow ( );

				SelectedCountryTable.Rows.Add ( tr );

				tr.Controls.Add ( new TableCell ( ) { Text = "Spoken Languages:" } );

				TableCell td = new TableCell ( );

				tr.Controls.Add ( td );

				ListBox spokenLanguagesListBox = new ListBox ( );

				td.Controls.Add ( spokenLanguagesListBox );

				spokenLanguagesListBox.ID = "SpokenLanguagesListBox";

				spokenLanguagesListBox.Items.Clear ( );

				spokenLanguagesListBox.Enabled = false;

				foreach ( string spokenLanguage in country.Languages )
				{
					var cultureInfo = new CultureInfo ( spokenLanguage );

					ListItem languageItem = spokenLanguagesListBox.Items.FindByValue ( spokenLanguage );

					if ( languageItem == null )
					{
						spokenLanguagesListBox.Items.Add (
							new ListItem ( cultureInfo.EnglishName , spokenLanguage )
						);
					}
					else
					{
						languageItem.Text = cultureInfo.NativeName;
					}
				}

				// Bordering countries
				tr = new TableRow ( );

				SelectedCountryTable.Rows.Add ( tr );

				tr.Controls.Add ( new TableCell ( ) { Text = "Bordering Countries:" } );

				td = new TableCell ( );

				tr.Controls.Add ( td );

				ListBox neighboringCountriesListBox = new ListBox ( );

				td.Controls.Add ( neighboringCountriesListBox );

				neighboringCountriesListBox.ID = "NeighboringCountriesListBox";

				neighboringCountriesListBox.Style.Add ( HtmlTextWriterStyle.Width , "240px" );

				neighboringCountriesListBox.Style.Add ( HtmlTextWriterStyle.Height , "80px" );

				neighboringCountriesListBox.Items.Clear ( );

				neighboringCountriesListBox.Enabled = false;

				if ( countries != null )
				{
					// Country name in selected language
					foreach ( string borderingCountryAlpha3 in country.Borders )
					{
						Country borderCountry
								= countries.Where ( c => c.Alpha3Code == borderingCountryAlpha3 )
										   .FirstOrDefault ( );

						if ( borderCountry != null )
						{
							string countryName = borderCountry.Name;

							if ( !string.IsNullOrWhiteSpace ( language ) && language != "en" )
							{
								List<Translation> borderingCountryTanslations
													= CountryController.GetTranslations (
														borderingCountryAlpha3 ,
															language );

								Translation borderingCountryTranslation = borderingCountryTanslations.First ( );

								if ( borderingCountryTranslation != null )
								{
									countryName = borderingCountryTranslation.Value;
								}
							}

							ListItem borderingItem = neighboringCountriesListBox.Items
															.FindByValue ( borderingCountryAlpha3 );

							if ( borderingItem == null )
							{
								neighboringCountriesListBox.Items.Add (
									new ListItem ( borderingCountryAlpha3 , countryName )
								);
							}
							else
							{
								borderingItem.Text = countryName;
							}
						}
					}
				}
			}
		}
	}
}