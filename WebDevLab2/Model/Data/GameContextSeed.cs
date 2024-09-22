using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Metadata;
using WebDevLab2.Model.Context;
using Microsoft.AspNetCore.Identity.Data;

namespace WebDevLab2.Model.Data
{
    public class GameContextSeed
    {
        public static async Task SeedAsync(MainContext context)
        {
            try
            {
                context.Database.EnsureCreated();
                if ((context.Player.Any()) || (context.Developer.Any()) || (context.Game.Any()))
                {
                    return;
                }

                Developer developer = new()
                {
                    CompanyName = "RandomPeopleFromTheGarage",
                    Id = 1,
                    Login = "RPFTG",
                    Password = "1111",

                };

                Player player = new()
                {
                    Id = 1,
                    Login = "Вася",
                    Password = "1111"
                };

                Game game = new()
                {
                    Title = "VasyaLive Ultra Delux",
                    Price = 500,
                    Description = "Totally Real Strory About Unreal Person",
                    Genre = "Симулятор", //Добавить словарь
                    Developer = developer
                };

                context.Developer.Add(developer);
                context.Player.Add(player);
                context.Game.Add(game);

                await context.SaveChangesAsync();

            }
            catch
            {
                throw;
            }
        }
    }
}
