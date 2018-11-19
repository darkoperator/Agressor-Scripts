
using System;
using Windows.Security.Credentials;

namespace passvault
{
    class Program
    {
        static void Main(string[] args)
        {

            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            
            if (!identity.IsSystem)
            {
                Console.WriteLine($"Pulling stored credentials from the Password Vault for current user {identity.Name}.\n");
                var vault = new PasswordVault();
                var cred_collection = vault.RetrieveAll();
                if (cred_collection.Count > 0)
                {
                    foreach (var password in cred_collection)
                    {
                        password.RetrievePassword();
                        Console.WriteLine("Username: " + password.UserName);
                        Console.WriteLine("Password: " + password.Password);
                        Console.WriteLine("Resource: " + password.Resource);
                        Console.WriteLine();
                    }
                }
                else
                {
                    Console.WriteLine("No credentials where stored on the password vault.");
                }
            }
            else
            {
                Console.WriteLine("Running as SYSTEM, must be ran under the context of a user!");
            }
        }
    }
}
