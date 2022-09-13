# Lab 9: Publish and subscribe to Event Grid events (Develop event-based solutions)
- Create event grid using CLI
- Publish events using console app

## Commands

- `az --version`
- `az --help`
- `az provider --help`
- `az provider list`
- `az provider list --query "[].namespace"`
- `az group create --name "az204-09-rg" --location "centralus"`
- `hrtopicpkolosovnew`
- `https://eventviewerpkolosov.azurewebsites.net/api/updates`
- `https://hrtopicpkolosov.centralus-1.eventgrid.azure.net/api/events`
- KEY1: `XmGTLo6YtT+EwX0tI/o8qAZbjkoBBaL2HZuPgPsZO2s=`
- `dotnet new console --name EventPublisher --output .`
- `dotnet add package Azure.Messaging.EventGrid --version 4.1.0`
- `az group delete --name "az204-09-rg" --yes`

## Notes
