using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace DotNetHiringTest.Models
{
	[KnownType ( typeof ( Translation ) )]
	[DataContract]
	public class Translation
	{
		[DataMember]
		public string LanguageCode { get; set; }

		[DataMember]
		public string Language { get; set; }

		[DataMember]
		public string Value { get; set; }

		public Translation ( string languageCode , string language , string Value )
		{
			this.LanguageCode = languageCode;

			this.Language = language;

			this.Value = Value;
		}
	}
}