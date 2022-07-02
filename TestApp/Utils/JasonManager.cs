using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace TestApp.Utils
{
    public class JasonManger
    {
        static IConfigurationRoot? configuration;
        public static void SetJeson()
        {
            IConfigurationBuilder configbuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            configuration = configbuilder.Build();
        }
        public static string GetConnectionString() => configuration.GetConnectionString("DefaultConnection");

        public static string GetStringSection(string sectionName) => configuration.GetSection(sectionName).Value;
        public static bool GetBoolSection(string sectionName) => Convert.ToBoolean(configuration.GetSection(sectionName).Value);
    }
}