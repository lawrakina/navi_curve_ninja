// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
// using Assert = UnityEngine.Assertions.Assert;
//
// namespace Misc.RootProviders.Tests.Providers
// {
//     public class RootProviderTests : MonoBehaviour
//     {
//         private RootBehavior _rootBehavior;
//         
//         [SetUp]
//         public void Setup()
//         {
//             _rootBehavior = CreateObjectForTest();
//         }
//
//         [TearDown]
//         public void Teardown()
//         {
//             Destroy(_rootBehavior.gameObject);
//         }
//         
//         [UnityTest]
//         public IEnumerator Transform_Root_Can_Be_Disabled_And_Enabled()
//         {
//             _rootBehavior.ChangeType(new TransformRoot());
//             _rootBehavior.Reset();
//             
//             _rootBehavior.Hide();
//             yield return null;
//             Assert.IsFalse(_rootBehavior.IsActive);
//         
//             _rootBehavior.Show();
//             yield return null;
//             Assert.IsTrue(_rootBehavior.IsActive);
//         }
//
//         [UnityTest]
//         public IEnumerator CanvasGroup_Root_Can_Be_Disabled_And_Enabled()
//         {
//             _rootBehavior.gameObject.AddComponent<CanvasGroup>();
//             _rootBehavior.ChangeType(new CanvasGroupRoot());
//             _rootBehavior.Reset();
//             
//             _rootBehavior.Hide();
//             yield return new WaitForSeconds(0.2f);
//             Assert.IsFalse(_rootBehavior.IsActive);
//         
//             _rootBehavior.Show();
//             yield return new WaitForSeconds(0.2f);
//             Assert.IsTrue(_rootBehavior.IsActive);
//         }
//
//         private static RootBehavior CreateObjectForTest()
//         {
//             var obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
//             var rootBehavior = obj.AddComponent<RootBehavior>();
//
//             return rootBehavior;
//         }
//     }
// }
