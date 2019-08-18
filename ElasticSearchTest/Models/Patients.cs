using Nest;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ElasticSearchTest.Models
{
	public abstract class Document
	{
	}

	//[ElasticsearchType(RelationName = "Patient")]
	public class Patient :Document
	{	
		[Key]
		public Guid PatientCard { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string FName { get; set; }
		public string Gender { get; set; }

		[RegularExpression(@"^[+]*[(]{0,1}[0-9]{1,4}[)]{0,1}[-\s\./0-9]*$")]
		public string TelephoneNumber { get; set; }

		public DateTime BirthDay { get; set; }
		[Column("IsActive", TypeName = "TINYINT")]
		[DefaultValue(true)]
		public bool IsActive { get; set; }

		[Object]
		public List<AdditionalTelephoneNumber> OtherTelephones { get; set; }

		public Patient()
		{
			setGuid();
		}

		private void setGuid()
		{
			PatientCard = Guid.Parse(Guid.NewGuid().ToString().Replace("-", ""));
		}

		public static List<Patient> Defaults() {

			var male = new Gender { Type = "Male" };
			var female = new Gender { Type = "Female" };
			var other = new Gender { Type = "Other" };
			var PatientsList = new List<Patient>
			{
				new Patient {
					FirstName ="Ivan",
					LastName ="Ivanov",
					FName ="Ivanovich",
					BirthDay =DateTime.Now,
					Gender ="male",
					//PatientCard =Guid.NewGuid(),
					TelephoneNumber ="+380952204391",
					OtherTelephones =new List<AdditionalTelephoneNumber>{
						new AdditionalTelephoneNumber
						{
							 Description="HOME",
							 TelephoneNumber="+380956004700"
						}
					} }, 

				new Patient {
					FirstName ="Dmitriy",
					LastName ="", FName="Olegovich",
					BirthDay =DateTime.Now,
					Gender ="male",
					//PatientCard =Guid.NewGuid(),
					TelephoneNumber ="+380952204391"},

				new Patient {
					FirstName ="Ivanna",
					LastName ="Ivanova",
					FName ="Ivanovna",
					BirthDay =DateTime.Now,
					Gender ="female",
					//PatientCard=Guid.NewGuid(),
					TelephoneNumber ="+380952204393"},

				new Patient {
					FirstName ="Maria",
					LastName ="Pticina",
					FName ="Olegovna",
					BirthDay =DateTime.Now,
					Gender ="female",
				//	PatientCard =Guid.NewGuid(),
					TelephoneNumber ="+380952204394"},

				new Patient {
					FirstName ="Vitaliy",
					LastName ="Troelson",
					FName ="Viktorovich",
					BirthDay =DateTime.Now,
					Gender ="male",
					//PatientCard=Guid.NewGuid(),
					TelephoneNumber ="+380952204392" },

				new Patient {FirstName="Anastasia",
					LastName ="Vinegret",
					FName ="Michailovna",
					BirthDay =DateTime.Now,
					Gender ="other",
					//PatientCard =Guid.NewGuid(),
					TelephoneNumber =""}

			};
			return PatientsList;
		}
	}

	public class Gender: Document
	{
		[Text(Name = "Type")]
		public string Type { get; set; }
	}
	public class AdditionalTelephoneNumber : Document
	{	public int Id { get; set; }
		public Patient Patient { get; set; }
		[Text(Name = "telephone")]
		public string TelephoneNumber { get; set; }
		public string Description { get; set; }
	}
}
