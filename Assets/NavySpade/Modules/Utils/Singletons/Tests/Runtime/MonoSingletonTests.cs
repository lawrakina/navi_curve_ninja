// using System.Collections;
// using NUnit.Framework;
// using UnityEngine;
// using UnityEngine.TestTools;
//
// namespace NavySpade.Modules.Utils.Singletons.Tests.Runtime
// {
//     public class MonoSingletonTests
//     {
//         private TestMonoSingleton _monoSingleton;
//
//         [SetUp]
//         public void Setup()
//         {
//             _monoSingleton = new GameObject().AddComponent<TestMonoSingleton>();
//         }
//
//         [TearDown]
//         public void Teardown()
//         {
//             Object.Destroy(_monoSingleton.gameObject);
//         }
//
//         [UnityTest]
//         public IEnumerator Instance_Not_Exists_Before_Creation()
//         {
//             yield return null;
//             Assert.IsFalse(EmptySingleton.InstanceExists);
//         }
//
//         [UnityTest]
//         public IEnumerator Instance_Exists_After_Creation()
//         {
//             yield return null;
//             Assert.IsTrue(TestMonoSingleton.InstanceExists);
//         }
//         
//         [UnityTest]
//         public IEnumerator Overridden_Awake_Method_Called()
//         {
//             yield return null;
//             Assert.IsTrue(_monoSingleton.AwakeCalled);
//         }
//
//         [UnityTest]
//         public IEnumerator Overridden_Initialization_Method_Called()
//         {
//             yield return null;
//             Assert.IsTrue(_monoSingleton.InitializedSuccessfully);
//         }
//     }
// }