using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using APIprojekt.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

    public class PokeDbContext : DbContext
    {
        public PokeDbContext (DbContextOptions<PokeDbContext> options)
            : base(options)
        {
        }

        public DbSet<PokemonApi> Pokemonsapi { get; set; }


    public static async Task Reset(IServiceProvider provider)
    {
        var context = provider.GetRequiredService<PokeDbContext>();

        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();

        await context.Pokemonsapi.AddRangeAsync(
            new PokemonApi { Name = "spearow", Url = "https://pokeapi.co/api/v2/pokemon/21/" },
            new PokemonApi { Name = "fearow", Url = "https://pokeapi.co/api/v2/pokemon/22/" },
            new PokemonApi { Name = "ekans", Url = "https://pokeapi.co/api/v2/pokemon/23/" },
            new PokemonApi { Name = "arbok", Url = "https://pokeapi.co/api/v2/pokemon/24/" },
            new PokemonApi { Name = "pikachu", Url = "https://pokeapi.co/api/v2/pokemon/25/" },
            new PokemonApi { Name = "raichu", Url = "https://pokeapi.co/api/v2/pokemon/26/" },
            new PokemonApi { Name = "sandshrew", Url = "https://pokeapi.co/api/v2/pokemon/27/" },
            new PokemonApi { Name = "sandslash", Url = "https://pokeapi.co/api/v2/pokemon/28/" },
            new PokemonApi { Name = "nidoran-f", Url = "https://pokeapi.co/api/v2/pokemon/29/" },
            new PokemonApi { Name = "nidorina", Url = "https://pokeapi.co/api/v2/pokemon/30/" }
            );
        await context.SaveChangesAsync();

        var userManager = provider.GetRequiredService<UserManager<IdentityUser>>();
        await userManager.CreateAsync(
        new IdentityUser
        {
            UserName = "TestUser"
        },
        "TestUser");
    }
}
