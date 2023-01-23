using System;
using Misc.Effects;
using NavySpade.Modules.Configuration.Runtime.SO;
using UnityEngine;

namespace Misc.FakeShadow
{
    [HelpURL("https://docs.google.com/document/d/1pOku7G-X-U1qHPqZLVki4D_UomUsONdF8uoXV8D33Ts/edit#heading=h.3r4dh5onqabh")]
    public class FakeShadow : MonoBehaviour
    {
        [Flags]
        public enum RotationMode
        {
            None = 0,
            X = 1 << 1,
            Y = 1 << 2,
            Z = 1 << 3,
            All = X | Y | Z
        }

        public RotationMode RotationAxis = RotationMode.None;

        private Vector3 _initialScale;
        private Transform _transform;

        private static FakeShadowConfig _config;

        private void Awake()
        {
            _initialScale = transform.localScale;

            if (_config == null)
                _config = FakeShadowConfig.Instance;

            _transform = transform;
        }

        private void Update()
        {
            var position = _transform.parent.position;

            _transform.position = new Vector3(position.x, position.y + _config.YOffset, position.z);

            if (RotationAxis == RotationMode.None)
            {
                _transform.rotation = Quaternion.identity;
            }
            else
            {
                var angles = _transform.localEulerAngles;
                _transform.localRotation = Quaternion.Euler(
                    (RotationAxis & RotationMode.X) != RotationMode.X ? 0 : angles.x,
                    (RotationAxis & RotationMode.Y) != RotationMode.Y ? 0 : angles.y,
                    (RotationAxis & RotationMode.Z) != RotationMode.Z ? 0 : angles.z
                );
            }


            var distance = Mathf.Abs(position.y - _config.YOffset);

            _transform.localScale = _initialScale * _config.ScaleByDistance.Evaluate(distance);
        }
    }
}