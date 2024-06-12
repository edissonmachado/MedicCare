using MediatR;
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
            return await Task.FromResult(Ok($"Patient id: {id}"));
        }

        [HttpGet]
        [Route("encounters")]
        public async Task<ActionResult> GetEncounters()
        {
            var query = new GetPatientsEncountersQuery();
            try
            {
                var result = await _mediator.Send(query).ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
