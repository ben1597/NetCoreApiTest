using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ghl.extension.DependencyInjection
{
    public class DIModuleConfigure
    {
        protected virtual void Load(HostBuilderContext builderContext ,IServiceCollection serviceCollection) { }
    }
    
    /// <summary>
    /// for Console使用
    /// </summary>
    public class ConsoleModuleConfigure
    {
        protected virtual void Load(IServiceCollection serviceCollection,IConfiguration configuration) { }
    }
}