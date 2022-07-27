using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Flurl;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("/")]
    public class ImagesController : ControllerBase
    {
        private HttpClient _httpClient;
        private readonly Options _options;

        public ImagesController(HttpClient httpClient, Options options)
        {
            _httpClient = httpClient;
            _options = options;
        }

        private async Task<BlobContainerClient> GetCloudBlobContainer(string containerName)
        {
            var serviceClient = new BlobServiceClient(_options.StorageConnectionString);
            var containerClient = serviceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();
            return containerClient;
        }

        [Route("/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var containerClient = await GetCloudBlobContainer(_options.FullImageContainerName);
            var results = new List<string>();

            await foreach (var blobItem in containerClient.GetBlobsAsync())
            {
                var result = Flurl.Url.Combine(containerClient.Uri.AbsoluteUri, blobItem.Name);
                results.Add(result);
            }

            await Console.Out.WriteLineAsync("Got Images");
            return Ok(results);
        }

        [Route("/")]
        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var image = Request.Body;
            var containerClient = await GetCloudBlobContainer(_options.FullImageContainerName);
            var blobName = Guid.NewGuid().ToString().ToLower().Replace("-", string.Empty);
            var blobClient = containerClient.GetBlobClient(blobName);
            await blobClient.UploadAsync(image);
            return Created(blobClient.Uri, null);
        }
    }
}