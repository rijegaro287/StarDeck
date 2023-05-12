using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stardeck.Logic;
using Stardeck.Models;

namespace Stardeck.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConstantsController : ControllerBase
    {
        private readonly StardeckContext context;
        private ConstantsLogic constantsLogic;

        public ConstantsController(StardeckContext context)
        {
            this.context = context;
            this.constantsLogic = new ConstantsLogic(context);
        }


        // GET: api/<ConstantsController>
        [HttpGet]
        [Route("get_all")]
        public async Task<IActionResult> Get()
        {
            if (constantsLogic.GetAll() == null)
            {
                return NotFound();
            }
            return Ok(constantsLogic.GetAll());

        }

        // GET api/<ConstantsController>/5
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (constantsLogic.GetConstant(id) == null)
            {
                return NotFound();
            }
            return Ok(constantsLogic.GetConstant(id));

        }

        // POST api/<ConstantsController>
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> Post([FromBody] Constant constant)
        {
            Constant constAux = constantsLogic.NewConstant(constant.Key, constant.Value);
            if (constAux == null)
            {
                return BadRequest();
            }
            return Ok(constAux);


        }



    }
}
