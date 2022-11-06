using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiSample.Models;

namespace WebApiSample.Data
{
    public class Seeder
    {
        public static void Seed(ApiDbContext ctx)
        {
            var ath = new Author
            {
                FirstName = "Zenek",
                LastName = "Martyniuk",
                Id = 1
            };

            var book = new Book
            {
                Id = 1,
                Description = "Oczy zielone",
                Title = "Piesni wybrane",
                AuthorId = ath.Id
            };

            ctx.Authors.Add(ath);
            ctx.Books.Add(book);
            ctx.SaveChanges();
        }
    }
}
