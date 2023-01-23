// using NavySpade.Modules.Configuration.Runtime.SO;
// using UnityEngine.Assertions;
//
// namespace NavySpade.Modules.Configuration.Tests
// {
//     public class ConfigurationTests
//     {
//         [Test]
//         public void Can_Get_Config_By_Type()
//         {
//             Assert.IsNotNull(ObjectConfig.GetConfig(typeof(EditorConfig)));
//         }
//
//         [Test]
//         public void CanGet_Config_By_Generic_Type()
//         {
//             Assert.IsNotNull(ObjectConfig<EditorConfig>.GetConfig());
//         }
//         
//         [Test]
//         public void Editor_Config_Existed()
//         {
//             Assert.IsNotNull(EditorConfig.Instance);
//         }
//     }
// }