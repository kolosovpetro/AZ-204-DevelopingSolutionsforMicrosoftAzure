// See https://aka.ms/new-console-template for more information

using Azure.Messaging.ServiceBus;

Console.WriteLine("Hello, World!");

const string storageConnectionString =
    "Endpoint=sb://pkolosovsbnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FiN/9YS9qfJD9B9FZq3wCLUkTA236UObPxPgo/HzZZc=";
const string queueName = "pkolsovqueue";
const int numOfMessages = 3;
ServiceBusClient client;
ServiceBusSender sender;

client = new ServiceBusClient(storageConnectionString);
sender = client.CreateSender(queueName);  

using var messageBatch = await sender.CreateMessageBatchAsync();

for (var i = 1; i <= numOfMessages; i++)
{
    if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
    {
        throw new Exception($"The message {i} is too large to fit in the batch.");
    }
}
try
{
    await sender.SendMessagesAsync(messageBatch);
    Console.WriteLine($"A batch of {numOfMessages} messages has been published to the queue.");
}
finally
{
    await sender.DisposeAsync();
    await client.DisposeAsync();
}