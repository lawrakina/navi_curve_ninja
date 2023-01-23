// using NUnit.Framework;
// using UnityEditor;
// using UnityEngine;
//
// namespace NavySpade.Modules.Utils.Singletons.Tests.Editor
// {
//     public class ScriptableObjectSingletonTests
//     {
//         [SetUp]
//         public void Setup()
//         {
//             _ = TestScriptableObjectSingleton.Instance;
//         }
//
//         [TearDown]
//         public void Teardown()
//         {
//             DeleteSingleton();
//         }
//
//         [Test]
//         public void Instance_Not_Null()
//         {
//             Assert.IsNotNull(TestScriptableObjectSingleton.Instance);
//         }
//
//         [Test]
//         public void Instance_Exists()
//         {
//             Assert.IsTrue(TestScriptableObjectSingleton.InstanceExists);
//         }
//
//         private static void DeleteSingleton()
//         {
//             var path = AssetDatabase.GetAssetPath(TestScriptableObjectSingleton.Instance);
//             Debug.Log(path);
//             AssetDatabase.DeleteAsset(path);
//             TestScriptableObjectSingleton.DeinitializeInternal();
//         }
//     }
// }