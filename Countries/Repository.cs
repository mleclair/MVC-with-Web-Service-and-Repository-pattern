using DotNetHiringTest.Logs;
using DotNetHiringTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace DotNetHiringTest
{
	/// <summary>
	/// 
	/// </summary>
	public class Repository : IRepository<List<Country>> , IDisposable
	{
		private List<Country> countries;

		/// <summary>
		/// Naturally, we could read this from web.config
		/// </summary>
		private Uri uri = new Uri ( "https://www.restcountries.eu/rest/v1/all" );

		public List<Country> Countries
		{
			get { return this.countries; }
			set { this.countries = value; }
		}

		public Repository ( )
		{
			if ( this.countries == null )
			{
				this.countries = this.GetCountries ( );
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List <Country> GetCountries ( )
		{
			try
			{
				HttpWebRequest request = WebRequest.Create ( this.uri ) as HttpWebRequest;

				if ( request.Connection == null )
				{
					Log.Append ( "No connection available" );
				}
				else
				{
					HttpWebResponse response = ( HttpWebResponse )request.GetResponse ( );

					if ( response.StatusCode == HttpStatusCode.OK )
					{
						using ( Stream stream = response.GetResponseStream ( ) )
						{
							StreamReader streamReader = new StreamReader ( stream );

							var serializer = new System.Web.Script.Serialization.JavaScriptSerializer ( );

							return serializer.Deserialize<List<Country>> ( streamReader.ReadToEnd ( ) );
						}
					}
				}
			}
			catch ( System.Exception ex )
			{
				Log.Append ( ex );

				throw ex;
			}

			return null;
		}

		/// <summary>
		/// Repository already has everything we need.
		/// </summary>
		/// <param name="countryId"></param>
		/// <returns></returns>
		public Country GetCountryById ( int countryId )
		{
			if ( this.countries == null || this.countries.Count ( ) == 0 )
			{
				this.countries = this.GetCountries ( );
			}

			return this.countries.Where ( c => c.CountryId == countryId ).FirstOrDefault ( );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="alpha3Code"></param>
		/// <returns></returns>
		public Country GetCountryByAlpha3Code ( string alpha3Code )
		{
			if ( this.countries == null || this.countries.Count ( ) == 0 )
			{
				this.countries = this.GetCountries ( );
			}

			return this.countries.Where ( c => c.Alpha3Code == alpha3Code ).FirstOrDefault ( );
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="countryAlpha3Code"></param>
		/// <param name="languageCode"></param>
		/// <returns></returns>
		public List<Translation> GetTranslations ( string countryAlpha3Code , string languageCode = null )
		{
			Country country = this.GetCountryByAlpha3Code ( countryAlpha3Code );

			List<Translation> translations = new List<Translation> ( );

			dynamic availableTranslations = country.Translations;

			if ( availableTranslations != null )
			{
				foreach ( KeyValuePair<string,object> language in availableTranslations )
				{
					var cultureInfo = new CultureInfo ( language.Key );

					if ( string.IsNullOrWhiteSpace ( languageCode ) || language.Key == languageCode )
					{
						translations.Add (
								new Translation (
									language.Key ,
										cultureInfo.DisplayName ,
											language.Value.ToString ( ) ) );
					}
				}
			}

			return translations;
		}

		public void Dispose ( )
		{
			Log.Append ( "Disposed" );

			GC.SuppressFinalize ( this );
		}
	}
}