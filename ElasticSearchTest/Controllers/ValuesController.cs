using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearchTest.Logic;
using ElasticSearchTest.Models;
using Microsoft.AspNetCore.Mvc;

namespace ElasticSearchTest.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		IElasticRepository _elasticRepository;
		public ValuesController(IElasticRepository elasticRepository)
		{
			_elasticRepository = elasticRepository;
		}


		// GET api/values
		[HttpGet]
		public IEnumerable<Patient> Get()
		{
			MySqlLogic mysql = new MySqlLogic();
			mysql.init();

			var list = _elasticRepository.GetAll();
			return list;
		}

		// GET api/values/5
		[HttpGet("{id}")]
		public IEnumerable<Patient> Get(string query)
		{
			var list = _elasticRepository.Find(query);
			return list;
		}

		// POST api/values
		[HttpPost]
		public void Post([FromBody] string value)
		{
			var p = new Patient
			{
				FirstName = "Ivan",
				LastName = "Troelson",
				FName = "Ivanovich",
				BirthDay = DateTime.Now,
				Gender = "male",
				PatientCard = Guid.NewGuid(),
				TelephoneNumber = "+380952204391",
				OtherTelephones = new List<AdditionalTelephoneNumber>{
						new AdditionalTelephoneNumber
						{
							 Description="HOME",
							 TelephoneNumber="+380956004700"
						}
					}
			};
			var result = _elasticRepository.Update(p);
			if (result) { }
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
			var p = new Patient
			{
				FirstName = "Ivan",
				LastName = "Troelson",
				FName = "Ivanovich",
				BirthDay = DateTime.Now,
				Gender = "male",
				PatientCard = Guid.NewGuid(),
				TelephoneNumber = "+380952204392",
				OtherTelephones = new List<AdditionalTelephoneNumber>{
						new AdditionalTelephoneNumber
						{
							 Description="HOME",
							 TelephoneNumber="+380956004700"
						}
					}
			};
			var result = _elasticRepository.Insert(p);
			if (result) { }
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
