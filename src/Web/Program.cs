using Web;

public class Program
{
    static void Main(string[] args)
            => CreateHostBuilder(args).Build().Run();


    static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(
                webBuilder => webBuilder.UseStartup<Startup>());

}


