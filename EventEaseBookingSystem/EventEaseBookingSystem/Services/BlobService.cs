using Azure.Storage.Blobs;
using System;
using System.IO;
using System.Threading.Tasks;

public class BlobService
{
    // Your Azure Storage connection string (from the Azure Portal)
    private string connectionString = "DefaultEndpointsProtocol=https;AccountName=eventeasest10448834;AccountKey=vgo3HI3ny3rHIftnYaolH6geqQBm5Djvq7XxjobegzFamFp2F1iItsy+7BjjLaTd/WO3Lqwf2Yyu+AStq4FLKQ==;EndpointSuffix=core.windows.net";

    // Name of your container
    private string containerName = "eventease-images";

    // Upload method
    public async Task<string> UploadImageAsync(Stream fileStream, string fileName)
    {
        // Create client to connect to Azure Blob Storage
        var blobServiceClient = new BlobServiceClient(connectionString);

        // Get container client (create it if it doesn't exist)
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync();

        // Get a blob client to represent the uploaded image
        var blobClient = containerClient.GetBlobClient(fileName);

        // Upload the image
        await blobClient.UploadAsync(fileStream, overwrite: true);

        // Return the public URL of the image to store in the database
        return blobClient.Uri.ToString();
    }
}
