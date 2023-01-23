using NavySpade.Modules.Configuration.Runtime.SO;
using UnityEngine;

namespace Misc.Effects
{
    public class FakeShadowConfig : ObjectConfig<FakeShadowConfig>
    {
        [SerializeField] private float _yOffset = 0.1f;
        [SerializeField] private AnimationCurve _scaleByDistance = default;

        public float YOffset => _yOffset;
        public AnimationCurve ScaleByDistance => _scaleByDistance;
    }
}