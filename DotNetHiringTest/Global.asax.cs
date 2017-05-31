using DotNetHiringTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace DotNetHiringTest
{
	public class Global : HttpApplication
	{
		void Application_Start ( object sender , EventArgs e )
		{
			// Code that runs on application startup
			BundleConfig.RegisterBundles ( BundleTable.Bundles );

			AuthConfig.RegisterOpenAuth ( );

			RouteConfig.RegisterRoutes ( RouteTable.Routes );

			//GlobalConfiguration.Configure ( WebApiConfig.Register );
		}

		void Application_End ( object sender , EventArgs e )
		{
			//  Code that runs on application shutdown

		}

		void Application_Error ( object sender , EventArgs e )
		{
			// Code that runs when an unhandled error occurs

		}

		//public static class WebApiConfig
		//{
		//	public static void Register ( HttpConfiguration config )
		//	{
		//		config.Routes.MapHttpRoute (
		//			name: "country" ,
		//			routeTemplate: "api/{controller}"
		//		);

		//		config.MapHttpAttributeRoutes ( );
		//	}
		//}
	}
}
