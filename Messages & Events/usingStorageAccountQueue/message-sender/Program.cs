using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace message_sender
{
    class Program
    {
        private static string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=storagequeueam14;AccountKey=PRsGAw6KuPoSIg0HLxHWORa4ZD2d53eUq7Fdn96H+qq4K13OIOzhkqyZqg1i6Q7OmG6WLHURr1bxvdIIqMxyFA==;EndpointSuffix=core.windows.net";

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string value = String.Join(" ", args);
                SendArticleAsync(value).Wait();
                Console.WriteLine($"Sent: {value}");
            }
        }

        static async Task SendArticleAsync(string newsMessage)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            CloudQueue queue = queueClient.GetQueueReference("newsqueue");
            bool createdQueue = await queue.CreateIfNotExistsAsync();
            if (createdQueue)
            {
                Console.WriteLine("The queue of news articles was created.");
            }

            CloudQueueMessage articleMessage = new CloudQueueMessage(newsMessage);
            await queue.AddMessageAsync(articleMessage);
        }
    }
}
