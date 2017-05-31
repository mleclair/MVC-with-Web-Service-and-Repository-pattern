using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DotNetHiringTest.Models
{
	/// <summary>
	/// Consumables of the RestCountries response
	/// </summary>
	[DataContract]
	[KnownType ( typeof ( Country ) )]
	public class Country
	{
		private int? countryId;

		[DataMember]
		public int? CountryId
		{
			get
			{
				int countryId;

				Int32.TryParse ( this.NumericCode , out countryId );

				return countryId;
			}
			set
			{
				this.countryId = value;
			}
		}

		[DataMember]
		[JsonProperty ( "name" )]
		public string Name { get; set; }

		[DataMember]
		[JsonProperty ( "alpha3Code" )]
		public string Alpha3Code { get; set; }

		[DataMember]
		[JsonProperty ( "numericCode" )]
		public string NumericCode { get; set; }

		[DataMember]
		[JsonProperty ( "languages" )]
		public List<string> Languages { get; set; }

		[DataMember]
		[JsonProperty ( "borders" )]
		public List<string> Borders { get; set; }

		[DataMember]
		[JsonProperty ( "capital" )]
		public string Capital { get; set; }

		[JsonProperty ( "translations" )]
		public object Translations { get; set; }
	}
}