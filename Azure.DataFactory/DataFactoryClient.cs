using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Microsoft.Rest;
using Microsoft.Azure.Management.ResourceManager;
using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace Azure.DataFactory
{
    public static class Client
    {

       static string tenantID = "<your tenant ID>";
       static string applicationId = "<your application ID>";
       static string authenticationKey = "<your authentication key for the application>";
       static string subscriptionId = "<your subscription ID where the data factory resides>";
       static string resourceGroup = "<your resource group where the data factory resides>";
       static string region = "<the location of your resource group>";
       static string dataFactoryName ="<specify the name of data factory to create. It must be globally unique.>";

        public static DataFactoryManagementClient Interface = null;

        public static void InIt()
        {
            tenantID = ConfigurationManager.AppSettings["tenantID"].ToString();
            applicationId = ConfigurationManager.AppSettings["applicationId"].ToString();
            authenticationKey = ConfigurationManager.AppSettings["authenticationKey"].ToString();
            subscriptionId = ConfigurationManager.AppSettings["subscriptionId"].ToString();
            resourceGroup = ConfigurationManager.AppSettings["resourceGroup"].ToString();
            region = ConfigurationManager.AppSettings["region"].ToString();
            dataFactoryName = ConfigurationManager.AppSettings["dataFactoryName"].ToString();

            Authenticate();

        }
        private static void Authenticate()
        {
            var context = new AuthenticationContext("https://login.windows.net/" + tenantID);
            ClientCredential cc = new ClientCredential(applicationId, authenticationKey);
            AuthenticationResult result = context.AcquireTokenAsync(
                "https://management.azure.com/", cc).Result;
            ServiceClientCredentials cred = new TokenCredentials(result.AccessToken);
             Interface = new DataFactoryManagementClient(cred)
            {
                SubscriptionId = subscriptionId
            };
        }


        public static void CreateOrUpdate(string resourceName,PipelineResource pipelineResource)
        {

            if(Interface!=null) Interface.Pipelines.CreateOrUpdate(resourceGroup, dataFactoryName, resourceName, pipelineResource);
        }

        public static void CreateOrUpdate(string resourceName, DatasetResource datasetResource)
        {
            if (Interface != null) Interface.Datasets.CreateOrUpdate(resourceGroup, dataFactoryName, resourceName, datasetResource);
        }



        public static void CreateOrUpdate(string resourceName, LinkedServiceResource linkedServiceResource)
        {
            if (Interface != null) Interface.LinkedServices.CreateOrUpdate(resourceGroup, dataFactoryName, resourceName, linkedServiceResource);
        }
    }
}
