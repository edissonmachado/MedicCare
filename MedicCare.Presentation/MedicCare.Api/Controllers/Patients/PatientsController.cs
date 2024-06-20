using MediatR;
using MedicCare.App.Patients.GetPatient;
using MedicCare.App.Patients.GetPatientReports;
using Microsoft.AspNetCore.Mvc;

namespace MedicCare.Api.Controllers.Patients
{
    [Route("api/patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var query = new GetPatientByIdQuery() { Id = id};

            var result = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        [Route("encounters")]
        public async Task<ActionResult> GetEncounters()
        {
            var query = new GetPatientsEncountersQuery();
            
            var result = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(result);

        }

        [HttpGet]
        [Route("encounters/light")]
        public async Task<ActionResult> GetEncountersLight()
        {
            var query = new GetPatientsEncountersLightQuery();

            var result = await _mediator.Send(query).ConfigureAwait(false);
            return Ok(result);

        }
    }
}
