using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {

        private static List<SuperHero> heroes = new List<SuperHero>()
            {
                new SuperHero{Id=1,Name="Spider Man",FirstName="Peter",LastName="Parker",Place="New York City"},
                new SuperHero{Id=2,Name="Ironman",FirstName="Tony",LastName="Stark",Place="Long Island"},
                new SuperHero{Id=3,Name="Dr Strange",FirstName="Stephan",LastName="Strange",Place="Sanctum Sanctorum"}
            };


        //[HttpGet]
        //public async Task<IActionResult> Get() //We cannot show the schemas in swegger because it is IActionresult interface.
        //{
        //    var heroes=new List<SuperHero>()
        //    {
        //        new SuperHero{Id=1,Name="Spider Man",FirstName="Peter",LastName="Parker",Place="New York City"}
        //    };

        //    return Ok(heroes);
        //}

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get() //we can show the schemas
        {
            return Ok(heroes);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            heroes.Add(hero);
            return Ok(heroes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id) //we can show the schemas
        {
            var hero = heroes.Find(x=>x.Id==id);
            if (hero==null)
            {
                return BadRequest("Hero not found.");
            }
            else
            {
                return Ok(hero);
            }
            
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {
            var hero = heroes.Find(x => x.Id == request.Id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }
            else
            {
                hero.Name = request.Name;
                hero.FirstName = request.FirstName;
                hero.LastName = request.LastName;
                hero.Place = request.Place;
                return Ok(heroes);
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = heroes.Find(x => x.Id == id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }
            else
            {
                heroes.Remove(hero);
                return Ok(heroes);
            }
        }
    }
}
