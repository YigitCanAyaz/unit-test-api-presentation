using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestApiPresentation.Api.Contexts;
using UnitTestApiPresentation.Api.Models;

namespace UnitTestApiPresentation.Test
{
    public class BaseTest
    {
        protected DbContextOptions<ApiUnitTestDBContext> _contextOptions { get; private set; }

        public void SetContextOptions(DbContextOptions<ApiUnitTestDBContext> contextOptions)
        {
            _contextOptions = contextOptions;
            Seed();
        }

        public void Seed()
        {
            using (ApiUnitTestDBContext context = new ApiUnitTestDBContext(_contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Category.Add(new Category() { Name = "Kalemler" });
                context.Category.Add(new Category() { Name = "Defterler" });
                context.SaveChanges();

                context.Product.Add(new Product() { CategoryId = 1, Name = "kalem 10", Price = 100, Stock = 100, Color = "Kırmızı" });

                context.Product.Add(new Product() { CategoryId = 1, Name = "kalem 10", Price = 100, Stock = 100, Color = "Mavi" });

                context.SaveChanges();
            }
        }
    }
}
