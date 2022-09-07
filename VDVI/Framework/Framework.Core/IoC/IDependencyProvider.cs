using Unity;

namespace Framework.Core.IoC
{
    public interface IDependencyProvider
    {
        void RegisterDependencies(IUnityContainer container);
    }
}
