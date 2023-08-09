using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestApiPresentation.Api.Contexts;
using UnitTestApiPresentation.Api.Controllers;
using UnitTestApiPresentation.Api.Models;
using Xunit;

namespace UnitTestApiPresentation.Test
{
    public class ProductControllerTestWithSqlServerLocalDb : BaseTest
    {
        public ProductControllerTestWithSqlServerLocalDb()
        {
            var sqlCon = @"Server=(localdb)\MSSQLLocalDB;Database=LocalDbTest,Trusted_Connection=true,MultipleActiveResultSets=true";

            SetContextOptions(new DbContextOptionsBuilder<ApiUnitTestDBContext>().UseSqlServer(sqlCon).Options);
        }

        [Fact]
        public async void PostProduct_ActionExecutes_ReturnCreatedAtAction()
        {

            var product = new Product { Name = "Kalem 30", Price = 200, Stock = 100, Color = "Mavi" };

            using (var context = new ApiUnitTestDBContext(_contextOptions))
            {
                var category = context.Category.First();

                product.CategoryId = category.Id;

                var controller = new ProductsContextController(context);

                var result = await controller.PostProduct(product);

                var redirect = Assert.IsType<CreatedAtActionResult>(result);

                Assert.Equal("GetProduct", redirect.ActionName);
            }
        }
    }
}
