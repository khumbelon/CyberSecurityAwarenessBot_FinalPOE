using System.Security.Cryptography;

namespace CybersecurityAwarenessBot.Data
{
    public static class DatabaseConfig
    {
        // Replace YOUR_PASSWORD on your own computer.
        // Do not publish your real database password in a public repository.
        public static string ConnectionString =
            "Server=localhost;Port=3306;" +
            "Database=CyberSecurityBotDB;" +
            "Uid=root;Pwd=Oritonda@05;";
    }
}