# ProfileMgrApp

CSE Challenge #1 - Profile Manager with Face Detection

## Future Improvements

* Integrate with App Insights
* Store secrets / connection strings in Azure Key Vault
* Store image files in Azure Blob storage

## Azure Deployment 

Azure CLI:

gitrepo=https://github.com/danielwnn/ProfileMgrApp.git

webappname=mywebapp$RANDOM

az login

az group create --location westus --name RG-ProfileMgrApp

az appservice plan create --name $webappname --resource-group RG-ProfileMgrApp --sku FREE

az webapp create --name $webappname --resource-group RG-ProfileMgrApp --plan $webappname

az webapp deployment source config --name $webappname --resource-group RG-ProfileMgrApp --repo-url $gitrepo --branch master --manual-integration

## Sample Site
[https://profilemgrapp.azurewebsites.net](https://profilemgrapp.azurewebsites.net)

## License
MIT license
