using CarApp5000.EfCoreData;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarApp5000.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        EfContext con = new EfContext("Server=(localdb)\\mssqllocaldb;Database=CarApp5000;Trusted_Connection=true;");

        // GET: api/<CarsController>
        [HttpGet]
        public IEnumerable<Car> Get()
        {
            return con.Cars.ToList();
        }

        // GET api/<CarsController>/5
        [HttpGet("{id}")]
        public Car Get(int id)
        {
            return con.Cars.Find(id);
        }

        // POST api/<CarsController>
        [HttpPost]
        public void Post([FromBody] Car value)
        {
            con.Cars.Add(value);
            con.SaveChanges();
        }

        // PUT api/<CarsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Car value)
        {
            con.Cars.Update(value);
            con.SaveChanges();
        }

        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            con.Cars.Remove(Get(id));
            con.SaveChanges();
        }
    }
}
