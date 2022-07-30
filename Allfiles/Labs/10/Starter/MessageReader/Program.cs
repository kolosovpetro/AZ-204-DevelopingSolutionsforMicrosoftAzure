using Azure.Messaging.ServiceBus;

var storageConnectionString =
    "Endpoint=sb://pkolosovsbnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FiN/9YS9qfJD9B9FZq3wCLUkTA236UObPxPgo/HzZZc=";
var queueName = "pkolsovqueue";
ServiceBusClient client;
ServiceBusProcessor processor;

static async Task MessageHandler(ProcessMessageEventArgs args)
{
    var body = args.Message.Body.ToString();
    Console.WriteLine($"Received: {body}");
    await args.CompleteMessageAsync(args.Message);
}

static Task ErrorHandler(ProcessErrorEventArgs args)
{
    Console.WriteLine(args.Exception.ToString());
    return Task.CompletedTask;
}

client = new ServiceBusClient(storageConnectionString);
processor = client.CreateProcessor(queueName, new ServiceBusProcessorOptions());

try
{
    processor.ProcessMessageAsync += MessageHandler;
    processor.ProcessErrorAsync += ErrorHandler;
    await processor.StartProcessingAsync();
    Console.WriteLine("Wait for a minute and then press any key to end the processing");
    Console.ReadKey();
    Console.WriteLine("\nStopping the receiver...");
    await processor.StopProcessingAsync();
    Console.WriteLine("Stopped receiving messages");
}
finally
{
    await processor.DisposeAsync();
    await client.DisposeAsync();
}