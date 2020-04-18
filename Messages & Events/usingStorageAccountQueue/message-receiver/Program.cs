using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace message_receiver
{
    class Program
    {
        private static string ConnectionString = "DefaultEndpointsProtocol=https;AccountName=storagequeueam14;AccountKey=PRsGAw6KuPoSIg0HLxHWORa4ZD2d53eUq7Fdn96H+qq4K13OIOzhkqyZqg1i6Q7OmG6WLHURr1bxvdIIqMxyFA==;EndpointSuffix=core.windows.net";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string value = await ReceiveArticleAsync();
            Console.WriteLine($"Received {value}");
        }

        static async Task<string> ReceiveArticleAsync()
        {
            CloudQueue queue = GetQueue();
            bool exists = await queue.ExistsAsync();
            if (exists)
            {
                CloudQueueMessage retrievedArticle = await queue.GetMessageAsync();
                if (retrievedArticle != null)
                {
                    string newsMessage = retrievedArticle.AsString;
                    await queue.DeleteMessageAsync(retrievedArticle);
                    return newsMessage;
                }
            }

            return "<queue empty or not created>";
        }

        static CloudQueue GetQueue()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
            return queueClient.GetQueueReference("newsqueue");
        }
    }
}
