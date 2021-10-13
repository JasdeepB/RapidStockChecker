using RapidStockChecker.DataAccess;
using RSC.Models;
using System;
using System.Collections.Generic;

namespace RSC.DataAccess
{
    public static class DbSeedingClass
    {
        public static void SeedDataContext(this ApplicationDbContext context)
        {
            var categories = new List<Category>()
            {
                new Category()
                {
                    Name = "RTX 30 Series Graphics Cards",
                    Types = new List<RSC.Models.Type>()
                    {
                        new RSC.Models.Type
                        {
                            Name = "RTX 3080",
                            Products = new List<Product>()
                            {
                                new Product()
                                {
                                    SKU = "ABCD",
                                    ImageUrl = "screenshot.best",
                                    Title = "MSI RTX 3080",
                                    Retailer = "Amazon",
                                    Url = "Amazon.com/ABCD=tag:RSC",
                                    InStock = false,
                                    Discord = new Discord()
                                    {
                                        Channel = 123,
                                        Role = 789,
                                        SleepTime = 60000,
                                        Color = "green",
                                        Name = "RTX 3080"
                                    }
                                },
                                new Product()
                                {
                                    SKU = "EFGH",
                                    Title = "EVGA RTX 3080",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/EFGH=tag:RSC",
                                    Retailer = "Amazon",
                                    InStock = true,
                                    Discord = new Discord()
                                    {
                                        Channel = 123,
                                        Role = 789,
                                        SleepTime = 60000,
                                        Color = "green",
                                        Name = "RTX 3080"
                                    }
                                }
                            },
                            RestockHistory = new List<RestockHistory>()
                            {
                                new RestockHistory
                                {
                                    Name = "RTX 3080",
                                    DateTime = DateTime.Now
                                },
                                new RestockHistory
                                {
                                    Name = "RTX 3080",
                                    DateTime = DateTime.UtcNow
                                }
                            }
                        },
                        new RSC.Models.Type
                        {
                            Name = "RTX 3070",
                            Products = new List<Product>()
                            {
                                new Product()
                                {
                                    SKU = "FGFG",
                                    Title = "ASUS RTX 3070",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/FGFG=tag:RSC",
                                    Retailer = "Amazon",
                                    InStock = true,
                                    Discord = new Discord()
                                    {
                                        Channel = 963,
                                        Role = 369,
                                        SleepTime = 60000,
                                        Color = "silver",
                                        Name = "RTX 3070"
                                    }
                                },
                                new Product()
                                {
                                    SKU = "EFGH",
                                    Title = "ZOTAC RTX 3070",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/EFGH=tag:RSC",
                                    Retailer = "Amazon",
                                    InStock = false,
                                    Discord = new Discord()
                                    {
                                        Channel = 963,
                                        Role = 369,
                                        SleepTime = 60000,
                                        Color = "silver",
                                        Name = "RTX 3070"
                                    }
                                }
                            },
                            RestockHistory = new List<RestockHistory>()
                            {
                                new RestockHistory
                                {
                                    Name = "RTX 3070",
                                    DateTime = DateTime.Now.AddDays(3)
                                },
                                new RestockHistory
                                {
                                    Name = "RTX 3070",
                                    DateTime = DateTime.UtcNow.AddDays(6)
                                }
                            },
                        }
                    }
                },
                new Category()
                {
                    Name = "AMD Radeon 6000 Series Graphics Cards",
                    Types = new List<RSC.Models.Type>()
                    {
                        new RSC.Models.Type
                        {
                            Name = "RX 6800XT",
                            Products = new List<Product>()
                            {
                                new Product()
                                {
                                    SKU = "IJKL",
                                    Title = "XFX RX 6800XT",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/IJKL=tag:RSC",
                                    Retailer = "Amazon",
                                    InStock = true,
                                    Discord = new Discord()
                                    {
                                        Channel = 456,
                                        Role = 741,
                                        SleepTime = 60000,
                                        Color = "red",
                                        Name = "RX 6800XT"
                                    }
                                },
                                new Product()
                                {
                                    SKU = "MNOP",
                                    Title = "XFX 2 RX 6800XT",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/MNOP=tag:RSC",
                                    Retailer = "Amazon",
                                    InStock = false,
                                    Discord = new Discord()
                                    {
                                        Channel = 456,
                                        Role = 741,
                                        SleepTime = 60000,
                                        Color = "red",
                                        Name = "RX 6800XT"
                                    }
                                },
                                new Product()
                                {
                                    SKU = "QRST",
                                    Title = "PNY RX 6800XT",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/QRST=tag:RSC",
                                    Retailer = "Amazon",
                                    InStock = true,
                                    Discord = new Discord()
                                    {
                                        Channel = 456,
                                        Role = 741,
                                        SleepTime = 60000,
                                        Color = "red",
                                        Name = "RX 6800XT"
                                    }
                                }
                            },
                            RestockHistory = new List<RestockHistory>()
                            {
                                new RestockHistory
                                {
                                    Name = "RX 6800XT",
                                    DateTime = DateTime.Now.AddDays(3)
                                },
                                new RestockHistory
                                {
                                    Name = "RX 6800XT",
                                    DateTime = DateTime.UtcNow.AddDays(6)
                                }
                            }
                        }
                    }
                }
            };
            context.Categories.AddRange(categories);
            context.SaveChanges();
        }
    }
}
