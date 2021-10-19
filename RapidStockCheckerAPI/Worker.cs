using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nager.AmazonProductAdvertising;
using RapidStockChecker.DataAccess;
using RSC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RapidStockCheckerAPI
{
    public class Worker : BackgroundService
    {
        protected IServiceProvider serviceProvider;
        protected IWorkerService workerService;

        public Worker(IServiceProvider serviceProvider, IWorkerService workerService)
        {
            this.serviceProvider = serviceProvider;
            this.workerService = workerService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = this.serviceProvider.CreateScope())
                    {
                        int productsFound = 0;
                        var watch = new System.Diagnostics.Stopwatch();
                        var db = (ApplicationDbContext)scope.ServiceProvider.GetRequiredService(typeof(ApplicationDbContext));
                        var authentication = new AmazonAuthentication("AKIAJVMZ5BI4GYNFNJYQ", "61plkh/hS7ltiwS24FiQWJQBBo/Fb6vvis2wO4QO");
                        var client = new AmazonProductAdvertisingClient(authentication, AmazonEndpoint.US, "rapidstockche-20");

                        List<string> products = db.Products.Select(p => p.SKU).ToList();

                        Console.WriteLine($"\nChecking stock for {products.Count} products");

                        for (int i = 0; i < products.Count; i = i + 10)
                        {
                            watch.Start();
                            List<string> items = new List<string>(products.Skip(i).Take(10));
                            var result = await client.GetItemsAsync(items.ToArray());

                            if (result.ItemsResult != null)
                            {
                                for (int j = 0; j < items.Count; j++)
                                {
                                    if (result.ItemsResult.Items[j].Offers != null)
                                    {
                                        if (result.ItemsResult.Items[j].Offers.Listings[0].MerchantInfo.Name == "Amazon.com")
                                        {
                                            if (result.ItemsResult.Items[j].Offers.Listings[0].Availability.Message == "In Stock."
                                               || result.ItemsResult.Items[j].Offers.Listings[0].Availability.Type == "Backorderable")
                                            {
                                                Console.WriteLine($"{result.ItemsResult.Items[j].ItemInfo.Title.DisplayValue} was found in stock!");

                                                Product product = db.Products.Where(p => p.SKU == result.ItemsResult.Items[j].ASIN).FirstOrDefault();
                                                product.InStock = true;
                                                db.Products.Update(product);
                                                productsFound++;

                                                var type = db
                                                    .Products
                                                    .Where(p => p.SKU == product.SKU)
                                                    .Select(t => t.Type)
                                                    .FirstOrDefault();

                                                RestockHistory mostRecentRestock = db
                                                    .RestockHistory
                                                    .Where(h => h.SKU == product.SKU)
                                                    .OrderByDescending(h => h.DateTime)
                                                    .FirstOrDefault();

                                                if (mostRecentRestock == null)
                                                {
                                                    RestockHistory restockHistory = new RestockHistory()
                                                    {
                                                        Name = product.Title,
                                                        SKU = product.SKU,
                                                        DateTime = DateTime.UtcNow,
                                                        Type = type
                                                    };

                                                    db.RestockHistory.Add(restockHistory);
                                                }
                                                else if (DateTime.UtcNow > mostRecentRestock.DateTime.AddHours(1)) //Change back to hour
                                                {
                                                    RestockHistory restockHistory = new RestockHistory()
                                                    {
                                                        Name = product.Title,
                                                        SKU = product.SKU,
                                                        DateTime = DateTime.UtcNow,
                                                        Type = type
                                                    };

                                                    db.RestockHistory.Add(restockHistory);
                                                }

                                                await db.SaveChangesAsync();
                                            }
                                        }
                                        else
                                        {
                                            Product product = db
                                                .Products
                                                .Where(p => p.SKU == result.ItemsResult.Items[j].ASIN)
                                                .FirstOrDefault();

                                            product.InStock = false;
                                            db.Products.Update(product);
                                            await db.SaveChangesAsync();
                                        }
                                    }
                                }

                                if (productsFound == 0)
                                {
                                    Console.WriteLine("No products found in stock, restarting\n");
                                }
                            }
                            else
                            {
                                Console.WriteLine("No valid ASIN(s) found");
                            }

                            Console.WriteLine($"\nStock checked. Getting Next Set...");
                            await Task.Delay(9000);
                        }

                        watch.Stop();
                        Console.WriteLine($"\nCompleted Stock Check for {products.Count} products in {watch.ElapsedMilliseconds} milliseconds.\nRestarting stock check at the begining of the list");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
