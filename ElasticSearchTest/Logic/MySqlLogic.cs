using ElasticSearchTest.Logic;
using ElasticSearchTest.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearchTest.Logic
{
	public class PatientsContext : DbContext
	{
		DbSet<Patient> Patients { get; set; }
		DbSet<AdditionalTelephoneNumber> AdditionalTelephoneNumbers { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseMySQL("server=localhost;UserId=root;Password=5YZxxkCM0C;database=patients;");
		}

		public int init() {
			int state = 0;
			using (PatientsContext db = new PatientsContext())
			{
				db.Database.EnsureCreated();

				foreach (var patient in Patient.Defaults()) {

				var i=	db.Patients.Add(patient);
				}
				state =db.SaveChanges();

			}
			return state;
		}
	}
	}

	public class MySqlLogic
	{
	public void init() {
		PatientsContext pat = new PatientsContext();
		var i =pat.init();

		if (i == 0) { }
	}
	}
