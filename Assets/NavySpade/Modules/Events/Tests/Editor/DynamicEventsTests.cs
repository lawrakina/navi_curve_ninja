using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using NUnit.Framework;

namespace Depra.EventSystem.Tests.Editor
{
    public class DynamicEventsTests
    {
        private EventDisposal _disposal;

        [SetUp]
        public void Setup()
        {
            _disposal = new EventDisposal();
        }

        [TearDown]
        public void Teardown()
        {
            _disposal.Dispose();
        }
        
        [Test]
        public void Dynamic_Event_Successfully_Invoked()
        {
            var eventARaised = false;
            var eventBRaised = false;
        
            DynamicEventManager.Add("DynamicA", dynamic => { eventARaised = true; }).AddTo(_disposal);
            DynamicEventManager.Add("DynamicB", dynamic => { eventBRaised = true; }).AddTo(_disposal);
        
            DynamicEventManager.InvokeArray("DynamicA");
            Assert.IsTrue(eventARaised);
        
            DynamicEventManager.InvokeArray("DynamicB");
            Assert.IsTrue(eventBRaised);
        }
    }
}