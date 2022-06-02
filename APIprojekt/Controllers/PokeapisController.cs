using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APIprojekt.Models;
using Microsoft.AspNetCore.Authorization;
using Swashbuckle.AspNetCore.Annotations;

namespace APIprojekt.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokeApisController : ControllerBase
    {
        private readonly PokeDbContext _context;

        public PokeApisController(PokeDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Authorize]

        [SwaggerOperation(
            Summary = "Man få en lista",
            Description = "Gör en lista med x antal olika pokemon"
        )]
        [SwaggerResponse(200, "En lista med pokemon")]

        public async Task<ActionResult<IEnumerable<PokeDTO>>> Get()
        {
            return await _context.Pokemonsapi.Select(t => t.ToDTO()).ToListAsync();
        }
        

        [HttpGet("{id}")]
        [Authorize]

        [SwaggerOperation(
            Summary = "Välja ett nummer",
            Description = "Välja ett nummer mellan 1-10 för att en åt gången"
        )]
        [SwaggerResponse(200, Description = "Se dom en åt gången")]

        public async Task<ActionResult<PokeDTO>> GetPoke(int id)
        {
            var poke = await _context.Pokemonsapi.FindAsync(id);

            if (poke == null)
            {
                return NotFound();
            }

            return poke.ToDTO();

        }


        [HttpPost]
        [Authorize]

        [SwaggerOperation(
            Summary = "Lägga till en ny",
            Description = "Lägga till en ny pokemon"
        )]
        [SwaggerResponse(200, Description = "En ny pokemon kan läggas till.")]

        public async Task<ActionResult<PokeDTO>> PostTodo(PokeDTO dto)
        {
            var poke = dto.ToModel();
            _context.Pokemonsapi.Add(poke);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPokeapi", new { id = poke.Id }, dto);
        }


        [HttpDelete("{id}")]
        [Authorize]

        [SwaggerOperation(
            Summary = "Ta bort",
            Description = "Välj ett nummer för att ta bort den pokemonen från listan"
        )]
        [SwaggerResponse(200, Description = "En lista med vilken som är borttagen.")]

        public async Task<IActionResult> DeletePoke(int id)
        {
            var todo = await _context.Pokemonsapi.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Pokemonsapi.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
