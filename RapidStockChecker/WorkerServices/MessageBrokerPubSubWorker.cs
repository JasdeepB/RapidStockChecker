using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RapidStockChecker.Hubs;
using RapidStockChecker.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace RapidStockChecker.WorkerServices
{
    public sealed class MessageBrokerPubSubWorker : BackgroundService
    {
        private IHubContext<MessageBrokerHub> hub;
        private List<string> productsCurrentlyInStock = new List<string>();

        public MessageBrokerPubSubWorker(IHubContext<MessageBrokerHub> messageBrokerHubContext)
        {
            this.hub = messageBrokerHubContext;
        }

        private async Task ProductCheck(List<ProductsInStock> products)
        {
            if (products.Count == 0)
            {
                Console.WriteLine("No Products are currently in stock\nIf you believe this is an error check the API response");
                productsCurrentlyInStock = new List<string>();
                return;
            }

            if (productsCurrentlyInStock.Count == 0)
            {
                for (int i = 0; i < products.Count; i++)
                {
                    productsCurrentlyInStock.Add(products[i].SKU);
                }
            }

            List<string> incomingProducts = new List<string>();

            for (int i = 0; i < products.Count; i++)
            {
                incomingProducts.Add(products[i].SKU);
            }

            for (int i = 0; i < productsCurrentlyInStock.Count; i++)
            {
                if (incomingProducts.Contains(productsCurrentlyInStock[i]) == false)
                {
                    Console.WriteLine("SKU: " + productsCurrentlyInStock[i] + " is no longer in stock, removed from current stock list");
                    productsCurrentlyInStock.RemoveAt(i);
                }
                else
                {
                    Console.WriteLine("SKU: " + productsCurrentlyInStock[i] + " is already marked as in stock");
                }
            }

            for (int i = 0; i < products.Count; i++)
            {
                if (productsCurrentlyInStock.Contains(products[i].SKU) == false)
                {
                    string groupName = products[i].type.Name.Replace(" ", String.Empty);
                    await this.hub.Clients.Groups(groupName).SendAsync(groupName + "AlertTrigger", products[i]);
                    productsCurrentlyInStock.Add(products[i].SKU);
                }
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    WebRequest request = WebRequest.Create("http://rapid-stock-checker-product-api-349794647.us-west-2.elb.amazonaws.com/Product/discord");
                    WebResponse response = request.GetResponse();

                    using (Stream dateStream = response.GetResponseStream())
                    {
                        StreamReader reader = new StreamReader(dateStream);
                        string responseFromServer = reader.ReadToEnd();

                        List<ProductsInStock> productsInStock = JsonConvert.DeserializeObject<List<ProductsInStock>>(responseFromServer);
                        await ProductCheck(productsInStock);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
