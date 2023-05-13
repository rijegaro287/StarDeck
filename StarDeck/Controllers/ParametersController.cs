using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stardeck.Logic;
using Stardeck.Models;

namespace Stardeck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParametersController : ControllerBase
    {
        private readonly StardeckContext context;
        private ParametersLogic parametersLogic;

        public ParametersController(StardeckContext context)
        {
            this.context = context;
            this.parametersLogic = new ParametersLogic(context);
        }


        // GET: api/<ConstantsController>
        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get()
        {
            if (parametersLogic.GetAll() == null)
            {
                return NotFound();
            }
            return Ok(parametersLogic.GetAll());

        }

        // GET api/<ConstantsController>/5
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (parametersLogic.GetParameter(id) == null)
            {
                return NotFound();
            }
            return Ok(parametersLogic.GetParameter(id));

        }

        // POST api/<ConstantsController>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Post([FromBody] Parameter constant)
        {
            Parameter constAux = parametersLogic.NewParameter(constant.Key, constant.Value);
            if (constAux == null)
            {
                return BadRequest();
            }
            return Ok(constAux);


        }



    }
}
