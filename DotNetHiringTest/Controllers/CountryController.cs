using DotNetHiringTest.CountryService;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DotNetHiringTest.Controllers
{
	public static class CountryController
	{
		public static List<Country> GetCountries ( )
		{
			try
			{
				using ( CountryService.CountryServiceClient client = new CountryService.CountryServiceClient ( ) )
				{
					return client.GetCountries ( );
				}
			}
			catch ( System.ServiceModel.CommunicationException ex )
			{
				//DotNetHiringTest.Logs.Log.Append ( ex );
				throw ex;
			}
			catch ( System.Exception ex )
			{
				throw ex;
			}
		}

		public static Country GetCountryById ( int countryId )
		{
			try
			{
				using ( CountryService.CountryServiceClient client = new CountryService.CountryServiceClient ( ) )
				{
					return client.GetCountryById ( countryId );
				}
			}
			catch ( System.Exception ex )
			{
				throw ex;
			}
		}

		public static Country GetCountryByAlpha3Code ( string alpha3Code )
		{
			try
			{
				using ( CountryService.CountryServiceClient client = new CountryService.CountryServiceClient ( ) )
				{
					return client.GetCountryByAlpha3Code ( alpha3Code );
				}
			}
			catch ( System.Exception ex )
			{
				throw ex;
			}
		}

		public static List<Translation> GetTranslations ( string countryAlpha3Code , string lanugageCode = null )
		{
			try
			{
				using ( CountryService.CountryServiceClient client = new CountryService.CountryServiceClient ( ) )
				{
					return client.GetTranslations ( countryAlpha3Code , lanugageCode );
				}
			}
			catch ( System.Exception ex )
			{
				throw ex;
			}
		}
	}
}