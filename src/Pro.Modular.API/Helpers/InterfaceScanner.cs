using System.Reflection;
using Pro.Modular.Shared.Interfaces;

namespace Pro.Modular.Shared;

public static class InterfaceScanner
{
    public static IEnumerable<IModule> DiscoverModules()
    {
        var directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        var moduleInterfaceType = typeof(IModule);

        var filesToProcess = Directory.EnumerateFiles(directoryPath!, "*.dll", SearchOption.TopDirectoryOnly);

        List<Assembly> files = [];
        files.AddRange(from file in filesToProcess
            where file.EndsWith("Module.dll", StringComparison.InvariantCultureIgnoreCase)
            select Assembly.LoadFile(file));
        
        if (files is null) throw new Exception("No assemblies found.");

        var modules = files
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && moduleInterfaceType.IsAssignableFrom(type))
            .Select(type => Activator.CreateInstance(type) as IModule);

        if (modules is null) throw new Exception("No modules found.");

        return modules;
    }
}