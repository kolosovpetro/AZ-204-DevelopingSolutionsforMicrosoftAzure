# Lab 4: Construct a polyglot data solution (Develop solutions that use Cosmos DB storage)
- Create azure cosmos db account
- Create storage account (CLI)
- Utilize azure cosmos db via SDK

# Commands

- `az group create --name "az204-04-rg" --location "centralus"`
- `az cosmosdb create --name "Polyglotdata" --resource-group "az204-04-rg" --default-consistency-level Eventual --locations regionName="centralus" failoverPriority=0 isZoneRedundant=False --locations regionName="westus" failoverPriority=1 isZoneRedundant=False` -- does not work
- URI: `https://polycosmospkolosov.documents.azure.com:443/`
- PK: `Z7tIiG02AAJCrDoEMsrdPtJCsoqNxpupsRV2GRUlz0is9Gq6Al8LwRJqcJstlWl1eo7JIDC5f2Ny9T6nlazO2A==`
- PRIMARY CONN STR: `AccountEndpoint=https://polycosmospkolosov.documents.azure.com:443/;AccountKey=Z7tIiG02AAJCrDoEMsrdPtJCsoqNxpupsRV2GRUlz0is9Gq6Al8LwRJqcJstlWl1eo7JIDC5f2Ny9T6nlazO2A==;`
- `az storage account create --name "polystorpkolosov" --resource-group "az204-04-rg" --location "centralus" --sku "Standard_ZRS" --kind "StorageV2"`
- `az storage container create --name "images" --account-name "polystorpkolosov" --public-access "blob"`
- Blob url: `https://polystorpkolosov.blob.core.windows.net/images`
- `dotnet add package Microsoft.Azure.Cosmos --version 3.20.1`
- `az group delete --name "az204-04-rg" --yes`

# Notes

- CLI documentation: https://docs.microsoft.com/en-us/azure/cosmos-db/scripts/cli/sql/create
- URI, PK, CONN STR are from: `On the Azure Cosmos DB account blade, find the Settings section, and then select the Keys link. In the Keys pane, on the Read-write Keys tab, record the values of the URI, PRIMARY KEY, and PRIMARY CONNECTION STRING text boxes. You’ll use these values later in this lab.`