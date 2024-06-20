using MedicCare.App.Patients;
using MedicCare.App.Patients.GetPatientReports;
using MedicCare.Domain;
using Moq;

namespace MedicCare.App.Tests
{
    public class GetPatientEncounterTests
    {
        private Mock<IPatientRepository> _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new Mock<IPatientRepository>();
        }

        [Test]
        public async Task GetPatientEncountersQueryHandler_TwoEncountersForSamePatient_ReturnsOneRecord()
        {
            int expectedPatients = 1;
            string[] expectedCities = new string[] { "city1", "city2" };
            char expectedCategory = 'A';

            _repo.Setup(c => c.GetEncountersAsync())
                .Returns(GetUniquePatientEncountersForTwoCities);

            var handler = new GetPatientEncountersQueryHandler(_repo.Object);
            var query = new GetPatientsEncountersQuery();
            var token = new CancellationToken();
            var results = await handler.Handle(query, token);

            Assert.That(expectedPatients, Is.EqualTo(results.Count));
            Assert.That(expectedCities, Is.EqualTo(results[0].Cities));
            Assert.That(expectedCategory, Is.EqualTo(results[0].Category));
        }

        [Test]
        public async Task GetPatientEncountersQueryHandler_TwoEncountersForTwoPatients_ReturnsZeroRecord()
        {
            int expectedPatients = 0;
            string[] expectedCities = new string[] { "city1", "city2" };
            

            _repo.Setup(c => c.GetEncountersAsync())
                .Returns(GetTwoPatientsEncountersForTwoCities);

            var handler = new GetPatientEncountersQueryHandler(_repo.Object);
            var query = new GetPatientsEncountersQuery();
            var token = new CancellationToken();
            var results = await handler.Handle(query, token);

            Assert.That(expectedPatients, Is.EqualTo(results.Count));
        }

        private async Task<List<Encounter>> GetUniquePatientEncountersForTwoCities()
        {
            int patientId = 1;
            var encounters = new List<Encounter>
            {
                new Encounter()
                {
                    Id = 1,
                    Patient = new Patient() { Id = patientId, FirstName = "name1", LastName = "lastname1", Age = 1 },
                    Facility = new Facility() { Id = 1, BranchName = "branch1", BranchCity = "city1" },
                    Payer = new Payer() { Id = 1, CompanyName = "company1", CompanyCity = "city1" }
                },
                new Encounter()
                {
                    Id = 2,
                    Patient = new Patient() { Id = patientId, FirstName = "name1", LastName = "lastname1", Age = 1 },
                    Facility = new Facility() { Id = 2, BranchName = "branch2", BranchCity = "city2" },
                    Payer = new Payer() { Id = 2, CompanyName = "company2", CompanyCity = "city2" }
                }
            };
            return await Task.FromResult(encounters);
        }

        private async Task<List<Encounter>> GetTwoPatientsEncountersForTwoCities()
        {
            var encounters = new List<Encounter>
            {
                new Encounter()
                {
                    Id = 1,
                    Patient = new Patient() { Id = 1, FirstName = "name1", LastName = "lastname1", Age = 1 },
                    Facility = new Facility() { Id = 1, BranchName = "branch1", BranchCity = "city1" },
                    Payer = new Payer() { Id = 1, CompanyName = "company1", CompanyCity = "city1" }
                },
                new Encounter()
                {
                    Id = 2,
                    Patient = new Patient() { Id = 2, FirstName = "name1", LastName = "lastname1", Age = 1 },
                    Facility = new Facility() { Id = 2, BranchName = "branch2", BranchCity = "city2" },
                    Payer = new Payer() { Id = 2, CompanyName = "company2", CompanyCity = "city2" }
                }
            };
            return await Task.FromResult(encounters);
        }
    }
}