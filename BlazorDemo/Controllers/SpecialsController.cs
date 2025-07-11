using BlazorDemo.Data;
using BlazorDemo.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlazorDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialsController : ControllerBase
    {
        private readonly PizzaStoreContext _context;

        public SpecialsController(PizzaStoreContext context)
        {
            _context = context;
        }


        // GET: api/<SpecialsController>
        [HttpGet]
        public ActionResult<IEnumerable<PizzaSpecial>> Get()
        {
            var pizzas = _context.Specials.OrderByDescending(p => p.BasePrice).ToList();
            return Ok(pizzas);
        }

        // GET api/<SpecialsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SpecialsController>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<SpecialsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<SpecialsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
