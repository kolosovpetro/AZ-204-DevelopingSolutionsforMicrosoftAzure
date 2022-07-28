namespace AdventureWorks.Upload
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text.Json;
    using System.IO;
    using Microsoft.Azure.Cosmos;

    public static class Program
    {
        private const string EndpointUrl = "https://polycosmospkolosov.documents.azure.com:443/";

        private const string AuthorizationKey =
            "Z7tIiG02AAJCrDoEMsrdPtJCsoqNxpupsRV2GRUlz0is9Gq6Al8LwRJqcJstlWl1eo7JIDC5f2Ny9T6nlazO2A==";

        private const string DatabaseName = "Retail";
        private const string ContainerName = "Online";
        private const string PartitionKey = "/Category";

        private const string JsonFilePath =
            "D:\\RiderProjects\\AZ-204-DevelopingSolutionsforMicrosoftAzure\\Allfiles\\Labs\\04\\Starter\\AdventureWorks\\AdventureWorks.Upload\\models.json";

        private static int _amountToInsert;
        private static List<Model> _models;

        private static async Task Main()
        {
            try
            {
                // <CreateClient>
                var cosmosClient = new CosmosClient(EndpointUrl, AuthorizationKey,
                    new CosmosClientOptions { AllowBulkExecution = true });
                // </CreateClient>

                // <Initialize>
                Console.WriteLine($"Creating a database if not already exists...");
                Database database = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);

                // Configure indexing policy to exclude all attributes to maximize RU/s usage
                Console.WriteLine($"Creating a container if not already exists...");

                try
                {
                    await database.DefineContainer(ContainerName, PartitionKey)
                        .WithIndexingPolicy()
                        .WithIndexingMode(IndexingMode.Consistent)
                        .WithIncludedPaths()
                        .Attach()
                        .WithExcludedPaths()
                        .Path("/*")
                        .Attach()
                        .Attach()
                        .CreateAsync();
                }
                catch (Exception)
                {
                }
                
                // </Initialize>

                using (var reader = new StreamReader(File.OpenRead(JsonFilePath)))
                {
                    var json = await reader.ReadToEndAsync();
                    _models = JsonSerializer.Deserialize<List<Model>>(json);
                    _amountToInsert = _models.Count;
                }

                // Prepare items for insertion
                Console.WriteLine($"Preparing {_amountToInsert} items to insert...");

                // Create the list of Tasks
                Console.WriteLine($"Starting...");
                var stopwatch = Stopwatch.StartNew();
                // <ConcurrentTasks>
                var container = database.GetContainer(ContainerName);

                var tasks = new List<Task>(_amountToInsert);
                foreach (var model in _models)
                {
                    tasks.Add(container.CreateItemAsync(model, new PartitionKey(model.Category))
                        .ContinueWith(itemResponse =>
                        {
                            if (!itemResponse.IsCompletedSuccessfully)
                            {
                                AggregateException innerExceptions = itemResponse.Exception.Flatten();
                                if (innerExceptions.InnerExceptions.FirstOrDefault(
                                        innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                                {
                                    Console.WriteLine(
                                        $"Received {cosmosException.StatusCode} ({cosmosException.Message}).");
                                }
                                else
                                {
                                    Console.WriteLine($"Exception {innerExceptions.InnerExceptions.FirstOrDefault()}.");
                                }
                            }
                        }));
                }

                // Wait until all are done
                await Task.WhenAll(tasks);
                // </ConcurrentTasks>
                stopwatch.Stop();

                Console.WriteLine($"Finished writing {_amountToInsert} items in {stopwatch.Elapsed}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public class Model
        {
            public string id { get; set; }
            public string Name { get; set; }
            public string Category { get; set; }
            public string Description { get; set; }
            public string Photo { get; set; }
            public IList<Product> Products { get; set; }
        }

        public class Product
        {
            public string id { get; set; }
            public string Name { get; set; }
            public string Number { get; set; }
            public string Category { get; set; }
            public string Color { get; set; }
            public string Size { get; set; }
            public decimal? Weight { get; set; }
            public decimal ListPrice { get; set; }
            public string Photo { get; set; }
        }
    }
}