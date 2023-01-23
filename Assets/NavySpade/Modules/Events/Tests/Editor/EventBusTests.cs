using System;
using System.Collections.Generic;
using System.Threading;
using EventSystem.Runtime.Bus;
using EventSystem.Runtime.Bus.Configuration;
using EventSystem.Runtime.Core.Events.Base;
using NUnit.Framework;
using UnityEngine;

namespace Depra.EventSystem.Tests.Editor
{
    public class EventBusTests
    {
        private const string CustomEventName = "Custom Event";
        
        private bool _methodHandlerHit;
        private bool _actionHandlerHit;

        [Test]
        public void Subscribe_And_Publish_Custom_Event_Method_Test()
        {
            var eventBus = new EventBus();
            eventBus.Subscribe<CustomTestEvent>(CustomTestEventMethodHandler);

            Assert.IsFalse(_methodHandlerHit);
            eventBus.Publish(new CustomTestEvent { Name = CustomEventName, Identifier = 1 });
            Assert.IsTrue(_methodHandlerHit);
        }

        [Test]
        public void Subscribe_And_Publish_Async_Custom_Event_Method_Test()
        {
            var eventBus = new EventBus();
            eventBus.Subscribe<CustomTestEvent>(CustomTestEventMethodHandler);

            Assert.IsFalse(_methodHandlerHit);
            eventBus.PublishAsync(new CustomTestEvent { Name = CustomEventName, Identifier = 1 });
            Thread.Sleep(500);
            Assert.IsTrue(_methodHandlerHit);
        }

        [Test]
        public void Subscribe_And_Publish_Custom_Event_Action_Test()
        {
            var eventBus = new EventBus();
            eventBus.Subscribe<CustomTestEvent>(s =>
            {
                Assert.AreEqual("Custom Event 2", s.Name);
                Assert.AreEqual(2, s.Identifier);

                _actionHandlerHit = true;
            });

            Assert.IsFalse(_actionHandlerHit);
            eventBus.Publish(new CustomTestEvent { Name = "Custom Event 2", Identifier = 2 });
            Assert.IsTrue(_actionHandlerHit);
        }

        [Test]
        public void Subscribe_And_Publish_Async_Built_In_Event_Action_Test()
        {
            var eventBus = new EventBus();
            eventBus.Subscribe<PayloadEvent<int>>(s =>
            {
                Assert.AreEqual(999, s.Payload);
                _actionHandlerHit = true;
            });

            Assert.IsFalse(_actionHandlerHit);
            eventBus.PublishAsync(new PayloadEvent<int>(999));
            Thread.Sleep(500);
            Assert.IsTrue(_actionHandlerHit);
        }

        [Test]
        public void Publish_In_Correct_Order_Test()
        {
            var eventBus = new EventBus();

            var customTestEventResults = new List<CustomTestEvent>();
            eventBus.Subscribe<CustomTestEvent>(s => { customTestEventResults.Add(s); });

            eventBus.Publish(new CustomTestEvent { Name = "Custom Test Event", Identifier = 1 });
            eventBus.Publish(new CustomTestEvent { Name = "Custom Test Event", Identifier = 2 });
            eventBus.Publish(new CustomTestEvent { Name = "Custom Test Event", Identifier = 3 });
            eventBus.Publish(new CustomTestEvent { Name = "Custom Test Event", Identifier = 4 });
            eventBus.Publish(new CustomTestEvent { Name = "Custom Test Event", Identifier = 5 });
            eventBus.Publish(new CustomTestEvent { Name = "Custom Test Event", Identifier = 6 });

            Assert.AreEqual(6, customTestEventResults.Count);
            Assert.AreEqual(1, customTestEventResults[0].Identifier);
            Assert.AreEqual(2, customTestEventResults[1].Identifier);
            Assert.AreEqual(3, customTestEventResults[2].Identifier);
            Assert.AreEqual(4, customTestEventResults[3].Identifier);
            Assert.AreEqual(5, customTestEventResults[4].Identifier);
            Assert.AreEqual(6, customTestEventResults[5].Identifier);
        }

        [Test]
        public void Unsubscribe_Test()
        {
            var eventBus = new EventBus();
            var result = eventBus.Subscribe<CustomTestEvent>(s =>
            {
                Assert.Fail("This should not be executed due to unsubscribing.");
            });

            eventBus.Unsubscribe(result.Token);
            eventBus.Publish(new CustomTestEvent { Name = "Custom Event 3", Identifier = 3 });
        }

        [Test]
        public void Publish_Throw_Subscriber_Exception_Test()
        {
            var eventBus = new EventBus(new EventBusConfiguration { ThrowSubscriberException = true });
            bool firstSubscriberHit = false, thirdSubscriberHit = false;
            eventBus.Subscribe<CustomTestEvent>(s => { firstSubscriberHit = true; });
            eventBus.Subscribe<CustomTestEvent>(s => throw new ApplicationException($"Subscriber error"));
            eventBus.Subscribe<CustomTestEvent>(s => { thirdSubscriberHit = true; });
            
            var thrownException = Assert.Throws<ApplicationException>(() =>
            { 
                eventBus.Publish(new CustomTestEvent());
            }); // Subscriber exception is thrown.

            Assert.AreEqual("Subscriber error", thrownException.Message); // Verify correct message from subscriber
            Assert.IsTrue(firstSubscriberHit); // The first subscriber will be hit
            Assert.IsFalse(thirdSubscriberHit); // Third subscriber will not be hit, missed due to thrown exception.
        }
        
        [Test]
        public void Publish_Dont_Throw_Subscriber_Exception_Test()
        {
            var eventBus = new EventBus(); // Default ThrowSubscriberException = false
            bool firstSubscriberHit = false, thirdSubscriberHit = false;
            eventBus.Subscribe<CustomTestEvent>(s => { firstSubscriberHit = true; });
            eventBus.Subscribe<CustomTestEvent>(s => throw new ApplicationException($"Subscriber error"));
            eventBus.Subscribe<CustomTestEvent>(s => { thirdSubscriberHit = true; });
            eventBus.Publish(new CustomTestEvent());

            // No Exception thrown, subscribers are hit.
            Assert.IsTrue(firstSubscriberHit); // The first subscriber will be hit
            Assert.IsTrue(thirdSubscriberHit); // Third subscriber will be hit, because we didn't throw.
        }

        private void CustomTestEventMethodHandler(CustomTestEvent customTestEvent)
        {
            Assert.AreEqual(CustomEventName, customTestEvent.Name);
            Assert.AreEqual(1, customTestEvent.Identifier);
            _methodHandlerHit = true;
        }
    }

    internal class CustomTestEvent : IEvent
    {
        public string Name { get; set; }
        public int Identifier { get; set; }
    }
}