# Azure

To use Azure an account will need to be setup, after the initial trial period a Pay-As-You-Go account will be used.

## Cost Management
The _Cost Management_ service is a free Azure service that allows the Azure cloud costs to be analysed and optimised.
For managing subscription costs and 

### Azure subscriptions
This area allows the service costs to be viewed for a given subscription.

## Resource Group
A resource group is a collection of resources that share the same lifecycle, permissions and polices.

## App Service
Before being able to create an app service a resource group needs to be created that the app will be assigned to.

To create an app service (particurally a free one, which is an F1 server), go into Azure and serach for App Services.
Click Add and Create a Web App. Select the Resource Group, and fill in the Instance Details.
For a Blazor WebAssembly App choose the Windows Operating System.
Change the Sku and size to Dev/Test > F1.

Unsure if Application Insights has an associated cost so select No under the Monitoring tab.

### Deploying to App Service using Visual Studio
Firstly, the code needs to be in either a GitHub or Azure DevOps source control repo. It will also need to use a deployment service, such as DevOps or GitHub actions.

Then right-click the Project and go Publish > Azure > Azure App Service (Windows)

> Currently Blazor WebAssembly Apps only work on Windows.

Sign into the account this app should be published to. Publish to the created resource group.

The next step is to Deploy the app so it can be accessed. Once deployed it may take a few minutes to register and appear on the website.
