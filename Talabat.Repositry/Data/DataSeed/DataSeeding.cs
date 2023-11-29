using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repositry.Data.Data_Seed
{
    public class DataSeeding
    {
        public async static Task DataSeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {

            #region Ex Code
            //var Products = File.ReadAllText("../DataSeed/products.json");

            //var ProductsList = JsonSerializer.Deserialize<List<Product>>(Products);

            //try
            //{
            //    foreach (var product in ProductsList)
            //        context.Products.Add(product);
            //    await context.SaveChangesAsync();
            //}
            //catch (Exception ex)
            //{
            //    var logger = loggerFactory.CreateLogger<DataSeeding>();

            //    logger.LogError(ex.ToString());

            //} 
            #endregion

            var pro = context.Products;
            var count = context.Products.Count();
                // for: categories
                await AddDateSeedToContext<ProductCategory>(context, "categories", loggerFactory);

                // for: brands
                await AddDateSeedToContext<ProductBrand>(context, "brands", loggerFactory);

                // for: products
                await AddDateSeedToContext<Product>(context, "products",  loggerFactory);

                //for:  deliveryMethods
                await AddDateSeedToContext<DeliveryMethod>(context, "delivery",  loggerFactory);

            

        }

        private async  static Task AddDateSeedToContext<T>(StoreContext context, string fileName,  ILoggerFactory loggerFactory) where T : BaseEntity
        {
            if (context.Set<T>().Count() <= 0)
            {
                //var path = $"../Talabat.Repositry/DataSeed/{fileName}.json";
                var Items = File.ReadAllText($"../Talabat.Repositry/Data/DataSeed/{fileName}.json");

                var ItemsList = JsonSerializer.Deserialize<List<T>>(Items);

                try
                {
                    if (ItemsList is not null)
                        foreach (var item in ItemsList)
                            context.Set<T>().Add(item);

                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<DataSeeding>();

                    logger.LogError(ex.ToString());

                }

            }

        }


    }
}
