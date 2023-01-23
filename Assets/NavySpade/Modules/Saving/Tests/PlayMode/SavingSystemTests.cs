// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
// using Assert = UnityEngine.Assertions.Assert;
//
// namespace NavySpade.Modules.Saving.Tests.PlayMode
// {
//     public class SavingSystemTests
//     {
//         private const string TestKey = "Test";
//
//         private SaveableTransform _testTransform;
//
//         [SetUp]
//         public void Setup()
//         {
//             SaveManager.DeleteAll();
//             _testTransform = new GameObject().AddComponent<SaveableTransform>();
//         }
//
//         [TearDown]
//         public void Teardown()
//         {
//             SaveManager.DeleteAll();
//             Object.Destroy(_testTransform.gameObject);
//         }
//
//         [UnityTest]
//         public IEnumerator Can_Save()
//         {
//             var state = _testTransform.CaptureState();
//             SaveManager.Save(TestKey, state);
//
//             yield return null;
//             Assert.IsTrue(SaveManager.HasKey(TestKey));
//         }
//
//         [UnityTest]
//         public IEnumerator Can_Save_And_Load()
//         {
//             var state = _testTransform.CaptureState();
//             SaveManager.Save(TestKey, state);
//
//             yield return null;
//             
//             var restoredState = SaveManager.Load<object>(TestKey);
//             
//             Assert.IsNotNull(restoredState);
//         }
//
//         [UnityTest]
//         public IEnumerator Can_Delete()
//         {
//             SaveManager.Save("can_Delete_key", _testTransform.CaptureState);
//             yield return null;
//
//             Assert.IsTrue(SaveManager.HasKey("can_Delete_key"));
//
//             SaveManager.DeleteKey("can_Delete_key");
//             yield return null;
//
//             Assert.IsFalse(SaveManager.HasKey("can_Delete_key"));
//         }
//
//         [UnityTest]
//         public IEnumerator Can_Delete_All()
//         {
//             SaveManager.Save("can_Delete_All_key", _testTransform.CaptureState);
//             yield return null;
//
//             Assert.IsTrue(SaveManager.HasKey("can_Delete_All_key"));
//
//             SaveManager.DeleteAll();
//             yield return new WaitForEndOfFrame();
//
//             Assert.IsFalse(SaveManager.GetAllKeys().Any());
//         }
//     }
// }