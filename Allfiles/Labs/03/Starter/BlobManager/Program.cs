using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Threading.Tasks;

namespace BlobManager;

public static class Program
{
    private const string BlobServiceEndpoint = "<primary-blob-service-endpoint>";
    private const string StorageAccountName = "<storage-account-name>";

    private const string StorageAccountKey = "<key>";


    public static async Task Main(string[] args)
    {
        var accountCredentials = new StorageSharedKeyCredential(StorageAccountName, StorageAccountKey);
        var serviceClient = new BlobServiceClient(new Uri(BlobServiceEndpoint), accountCredentials);
        AccountInfo info = await serviceClient.GetAccountInfoAsync();

        await Console.Out.WriteLineAsync($"Connected to Azure Storage Account");
        await Console.Out.WriteLineAsync($"Account name:\t{StorageAccountName}");
        await Console.Out.WriteLineAsync($"Account kind:\t{info?.AccountKind}");
        await Console.Out.WriteLineAsync($"Account sku:\t{info?.SkuName}");

        await EnumerateContainersAsync(serviceClient);

        const string existingContainerName = "raster-graphics";

        await EnumerateBlobsAsync(serviceClient, existingContainerName);

        const string newContainerName = "vector-graphics";

        var containerClient = await GetOrCreateContainerAsync(serviceClient, newContainerName);

        const string uploadedBlobName = "graph.svg";

        var blobClient = await GetBlobAsync(containerClient, uploadedBlobName);

        await Console.Out.WriteLineAsync($"Blob Url:\t{blobClient.Uri}");
    }

    private static async Task EnumerateContainersAsync(BlobServiceClient client)
    {
        await foreach (var container in client.GetBlobContainersAsync())
        {
            await Console.Out.WriteLineAsync($"Container:\t{container.Name}");
        }
    }

    private static async Task EnumerateBlobsAsync(BlobServiceClient client, string containerName)
    {
        var container = client.GetBlobContainerClient(containerName);

        await Console.Out.WriteLineAsync($"Searching:\t{container.Name}");

        await foreach (var blob in container.GetBlobsAsync())
        {
            await Console.Out.WriteLineAsync($"Existing Blob:\t{blob.Name}");
        }
    }

    private static async Task<BlobContainerClient> GetOrCreateContainerAsync(BlobServiceClient client,
        string containerName)
    {
        var container = client.GetBlobContainerClient(containerName);

        await container.CreateIfNotExistsAsync(PublicAccessType.Blob);
        await Console.Out.WriteLineAsync($"New Container:\t{container.Name}");

        return container;
    }

    private static async Task<BlobClient> GetBlobAsync(BlobContainerClient client, string blobName)
    {
        var blob = client.GetBlobClient(blobName);
        await Console.Out.WriteLineAsync($"Blob Found:\t{blob.Name}");
        return blob;
    }
}