using System;

namespace Users.Common.IoC
{
    public interface IContainer
    {
        void RegisterTransient<Implementation, Interface>();

        Interface GetDependency<Interface>();
        object GetDependency(string identifier);

        object Resolve(Type ctor, string[] identifiers = null, params object[] args);
        object Resolve<Type>(string[] identifiers = null, params object[] args);
    }
}