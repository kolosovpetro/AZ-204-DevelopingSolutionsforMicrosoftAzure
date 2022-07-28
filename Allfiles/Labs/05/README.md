# Commands

- `az group create --name "az204-05-rg" --location "centralus"`
- `az vm create --resource-group "az204-05-rg" --name "quickvm" --image "Debian" --admin-username "razumovsky_r" --admin-password "DRebrEdrAru3#uCatoNA"`
- `az vm show --resource-group "az204-05-rg" --name "quickvm"`
- `az vm list-ip-addresses --resource-group "az204-05-rg" --name "quickvm"`
- `az vm list-ip-addresses --resource-group "az204-05-rg" --name "quickvm" --query '[].{ip:virtualMachine.network.publicIpAddresses[0].ipAddress}' --output tsv`
- `$ipAddress=$(az vm list-ip-addresses --resource-group "az204-05-rg" --name "quickvm" --query '[].{ip:virtualMachine.network.publicIpAddresses[0].ipAddress}' --output tsv)`
- `echo $ipAddress`
- `ssh razumovsky_r@$ipAddress`
- `uname -a`
- `$registryName="pkolosovregistry"`
- `az acr check-name --name $registryName`
- `az acr create --resource-group "az204-05-rg" --name $registryName --sku "Basic"`
- `az acr list`
- `az acr list --query "max_by([], &creationDate).name" --output tsv`
- `$acrName=$(az acr list --query "max_by([], &creationDate).name" --output tsv)`
- `echo $acrName`
- `az acr build --registry $acrName --image ipcheck:latest .`
- `docker build -t vieux/apache:2.0 .`
- `az group delete --name "az204-05-rg" --yes`

# Push container to the ACI via CLI

## Login interactively and set a subscription to be the current active subscription

- `az login`
- `az account set --subscription "Demonstration Account"`


## Demo 0 - Deploy a container from a public registry. dns-name-label needs to be unique within your region.

- `az container create --resource-group psdemo-rg --name psdemo-hello-world-cli-pkolosov --dns-name-label psdemo-hello-world-cli-pkolosov --image mcr.microsoft.com/azuredocs/aci-helloworld --ports 80`


## Show the container info

- `az container show --resource-group "psdemo-rg" --name "psdemo-hello-world-cli-pkolosov"`


## Retrieve the URL, the format is [name].[region].azurecontainer.io

- `$URL=$(az container show --resource-group 'psdemo-rg' --name 'psdemo-hello-world-cli' --query ipAddress.fqdn | tr -d '"') `
- `echo "http://$URL"`
- `echo "http://psdemo-hello-world-cli-pkolosov.centralus.azurecontainer.io"`




## Demo 1 - Deploy a container from Azure Container Registry with authentication

### Step 0 - Set some environment variables and create Resource Group for our demo

- `$ACR_NAME='pkolosovpsdemoacr'` -- must be globally unique over ACR
- `echo $ACR_NAME`


## Step 1 - Obtain the full registry ID and login server which well use in the security and create sections of the demo

- `$ACR_REGISTRY_ID=$(az acr show --name $ACR_NAME --query id --output tsv)`
- `$ACR_LOGINSERVER=$(az acr show --name $ACR_NAME --query loginServer --output tsv)`
- `echo "ACR ID: $ACR_REGISTRY_ID"`
- `echo "ACR Login Server: $ACR_LOGINSERVER"`


## Step 2 - Create a service principal and get the password and ID, this will allow Azure Container Instances to Pull Images from our Azure Container Registry

- `$SP_PASSWD=$(az ad sp create-for-rbac --name http://$ACR_NAME-pull --scopes $ACR_REGISTRY_ID --role acrpull --query password --output tsv)`

## UPDATE 11 JAN 21 - --name has been deprecated by azure cli and now uses display name.

- `$SP_APPID=$(az ad sp list --display-name http://$ACR_NAME-pull --query '[].appId' --output tsv)`
- `echo "Service principal ID: $SP_APPID"`
- `echo "Service principal password: $SP_PASSWD"`


## Step 3 - Create the container in ACI, this will pull our image named

- `az container create --resource-group psdemo-rg --name psdemo-webapp-cli --dns-name-label psdemo-webapp-cli --ports 80 --image $ACR_LOGINSERVER/webappimage:v1 --registry-login-server $ACR_LOGINSERVER --registry-username $SP_APPID --registry-password $SP_PASSWD`


## Step 4 - Confirm the container is running and test access to the web application, look in instanceView.state

- `az container show --resource-group psdemo-rg --name psdemo-webapp-cli`


## Get the URL of the container running in ACI...

- `$URL=$(az container show --resource-group psdemo-rg --name psdemo-webapp-cli --query ipAddress.fqdn | tr -d '"') `
- `echo $URL`
- `curl $URL`


## Step 5 - Pull the logs from the container

- `az container logs --resource-group psdemo-rg --name psdemo-webapp-cli`


## Step 6 - Delete the running container

- `az container delete  --resource-group psdemo-rg --name psdemo-webapp-cli --yes`


## Step 7 - Clean up from our demos, this will delete all of the ACIs and the ACR deployed in this resource group

- `az group delete --name psdemo-rg --yes`
- `docker image rm pkolosovpsdemoacr.azurecr.io/webappimage:v1`
- `docker image rm webappimage:v1`