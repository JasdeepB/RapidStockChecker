﻿using RapidStockChecker.DataAccess;
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
                        new Models.Type
                        {
                            Name = "RTX 3080",
                            Discord = new Discord()
                            {
                                Channel = 123,
                                Role = 789,
                                SleepTime = 60000,
                                Color = "green"
                            },
                            Products = new List<Product>()
                            {
                                new Product()
                                {
                                    SKU = "ABCD",
                                    ImageUrl = "screenshot.best",
                                    Title = "MSI RTX 3080",
                                    Url = "Amazon.com/ABCD=tag:RSC",
                                    InStock = false,
                                    RestockHistory = new List<RestockHistory>()
                                    {
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.Now
                                        },
                                        new RestockHistory
                                        {
                                        DateTime = DateTime.UtcNow
                                        }
                                    }
                                },
                                new Product()
                                {
                                    SKU = "EFGH",
                                    Title = "EVGA RTX 3080",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/EFGH=tag:RSC",
                                    InStock = true,
                                    RestockHistory = new List<RestockHistory>()
                                    {
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.Now.AddDays(3)
                                        },
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.UtcNow.AddDays(6)
                                        }
                                    }
                                }
                            }
                        },
                        new Models.Type
                        {
                            Name = "RTX 3070",
                            Discord = new Discord()
                            {
                                Channel = 963,
                                Role = 369,
                                SleepTime = 60000,
                                Color = "silver"
                            },
                            Products = new List<Product>()
                            {
                                new Product()
                                {
                                    SKU = "FGFG",
                                    Title = "ASUS RTX 3070",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/FGFG=tag:RSC",
                                    InStock = true,
                                    RestockHistory = new List<RestockHistory>()
                                    {
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.Now.AddDays(3)
                                        },
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.UtcNow.AddDays(6)
                                        }
                                    },
                                },
                                new Product()
                                {
                                    SKU = "EFGH",
                                    Title = "ZOTAC RTX 3070",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/EFGH=tag:RSC",
                                    InStock = false,
                                    RestockHistory = new List<RestockHistory>()
                                    {
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.Now.AddDays(3)
                                        },
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.UtcNow.AddDays(6)
                                        }
                                    }
                                }
                            }
                        }
                    }
                },
                new Category()
                {
                    Name = "AMD Radeon 6000 Series Graphics Cards",
                    Types = new List<RSC.Models.Type>()
                    {
                        new Models.Type
                        {
                            Name = "RX 6800XT",
                            Discord = new Discord()
                            {
                                Channel = 456,
                                Role = 741,
                                SleepTime = 60000,
                                Color = "red"
                            },
                            Products = new List<Product>()
                            {
                                new Product()
                                {
                                    SKU = "IJKL",
                                    Title = "XFX RX 6800XT",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/IJKL=tag:RSC",
                                    InStock = true,
                                    RestockHistory = new List<RestockHistory>()
                                    {
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.Now.AddDays(3)
                                        },
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.UtcNow.AddDays(6)
                                        }
                                    }
                                },
                                new Product()
                                {
                                    SKU = "MNOP",
                                    Title = "XFX 2 RX 6800XT",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/MNOP=tag:RSC",
                                    InStock = false,
                                    RestockHistory = new List<RestockHistory>()
                                    {
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.Now.AddDays(3)
                                        },
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.UtcNow.AddDays(6)
                                        }
                                    }
                                },
                                new Product()
                                {
                                    SKU = "QRST",
                                    Title = "PNY RX 6800XT",
                                    ImageUrl = "screenshot.best",
                                    Url = "Amazon.com/QRST=tag:RSC",
                                    InStock = true,
                                    RestockHistory = new List<RestockHistory>()
                                    {
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.Now.AddDays(3)
                                        },
                                        new RestockHistory
                                        {
                                            DateTime = DateTime.UtcNow.AddDays(6)
                                        }
                                    }
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
