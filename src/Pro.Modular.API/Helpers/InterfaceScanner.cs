using System.Reflection;
using Pro.Modular.Shared.Interfaces;

namespace Pro.Modular.Shared;

public static class InterfaceScanner
{
    public static IEnumerable<IModule> DiscoverModules()
    {
        var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var moduleInterfaceType = typeof(IModule);

        var files = Directory.EnumerateFiles(directoryPath!, "*.dll", SearchOption.TopDirectoryOnly)
            .Where(f => !Path.GetFileName(f).StartsWith("Microsoft.") && !Path.GetFileName(f).StartsWith("System."))
            .Select(Assembly.LoadFrom);

        if (files is null) throw new Exception("No assemblies found");

        var types = files
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && moduleInterfaceType.IsAssignableFrom(type))
            .Select(type => Activator.CreateInstance(type) as IModule);

        return types;
    }
}