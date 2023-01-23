using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.UI.Counters
{
    public class FallingCounter : CounterViewBase
    {
        [Min(0f)] [SerializeField] private float _duration = 0.7f;
        [SerializeField] private SpawnParameters _spawn = new SpawnParameters();
        [SerializeField] private OptionalSettings _optional = new OptionalSettings();

        public override void UpdateValue(int value)
        {
            var preparedText = PrepareValue(value);
            SpawnFallingText(preparedText, _optional.color);
        }

        private void SpawnFallingText(string text, Color color)
        {
            var targetPos = _spawn.spawnPoint.position;
            targetPos.x += Random.Range(_spawn.ranges.x.x, _spawn.ranges.x.y);
            targetPos.y += Random.Range(_spawn.ranges.y.x, _spawn.ranges.y.y);
            targetPos.z += Random.Range(_spawn.ranges.z.x, _spawn.ranges.z.y);

            var fallingText = Instantiate(_spawn.prefab);

            fallingText.transform.position = targetPos;
            fallingText.SetText(text);
            fallingText.SetColor(color);
            fallingText.FadeOut();

            Destroy(fallingText.gameObject, _duration);
        }

        [Serializable]
        private class SpawnParameters
        {
            [Serializable]
            public class Ranges
            {
                public Vector2 x = new Vector2(-1.5f, 1.5f);
                public Vector2 y = new Vector2(0f, 0.5f);
                public Vector2 z = new Vector2(-1.5f, 1.5f);
            }

            public FallingText prefab;
            public Transform spawnPoint;
            public Ranges ranges;
        }

        [Serializable]
        private class OptionalSettings
        {
            public Color color = Color.white;
        }
    }
}