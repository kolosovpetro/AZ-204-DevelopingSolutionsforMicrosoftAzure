# Commands

- `az group create --name "az204-07-rg" --location "centralus"`
- `az storage account create --name "securestorpkolosov" --resource-group "az204-07-rg" --location "centralus" --sku "Standard_ZRS" --kind "StorageV2"`
- `az keyvault create --resource-group "az204-07-rg" --name "newsecurevaultpkolosov" --sku "Standard" --location "centralus"`
- `az functionapp create --resource-group "az204-07-rg" --name "securefuncpkolosov" --runtime "dotnet" --runtime-version "3.1" --os-type "Linux" --storage-account "securestorpkolosov" --consumption-plan-location "centralus"`
- `DefaultEndpointsProtocol=https;AccountName=securestorpkolosov;AccountKey=5l3a4kJ0T5PYJmyOV9s9vYyeQk0qqSqkzkbxwzawhZiIyPY2Puda/Fdd5fFLwFRuC0VM0NTCI8tE+AStaZ8Lgg==;EndpointSuffix=core.windows.net`
- System assigned user id: `a8b87147-7df4-4672-a4cb-5b033e78d8a3`
- Secret Identifier: `https://newsecurevaultpkolosov.vault.azure.net/secrets/storagecredentials/0a91b320ac494bf391c7eaa8d8e305dc`
- Create a Key Vault-derived application setting: `@Microsoft.KeyVault(SecretUri=https://newsecurevaultpkolosov.vault.azure.net/secrets/storagecredentials/0a91b320ac494bf391c7eaa8d8e305dc)`
- `func init --worker-runtime dotnet --force`
- `func new --template "HTTP trigger" --name "FileParser"`
- `func start --build`
- `httprepl http://localhost:7071`
- `az storage container create --name "drop" --account-name "securestorpkolosov" --public-access "blob"`
- `func azure functionapp publish "securefuncpkolosov"`
- `func azure functionapp publish "mynewfuncapppkolosov"`
- `az group delete --name "az204-07-rg" --yes`

# Notes

- Due to difference in SDK core 3.1 of azure function and local azure tool >= 4.2 version -- azure function gives error 404