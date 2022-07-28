# Commands

- `az group create --name "az204-03-rg" --location "centralus"`
- `az storage account create --name "mediastorpkolosov" --resource-group "az204-03-rg" --location "centralus" --sku "Standard_ZRS" --kind "StorageV2"`
- `az storage container create --name "raster-graphics" --account-name "mediastorpkolosov" --public-access "off"`
- `az storage container create --name "compressed-audio" --account-name "mediastorpkolosov" --public-access "off"`
- `dotnet new console --name BlobManager --output .`
- `dotnet add package Azure.Storage.Blobs --version 12.0.0`
- `az group delete --name "az204-03-rg" --yes`

# Notes

- <primary-blob-service-endpoint> is from Storage account blade, in the Settings section, select the Endpoints, copy the value of the Blob Service
- <storage-account-name> is from Storage account blade, in the Security + networking section, select Access keys, copy the Storage account name
- <key> is from Storage account blade, in the Security + networking section, select Access keys, select Show keys, copy the value of either of the Key boxes