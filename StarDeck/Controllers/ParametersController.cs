using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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


        // GET: api/<ParametersController>
        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get()
        {
            if (parametersLogic.GetAll() is null)
            {
                return NotFound();
            }

            return Ok(parametersLogic.GetAll());
        }

        // GET api/<ParametersController>/5
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

        // GET api/<ParametersController>/5
        [HttpGet]
        [Route("/api/Parameters/CardsType")]
        public async Task<IActionResult> GetCardType()
        {
            return Ok(JsonConvert.SerializeObject(parametersLogic.GetCardType()).ToUpper());
        }

        // POST api/<ParametersController>
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

        // PUT api/<ParametersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, string nValue)
        {
            var param = parametersLogic.UpdateParameter(id, nValue);
            if (param != null)
            {
                return Ok(param);
            }

            return NotFound();
        }

        // DELETE api/<ParametersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var param = parametersLogic.DeleteParameter(id);
            if (param != null)
            {
                return Ok(param);
            }

            return NotFound();
        }
    }
}