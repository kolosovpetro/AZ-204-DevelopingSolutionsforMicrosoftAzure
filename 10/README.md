# Lab 10: Asynchronously process messages by using Azure Service Bus Queues (Develop message-based solutions)
- Create service bus namespace using CLI
- Create service bus queue using CLI
- Create console app message sender
- Create console app message receiver

## Commands

- `az group create --name "az204-10-rg" --location "centralus"`
- `az servicebus namespace create --name "pkolosovsbnamespace" --resource-group "az204-10-rg"`
- `az servicebus queue create --name "pkolsovqueue" --namespace-name "pkolosovsbnamespace" --resource-group "az204-10-rg"`
- Service bus connection string: `Endpoint=sb://pkolosovsbnamespace.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=FiN/9YS9qfJD9B9FZq3wCLUkTA236UObPxPgo/HzZZc=`
- `dotnet new console --name MessagePublisher --output .`
- `dotnet add package Azure.Messaging.ServiceBus --version 7.2.1`
- `dotnet new console --name MessageReader --output .`
- `az group delete --name "az204-10-rg" --yes`

## Notes

- `Message processing logics should be inside event handlers`