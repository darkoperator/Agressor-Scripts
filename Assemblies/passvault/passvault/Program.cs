
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
                    Console.WriteLine($"{"Username",-50}    {"Password",50}   {"Resource",50}");
                    string separator = new string('-', 158);
                    Console.WriteLine(separator);
                    foreach (var password in cred_collection)
                    {
                        password.RetrievePassword();
                        Console.WriteLine($"{password.UserName,-50} || {password.Password, 50} || {password.Resource,50}");

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
