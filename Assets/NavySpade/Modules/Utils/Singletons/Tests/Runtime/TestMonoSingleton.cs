// namespace NavySpade.Modules.Utils.Singletons.Tests.Runtime
// {
//     public class TestMonoSingleton : MonoSingleton<TestMonoSingleton>
//     {
//         public bool InitializedSuccessfully;
//         public bool AwakeCalled;
//         public bool OnDestroyCalled;
//
//         protected override void AwakeOverride()
//         {
//             AwakeCalled = true;
//         }
//
//         protected override void InitializeOverride()
//         {
//             InitializedSuccessfully = true;
//         }
//         
//         protected override void OnDestroyOverride()
//         {
//             OnDestroyCalled = true;
//         }
//     }
// }