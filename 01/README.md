# Lab 1: Build a web application on Azure platform as a service offering (Create Azure App Service Web Apps)
  - Azure App Service
  - Azure Storage
  - Azure Container
  - Read/Write files by two web applications
  
## Commands

Get a list of your subscriptions with the az account list command
- `az account list --output table`

Get the current default subscription using show
- `az account show --output table`

To switch to a different subscription, use az account set with the subscription ID or name you want to switch to.
- `az account set --subscription "Subscription Name" or "Subscription ID"`

Implementing solution:

- `az group create --name "az204-01-rg" --location "centralus"`
- `az storage account create --name "imgstorpkolosov" --resource-group "az204-01-rg" --location "centralus" --sku "Standard_ZRS" --kind "StorageV2"`
- `az storage container create --name "images" --account-name "imgstorpkolosov" --public-access "blob"`
- `az appservice plan create --name "imagesserviceplan" --resource-group "az204-01-rg" --sku "F1"`
- `az webapp list-runtimes`
- `az webapp create --resource-group "az204-01-rg" --name "imgapipkolosov" --plan "imagesserviceplan" --runtime '"DOTNETCORE|3.1"'`
- https://imgapipkolosov.azurewebsites.net
- `dotnet publish --configuration Release --output .\bin\publish`
- `Compress-Archive .\bin\publish\* .\app.zip -Force`
- `az webapp deployment source config-zip --resource-group "az204-01-rg" --src "app.zip" --name "imgapipkolosov"`
- `az webapp create --resource-group "az204-01-rg" --name "imgwebpkolosov" --plan "imagesserviceplan" --runtime '"DOTNETCORE|3.1"'`
- `dotnet publish --configuration Release --output .\bin\publish`
- `Compress-Archive .\bin\publish\* .\webapp.zip -Force`
- `az webapp deployment source config-zip --resource-group "az204-01-rg" --src "webapp.zip" --name "imgwebpkolosov"`
- `az group delete --name "az204-01-rg" --yes`

## Comments