using UnityEngine;

namespace Depra.EventSystem.Tests.Editor
{
    public class UniRxTests : MonoBehaviour
    {
        // private readonly  _disposal = new EventDisposal();
        //
        // [Test]
        // public void Single_Event_Successfully_Invoked()
        // {
        //     var eventARaised = false;
        //     var eventBRaised = false;
        //     
        //     
        //     Core.EventManager.Add("SingleA", () => { eventARaised = true; }).AddTo(_disposal);
        //     Core.EventManager.Add("SingleB", () => { eventBRaised = true; }).AddTo(_disposal);
        //     
        //     Core.EventManager.Invoke("SingleA");
        //     Assert.IsTrue(eventARaised);
        //     
        //     Core.EventManager.Invoke("SingleB");
        //     Assert.IsTrue(eventBRaised);
        //
        //     _disposal.Dispose();
        // }
    }
}
