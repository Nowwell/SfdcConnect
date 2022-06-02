/****************************************************************************
*
*   File name: SfdcDataProtection.cs
*   Author: Sean Fife
*   Create date: 5/27/2022
*   Solution: SfdcConnect
*   Project: SfdcConnect
*   Description: Encryption and Decryption services
*
****************************************************************************/
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

namespace SfdcConnect
{
    internal class SfdcDataProtection
    {

        readonly IDataProtectionProvider rootProvider;
        public SfdcDataProtection()
        {
            ServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDataProtection();
            rootProvider = serviceCollection.BuildServiceProvider().GetDataProtectionProvider();
        }

        public SfdcDataProtection(IDataProtectionProvider rootProvider)
        {
            this.rootProvider = rootProvider;
        }

        public string Encrypt(string clearText)
        {
            
            IDataProtector protector = rootProvider.CreateProtector("SfdcConnect.SfdcDataProtection.v" + System.Environment.Version.ToString() + "." + System.Environment.MachineName.ToString());
            return protector.Protect(clearText);
        }

        public string Decrypt(string cipherText)
        {
            IDataProtector protector = rootProvider.CreateProtector("SfdcConnect.SfdcDataProtection.v" + System.Environment.Version.ToString() + "." + System.Environment.MachineName.ToString());
            return protector.Unprotect(cipherText);
        }


    }
}
