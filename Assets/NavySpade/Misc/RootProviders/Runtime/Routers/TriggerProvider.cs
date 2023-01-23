using System;

namespace Depra.Toolkit.RootProviders.Runtime.Routers
{
    public interface IActionLifetimeRouter
    {
        void Init(Action onStarted, Action onCompleted);
    }
}