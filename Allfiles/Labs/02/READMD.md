# Commands

- `az group create --name "az204-02-rg" --location "centralus"`
- `az storage account create --name "imgstorpkolosov" --resource-group "az204-02-rg" --location "centralus" --sku "Standard_ZRS" --kind "StorageV2"`
- `az functionapp create --resource-group "az204-02-rg" --name "funclogicpkolosov" --runtime "dotnet" --runtime-version "3.1" --os-type "Linux" --storage-account "imgstorpkolosov"` -- does not work
- `func init --worker-runtime dotnet --force`
- `func new --template "HTTP trigger" --name "Echo"`
- `func start --build`
- `httprepl http://localhost:7071`
- `post --content "Hello"`
- `dotnet tool list --global`
- `dotnet tool install -g Microsoft.dotnet-httprepl`
- `func new --template "Timer trigger" --name "Recurring"`
- `az storage container create --name "images" --account-name "imgstorpkolosov" --public-access "blob"`
- `func new --template "HTTP trigger" --name "GetSettingInfo"`
- `func extensions install --package Microsoft.Azure.WebJobs.Extensions.Storage --version 4.0.4`
- `func azure functionapp publish funclogicpkolosov`
- `httprepl https://funclogicpkolosov.azurewebsites.net`
- ` az group delete --name "az204-02-rg" --no-wait --yes`

# Reccuring patterns

- `"0 */5 * * * *"` -- triggers every 5 minutes
- `"*/30 * * * * *"` -- triggers every 30 seconds

# Notes

- To create function, we need to create plan first using https://docs.microsoft.com/en-us/cli/azure/functionapp/plan?view=azure-cli-latest
- httprepl should be installed using `dotnet tool install -g Microsoft.dotnet-httprepl`