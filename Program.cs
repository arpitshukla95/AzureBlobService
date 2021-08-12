using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using System;

namespace AzureBlobService
{
    class Program
    {
        private static string _container_name = "data";
        
        private static string _connection_string = "_CONNECTION_STRING";
        private static string _blobName = "GreenBeans1.cs";
        private static string _path = @"C:\Azure downloads\Blobs\GreenBeans1.cs";
        static void Main(string[] args)
        {
            BlobServiceClient _blobServiceClient = new BlobServiceClient(_connection_string);

            BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_container_name);
            //List blobs
            //foreach(BlobItem item in _blobContainerClient.GetBlobs())
            //{
            //    Console.WriteLine(item.Name);
            //}
            //------------------------------------------------------------------
            //Upload blob
            //BlobClient blobClient = _blobContainerClient.GetBlobClient(_blobName);
            //blobClient.DownloadTo(_path);
            //Console.WriteLine("Blob uploaded successfully");
            //DownLoad blob
            //BlobClient blobClient = _blobContainerClient.GetBlobClient(_blobName);
            //blobClient.DownloadTo(_path);
            //Console.WriteLine("Blob downloaded successfully");
            ReadBlob();
            Console.ReadKey();

            
        }
        public static void ReadBlob()
        {
            Uri _blob_uri = GenerateSAS();
            BlobClient blobClient = new BlobClient(_blob_uri);
            blobClient.DownloadTo(_path);
            Console.WriteLine("Downloaded successfully");
        }
        public static Uri GenerateSAS()
        {
            BlobServiceClient _blobServiceClient = new BlobServiceClient(_connection_string);

            BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(_container_name);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(_blobName);
            BlobSasBuilder blobSasBuilder = new BlobSasBuilder()
            {
                BlobContainerName = _container_name,
                BlobName = _blobName,
                Resource = "b"
            };
            blobSasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.List);
            blobSasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddHours(1);
            return blobClient.GenerateSasUri(blobSasBuilder);

        }
    }
}
