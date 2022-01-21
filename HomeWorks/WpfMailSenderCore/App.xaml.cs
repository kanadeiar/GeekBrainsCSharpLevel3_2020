using MailSender.Interfaces;
using MailSender.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Windows;
using WpfMailSenderCore.ViewModels;

namespace WpfMailSenderCore
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static IHost __Hosting;
        public static IHost Hosting
        {
            get
            {
                if (__Hosting != null) 
                    return __Hosting;
                var hostBuilder = Host.CreateDefaultBuilder(Environment.GetCommandLineArgs());
                hostBuilder.ConfigureServices(ConfigureServices);
                return __Hosting = hostBuilder.Build();
            }
        }
        public static IServiceProvider Services => Hosting.Services;
        private static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
#if DEBUG
            services.AddTransient<IMailService, DebugMailService>();
#else
            services.AddTransient<IMailService, SmtpMailService>();
#endif
            services.AddSingleton<IEncryptService, Rfc2898Encryptor>();

            var memoryStore = new InMemoryDataStorage();
            services.AddSingleton<IServerStorage>(memoryStore);
            services.AddSingleton<ISenderStorage>(memoryStore);
            services.AddSingleton<IRecipientStorage>(memoryStore);
            services.AddSingleton<IMessageStorage>(memoryStore);
            //const string dataFileName = "storage.xml";
            //var fileStorage = new InXmlFileDataStorage(dataFileName);
            //services.AddSingleton<IServerStorage>(fileStorage);
            //services.AddSingleton<ISenderStorage>(fileStorage);
            //services.AddSingleton<IRecipientStorage>(fileStorage);
            //services.AddSingleton<IMessageStorage>(fileStorage);
            services.AddTransient<ISchedulerMailService, SchedulerMailService>();

        }
    }
}
