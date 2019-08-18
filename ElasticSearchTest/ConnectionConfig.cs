using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nest;
namespace ElasticSearchTest
{
	internal class ConnectionConfig
	{
		public static Dictionary<string,string> Params { get { return getConfig(); } }

		static private Dictionary<string, string>  getConfig() {
			var dict = new Dictionary<string, string>();

			dict.Add("Uri", "http://localhost:9200");
			dict.Add("Index", "patients");
			dict.Add("Type", "patient");

			return dict; ;
		}

	}
}
