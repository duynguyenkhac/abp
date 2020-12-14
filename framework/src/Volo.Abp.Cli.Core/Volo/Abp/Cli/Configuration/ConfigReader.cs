﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Cli.Configuration
{
    public class ConfigReader : IConfigReader, ITransientDependency
    {
        const string appSettingFileName = "appsettings.json";

        public AbpCliConfig Read(string directory)
        {
            var settingsFilePath = Path.Combine(directory, appSettingFileName);

            if (!File.Exists(settingsFilePath))
            {
                throw new Exception();
            }

            var settingsFileContent = File.ReadAllText(settingsFilePath);

            using (var document = JsonDocument.Parse(settingsFileContent))
            {
                var element = document.RootElement.GetProperty("AbpCli");
                var configText = element.GetRawText();
                var options = new JsonSerializerOptions
                {
                    Converters =
                    {
                        new JsonStringEnumConverter()
                    }
                };
                return JsonSerializer.Deserialize<AbpCliConfig>(configText,options);
            }
        }
    }
}