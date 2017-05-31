using DotNetHiringTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DotNetHiringTest
{
	
	public class CountryService : ICountryService
	{
		public List<Country> GetCountries ( )
		{
			var repository = new DotNetHiringTest.Repository ( );

			return repository.Countries;
		}

		public Country GetCountryById ( int countryId )
		{
			var repository = new DotNetHiringTest.Repository ( );

			return repository.GetCountryById ( countryId );
		}

		public Country GetCountryByAlpha3Code ( string alpha3Code )
		{
			var repository = new DotNetHiringTest.Repository ( );

			return repository.GetCountryByAlpha3Code ( alpha3Code );
		}

		public List<Translation> GetTranslations ( string countryAlpha3Code , string languageCode = null )
		{
			var repository = new DotNetHiringTest.Repository ( );

			return repository.GetTranslations ( countryAlpha3Code , languageCode );
		}
	}
}
