using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using DotNetHiringTest.Models;
using System.Reflection;

namespace DotNetHiringTest
{
	//[ServiceKnownType ( typeof ( RestCountries ) )]
	[ServiceKnownType ( typeof ( Country ) )]
	[ServiceKnownType ( typeof ( Translation ) )]
	[ServiceKnownType ( typeof ( List<string[]> ) )]
	[ServiceContract]
	public interface ICountryService
	{
		[OperationContract]
		List<Country> GetCountries ( );

		[OperationContract]
		Country GetCountryById ( int countryId );

		[OperationContract]
		Country GetCountryByAlpha3Code ( string alpha3Code );

		[OperationContract]
		List<Translation> GetTranslations ( string countryAlpha3Code , string lanuageCode = null );
	}
}
