using NavySpade.Modules.Saving.Runtime.Data;
using NavySpade.Modules.Saving.Runtime.Interfaces;
using UnityEngine;

namespace NavySpade.Modules.Saving.Runtime.Mono.Transform
{
    public class SaveableTransform : MonoBehaviour, ISaveable
    {
        public TransformData LastData { get; private set; }
        
        public object CaptureState()
        {
            LastData = new TransformData(transform);
            return LastData;
        }

        public void RestoreState(object state)
        {
            LastData = (TransformData)state;
            SetState(LastData);
        }

        public void SetState(TransformData data)
        {
            transform.SetPositionAndRotation(data.LocalPosition, data.LocalRotation);
            transform.localScale = data.LocalScale;
        }
    }
}