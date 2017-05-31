using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace DotNetHiringTest.Logs
{
	public class Log
	{
		private static string file = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "\\Logs\\log.txt";

		public static void Append ( string text )
		{
			using ( StreamWriter writer = File.AppendText ( file ) )
			{
				writer.Write ( "\r\nLog Entry : " );
				writer.WriteLine ( "{0} {1}" , DateTime.Now.ToLongTimeString ( ) ,
					DateTime.Now.ToLongDateString ( ) );
				writer.WriteLine ( "  :" );
				writer.WriteLine ( "  :{0}" , text );
				writer.WriteLine ( "-------------------------------" );
			}
		}

		public static void Append ( System.Exception ex )
		{
			using ( StreamWriter writer = File.AppendText ( file ) )
			{
				writer.Write ( "\r\nException : " );
				writer.WriteLine ( "{0} {1}" , DateTime.Now.ToLongTimeString ( ) ,
					DateTime.Now.ToLongDateString ( ) );
				writer.WriteLine ( "  :" );
				writer.WriteLine ( "  :{0}" , ex.Message );
				writer.WriteLine ( "  :" );
				writer.WriteLine ( "  :{0}" , ex.InnerException );
				writer.WriteLine ( "-------------------------------" );
			}
		}
	}
}