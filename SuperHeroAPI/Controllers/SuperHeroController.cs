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

        private readonly DataContext _dataContext;
        public SuperHeroController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
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
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _dataContext.SuperHeroes.Add(hero);
            await _dataContext.SaveChangesAsync();  
            return Ok(await _dataContext.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id) //we can show the schemas
        {
            var hero =await _dataContext.SuperHeroes.FindAsync(id);
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
            var dbHero = await _dataContext.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found.");
            }
            else
            {
                dbHero.Name = request.Name;
                dbHero.FirstName = request.FirstName;
                dbHero.LastName = request.LastName;
                dbHero.Place = request.Place;

                await _dataContext.SaveChangesAsync();
                return Ok(await _dataContext.SuperHeroes.ToListAsync());
            }
            
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> DeleteHero(int id)
        {
            var hero = await _dataContext.SuperHeroes.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found.");
            }
            else
            {
                _dataContext.SuperHeroes.Remove(hero);
                await _dataContext.SaveChangesAsync();
                return Ok(await _dataContext.SuperHeroes.ToListAsync());
            }
        }
    }
}
