using DotNetHiringTest.Models;
using System;
using System.Collections.Generic;

namespace DotNetHiringTest
{
	/// <summary>
	/// 
	/// </summary>
	public interface IRepository<TEntity> : IDisposable where TEntity : class
	{
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		List<Country> GetCountries ( );

		/// <summary>
		/// 
		/// </summary>
		/// <param name="countryId"></param>
		/// <returns></returns>
		Country GetCountryById ( int countryId );
	}
}