using System.Collections.Generic;

namespace EventSystem.Runtime.Core.Dispose
{
    public class EventDisposal
    {
        private readonly List<DisposeContainer> _disposeActions = new List<DisposeContainer>();

        public void Add(DisposeContainer container)
        {
            _disposeActions.Add(container);
        }

        public void Dispose()
        {
            while (_disposeActions.Count > 0)
            {
                var container = _disposeActions[0];
                _disposeActions.Remove(container);
                container.Invoke();
            }
        }
    }
}