using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SuperHeroAPI.Data;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }



        [HttpGet]

        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());


    }







        [HttpGet]
        [Route("find")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please provide a valid super hero id");
            }

            SuperHero? selectedSuperHero = new SuperHero();

            selectedSuperHero = await _context.SuperHeroes.FindAsync(id);

            if (selectedSuperHero == null)
                return BadRequest("Please provide a valid super hero id");

            return Ok(selectedSuperHero);
        }



        [HttpPost]
        [Route("Add")]

        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());


        }



        [HttpPut]
        [Route("Edit")]

        public async Task<ActionResult<List<SuperHero>>> UpdateHero(SuperHero request)
        {


            var dbhero = await _context.SuperHeroes.FindAsync(request.Id);
            if (dbhero == null)
            {
                return BadRequest("Please provide a valid super hero id");
            }


            dbhero.Name = request.Name;
            dbhero.FirstName = request.FirstName;
            dbhero.LastName = request.LastName;
            dbhero.Place = request.Place;


            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }


        [HttpDelete("{id}")]

        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {

            var dbhero = await _context.SuperHeroes.FindAsync(id);
            if (dbhero == null)
                return BadRequest("Hero Not Found");

            _context.SuperHeroes.Remove(dbhero);
            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());






        }














    }

}