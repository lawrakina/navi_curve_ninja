using System;
using System.ComponentModel;
using NUnit.Framework;

namespace Depra.EventSystem.Tests.Editor
{
    public class Sandbox
    {
        [Test]
        public void Event_UserName_PropertyChangedWillBeFired()
        {
            var testClass = new TestMyClassWithINotifyPropertyChangedInterface();
            AssertPropertyChanged(testClass, (x) => x.Id = 666, "UserName");
        }
        
        private static void AssertPropertyChanged<T>(T instance, Action<T> actionPropertySetter, string expectedPropertyName) where T : INotifyPropertyChanged
        {
            string actual = null;
            instance.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                actual = e.PropertyName;
            };
            actionPropertySetter.Invoke(instance);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedPropertyName, actual);
        }
    }
}