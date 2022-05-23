# ProfileMgrApp

 Web App - Employee Profile Manager with Face Detection

## Future Improvements

* Add more logging info
* Add friendly error page for production
* Integrate with App Insights
* Store secrets / connection strings in Azure Key Vault
* Store image files in Azure Blob storage
* Client-side resources are bundled, minified, and potentially served from a CDN

## Configuration

* Please update appsettings.json in both ProfileMgrApp and ProfileMgrApp.Tests folders to have the correct DB connection string and Vision/Face API endpoint / key.

## How to run locally

* At the command line: Change to the project folder - "ProfileMgrApp" and then execute "dotnet run".

## How to run tests

* At the command line: Change to the test project folder - "ProfileMgrApp.Tests" and then execute "dotnet test".
* Visual Studio 2017: Launch Test Explorer (Menu path: Test -> Windows -> Test Explorer)

## Azure Deployment with Azure CLI:

~~~
gitrepo=https://github.com/danielwnn/ProfileMgrApp.git

webappname=mywebapp$RANDOM

az login

az group create --location westus --name RG-ProfileMgrApp

az appservice plan create --name $webappname --resource-group RG-ProfileMgrApp --sku FREE

az webapp create --name $webappname --resource-group RG-ProfileMgrApp --plan $webappname

az webapp deployment source config --name $webappname --resource-group RG-ProfileMgrApp --repo-url $gitrepo --branch master --manual-integration
~~~

## GCP Deployment with Google Cloud Shell

~~~
git clone https://github.com/danielwnn/ProfileMgrApp.git

cd ProfileMgrApp/ProfileMgrApp

docker build . -t gcr.io/<gcp project>/profilemgrapp

docker push gcr.io/<gcp project>/profilemgrapp

gcloud run deploy <service name> --image gcr.io/<gcp project>/profilemgrapp
~~~

## License
MIT license
