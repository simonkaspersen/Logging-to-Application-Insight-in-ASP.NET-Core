using System;
using LightInject;
using Logging_to_Application_Insight_in_ASP.NET_Core.Logging;

namespace Logging_to_Application_Insight_in_ASP.NET_Core
{
    public class CompositionRoot : ICompositionRoot
    {
        public void Compose(IServiceRegistry serviceRegistry)
        {
            serviceRegistry.Register<ILogFactory, NLogFactory>(new PerContainerLifetime());
            serviceRegistry.Register<Type, ILog>((factory,          type) => factory.GetInstance<ILogFactory>().GetLogger(type));
            serviceRegistry.Register<ILogFactoryInitializer, NLogFactoryInitializer>();
            serviceRegistry.RegisterConstructorDependency((factory, info) => factory.GetInstance<Type, ILog>(info.Member.DeclaringType));
        }
        
    }
}