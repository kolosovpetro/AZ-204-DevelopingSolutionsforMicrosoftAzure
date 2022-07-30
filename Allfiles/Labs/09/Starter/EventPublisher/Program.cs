// See https://aka.ms/new-console-template for more information

using Azure;
using Azure.Messaging.EventGrid;

Console.WriteLine("Hello, World!");

const string topicEndpoint = "https://hrtopicpkolosov.centralus-1.eventgrid.azure.net/api/events";
const string topicKey = "XmGTLo6YtT+EwX0tI/o8qAZbjkoBBaL2HZuPgPsZO2s=";

var endpoint = new Uri(topicEndpoint);

var credential = new AzureKeyCredential(topicKey);

var client = new EventGridPublisherClient(endpoint, credential);

var firstEvent = new EventGridEvent(
    subject: "New Employee: Alba Sutton",
    eventType: "Employees.Registration.New",
    dataVersion: "1.0",
    data: new
    {
        FullName = "Alba Sutton",
        Address = "4567 Pine Avenue, Edison, WA 97202"
    }
);

var secondEvent = new EventGridEvent(
    subject: "New Employee: Alexandre Doyon",
    eventType: "Employees.Registration.New",
    dataVersion: "1.0",
    data: new
    {
        FullName = "Alexandre Doyon",
        Address = "456 College Street, Bow, WA 98107"
    }
);

await client.SendEventAsync(firstEvent);
Console.WriteLine("First event published");

await client.SendEventAsync(secondEvent);
Console.WriteLine("Second event published");