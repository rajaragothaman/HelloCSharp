using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Management.DataFactory.Models;
namespace Azure.DataFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Client.InIt();
            Client.CreateOrUpdate("C_ADLS_CON", CreateLakeGen2Connection());
        
        }


        private static LinkedServiceResource CreateLakeGen2Connection()
        {

          
            AzureBlobFSLinkedService azureBlobFSLinkedService = new AzureBlobFSLinkedService();
            azureBlobFSLinkedService.Parameters = new Dictionary<string, ParameterSpecification>()
            {
                { "uri" ,new ParameterSpecification() { Type=ParameterType.String } },
                { "tenant" ,new ParameterSpecification() { Type=ParameterType.String } },
                 { "spn" ,new ParameterSpecification() { Type=ParameterType.String } },
                 { "spnkey" ,new ParameterSpecification() { Type=ParameterType.String } }

            };

           

            azureBlobFSLinkedService.Url = "@{linkedService().uri}";

            azureBlobFSLinkedService.Tenant = "@{linkedService().tenant}";

            azureBlobFSLinkedService.ServicePrincipalId = "@{linkedService().spn}";

            azureBlobFSLinkedService.ServicePrincipalKey = new SecureString("@{linkedService().spnkey}");

            return new LinkedServiceResource(azureBlobFSLinkedService);
        }
    }
}
