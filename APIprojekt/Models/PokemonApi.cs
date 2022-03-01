using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIprojekt.Models
{
    public class PokemonApi : PokeDTO
    {
        public int Id { get; set; }
    }

    public class PokeDTO
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public PokemonApi ToModel()
        {
            return new PokemonApi
            {
                Name = this.Name,
                Url = this.Url,
            };
        }
        public PokeDTO ToDTO()
        {
            return this;
        }
    }
}
