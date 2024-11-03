namespace DonChamol.Models.Data
{
    public class BDConnection
    {
        public static string Connection()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"));

            var init = builder.Build();
            var connection = init.GetConnectionString("AppConnectionString");

            return connection;
        }
    }
}
