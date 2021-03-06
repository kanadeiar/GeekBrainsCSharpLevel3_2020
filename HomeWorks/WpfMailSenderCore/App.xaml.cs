﻿using MailSender.Interfaces;
using MailSender.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
                var host_builder = Host.CreateDefaultBuilder(Environment.GetCommandLineArgs());
                host_builder.ConfigureServices(ConfigureServices);
                return __Hosting = host_builder.Build();
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
            var memory_store = new InMemoryDataStorage();
            services.AddSingleton<IServerStorage>(memory_store);
            services.AddSingleton<ISenderStorage>(memory_store);
            services.AddSingleton<IRecipientStorage>(memory_store);
            services.AddSingleton<IMessageStorage>(memory_store);
            //const string data_file_name = "storage.xml";
            //var file_storage = new InXmlFileDataStorage(data_file_name);
            //services.AddSingleton<IServerStorage>(file_storage);
            //services.AddSingleton<ISenderStorage>(file_storage);
            //services.AddSingleton<IRecipientStorage>(file_storage);
            //services.AddSingleton<IMessageStorage>(file_storage);
        }
    }
}
