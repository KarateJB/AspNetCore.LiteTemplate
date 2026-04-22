using System.Collections;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.FileProviders;
using Scriban;
using Scriban.Runtime;

namespace webapi.Extensions;

public static class ConfigurationExtension
{
    public static IHostBuilder AddBasicConfiguration(this IHostBuilder builder, string[] args)
    {
        builder.ConfigureAppConfiguration((context, config) =>
                {
                    var env = context.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                        .AddEnvironmentVariables(); // Load env variables
                });
        return builder;
    }

    public static IHostBuilder AddCustomConfiguration(this IHostBuilder builder, string[] args)
    {
        builder.ConfigureAppConfiguration((context, config) =>
            {
                config.Sources.Clear();

                var env = context.HostingEnvironment;

                config.AddJsonFileWithEnvVar("appsettings.json", false)
                        .AddJsonFileWithEnvVar($"appsettings.{env.EnvironmentName}.json", true);

                if (env.IsDevelopment())
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly != null)
                    {
                        config.AddUserSecrets(appAssembly, optional: true);
                    }
                }

                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            });

        return builder;
    }

    public static IConfigurationBuilder AddJsonFileWithEnvVar(this IConfigurationBuilder configBuilder, string path, bool optional)
    {
        var fileInfo = configBuilder.GetFileProvider().GetFileInfo(path);

        switch (fileInfo.Exists)
        {
            case bool fileExist when !fileExist && !optional:
                throw new FileNotFoundException($"The configuration file '{fileInfo.PhysicalPath}' was not found.");
            case bool fileExist when !fileExist && optional:
                return configBuilder;
            default:
                var stream = ReadJsonFileWithEnvVar(fileInfo);
                configBuilder.AddJsonStream(stream);
                return configBuilder;
        }
    }

    private static MemoryStream ReadJsonFileWithEnvVar(IFileInfo fileInfo)
    {
        var environmentVariables = Environment.GetEnvironmentVariables();

        using var fileStream = fileInfo.CreateReadStream();
        using var reader = new StreamReader(fileStream, Encoding.UTF8);

        var configJson = reader.ReadToEnd();

        configJson = RenderEnvVars(environmentVariables, configJson);

        return new MemoryStream(Encoding.UTF8.GetBytes(configJson));
    }

    private static string RenderEnvVars(IDictionary environmentVariables, string configJson)
    {
        var template = Template.Parse(configJson);
        string renderedJson = template.Render(environmentVariables);
        return renderedJson;
    }

    [Obsolete]
    private static string RenderEnvVars_Deprecated(IDictionary environmentVariables, string configJson)
    {
        var template = Template.Parse(configJson);
        var scriptObject = new ScriptObject();

        foreach (DictionaryEntry entry in environmentVariables)
        {
            var key = entry.Key?.ToString();
            if (!string.IsNullOrEmpty(key))
            {
                scriptObject.Add(key, entry.Value);
            }
        }

        var context = new TemplateContext();
        context.PushGlobal(scriptObject);
        string renderedJson = template.Render(context);
        return renderedJson;
    }
}
