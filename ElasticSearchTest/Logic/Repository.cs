using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ElasticSearchTest.Models;
using Nest;
namespace ElasticSearchTest.Logic
{
	public class ELasticRepository :IElasticRepository
	{
		ElasticClient _client;

		public ELasticRepository()
		{
			var index = "patients";

			var settings = new ConnectionSettings(new Uri(ConnectionConfig.Params["Uri"]))
				.DefaultIndex(index)
				.PrettyJson()
				.DisableDirectStreaming();

			_client = new ElasticClient(settings);

			if (_client.Indices.Exists(Indices.Parse(index)).Exists)
			{
				_client.Indices.Create(index, c => c
				.Map<Patient>(m => m.AutoMap().AutoMap<AdditionalTelephoneNumber>()));
			}

		}

		public IEnumerable<Patient> GetAll()
		{
			return _client.Search<Patient>(s => s.Index("patients")).Documents;
		}

		public IEnumerable<Patient> Find(string query)
		{

			query = "Troelson";
			return _client.Search<Patient>(s => s
			.Index("patients")
		    .Query(q =>
			   //Проверка Имени
			   q.Match(m => m
				   .Field(f => f.FirstName)
					   .Query(query))

			//Проверка Фамилии
			|| q.Match(m => m
				  .Field(f => f.LastName)
					  .Query(query))

			////Проверка Отчества
			//|| q.Match(m => m
			//	  .Field(f => f.FName)
			//		  .Query(query))

			////Проверка Даты рождения
			//|| q.Match(m => m
			//	  .Field(f => f.BirthDay)
			//		  .Query(query))

			////Проверка Номера телефона
			//|| q.Match(m => m
			//	  .Field(f => f.TelephoneNumber)
			//		  .Query(query))

			//////Проверка Других номеров
			////&& q.Match(m => m
			////	  .Field(f => f.OtherTelephones)
			////		  .Query(query))

			)).Documents;
		}

		public bool Insert(Patient patient)
		{
			var result = _client.Index(patient, i => i
					.Index("patients")
					.Id(patient.PatientCard)
					.Refresh(Elasticsearch.Net.Refresh.True)
					);
			return (result.Result == Result.Created);
		}


		public bool Update (Patient patient)
		{
			var result = _client.Index(patient, i => i
					.Index("patients")
					.Id(patient.PatientCard)
					.Refresh(Elasticsearch.Net.Refresh.True)
					);
			return (result.Result == Result.Updated);
		}
		public  void  DO()
		{
			foreach (var patient in Patient.Defaults())
			{
				var result = _client.Index(patient, i => i
					.Index("patients")
					.Id(patient.PatientCard)
					.Refresh(Elasticsearch.Net.Refresh.True)
					);
				var id = result.ApiCall.HttpMethod;
			}
			
		}
	}

	public interface IElasticRepository
	{
		bool Update(Patient patient);
		bool Insert(Patient patient);
		IEnumerable<Patient> Find(string query);
		IEnumerable<Patient> GetAll();
	}
}
