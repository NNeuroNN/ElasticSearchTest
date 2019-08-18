using ElasticsearchCRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchTest.Models
{
	public class DummyContext
	{

	
		public IEnumerable<object> Get()
		{
			return null;
		}
	}

	public class Dummy
	{
		public long Id { get; set; }
		public string  Name { get; set; }
		public DateTimeOffset Birth { get; set; }
	}
}
