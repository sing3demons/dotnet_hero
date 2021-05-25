using System;
using System.Security.Cryptography;
using System.Threading.Tasks;
using dotnet_hero.Data;
using dotnet_hero.Entities;
using dotnet_hero.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;

namespace dotnet_hero.Services
{
    public class AccountService : IAccountService
    {
        private readonly DatabaseContext databaseContext;

        public AccountService(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }

        // --> register
        public async Task Register(Account account)
        {
            var existingAccount = await databaseContext.Accounts.SingleOrDefaultAsync(a => a.Username == account.Username);
           
            if (existingAccount != null) throw new Exception("Existung Account");

            account.Password = CreatePasswordHash(account.Password);
            databaseContext.Add(account);
            await databaseContext.SaveChangesAsync();
        }

        public Task Login(string username, string password)
        {
            throw new NotImplementedException();
        }



        private string CreatePasswordHash(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: 10000,
               numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}.{hashed}";
        }


        private bool VarifyPaasword(string hash, string password)
        {
            var parts = hash.Split('.', 2);
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var passwordHash = parts[1];
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: 10000,
               numBytesRequested: 256 / 8));

            bool result = passwordHash == hashed;
            return result;
        }
    }
}
