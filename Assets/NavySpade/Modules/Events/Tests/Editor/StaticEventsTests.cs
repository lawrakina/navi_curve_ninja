using System;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;
using NUnit.Framework;

namespace Depra.EventSystem.Tests.Editor
{
    public class StaticEventsTests
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
        public void Single_Event_Successfully_Invoked()
        {
            var eventARaised = false;
            var eventBRaised = false;

            EventManager.Add("SingleA", () => { eventARaised = true; }).AddTo(_disposal);
            EventManager.Add("SingleB", () => { eventBRaised = true; }).AddTo(_disposal);

            EventManager.Invoke("SingleA");
            Assert.IsTrue(eventARaised);

            EventManager.Invoke("SingleB");
            Assert.IsTrue(eventBRaised);
        }

        [Test]
        public void Func_Event_Successfully_Invoked()
        {
            var eventARaised = false;
            var eventBRaised = false;

            EventManager.Add("FuncA", () => eventARaised = true).AddTo(_disposal);
            EventManager.Add("FuncB", () => eventBRaised = true).AddTo(_disposal);

            EventManager.Invoke("FuncA");
            Assert.IsTrue(eventARaised);

            EventManager.Invoke("FuncB");
            Assert.IsTrue(eventBRaised);
        }

        [Test]
        public void Object_Event_Successfully_Invoked()
        {
            var eventARaised = false;
            var eventBRaised = false;

            EventManager.Add("ObjectsA", objects => { eventARaised = true; }).AddTo(_disposal);
            EventManager.Add("ObjectsB", objects => { eventBRaised = true; }).AddTo(_disposal);

            EventManager.InvokeArray("ObjectsA");
            Assert.IsTrue(eventARaised);

            EventManager.InvokeArray("ObjectsB");
            Assert.IsTrue(eventBRaised);
        }
    }
}