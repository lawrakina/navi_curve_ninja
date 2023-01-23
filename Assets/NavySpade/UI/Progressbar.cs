using UnityEngine;

namespace Core.UI
{
    public class Progressbar : MonoBehaviour
    {
        public GameObject bar;
        public GameObject barBg;
        Vector3 initBarScale;

        [HideInInspector] public float maxPoints;
        [HideInInspector] public float curPoints;

        void Start()
        {
            initBarScale = bar.transform.localScale;

            if (maxPoints != default(float) && curPoints != default(float)) SetupProgressbar(maxPoints, curPoints);
        }

        public void SetupProgressbar(float _maxPoints, float _curPoints = -1)
        {
            if (maxPoints != _maxPoints)
            {
                maxPoints = _maxPoints;

                if (_curPoints == -1) curPoints = maxPoints;
                else curPoints = _curPoints;

                UpdateProgressbar(curPoints);
            }
        }

        public void UpdateProgressbar(float points)
        {
            barBg.SetActive(points > 0);
            //var coef = points * 100.0 / init_points
            //var change_percents = coef / 100.0 # 100 => 1.0
            //var bar_region_rect = bar.get_region_rect()
            //bar_region_rect.size.width = init_bar_width * change_percents

            if (maxPoints == 0)
            {
                return;
            }

            curPoints = points;

            if (curPoints == 0 || curPoints == -1)
            {
                bar.gameObject.SetActive(false);
            }
            else
            {
                bar.gameObject.SetActive(true);
            }

            float coef = points * 100.0f / maxPoints;
            float changePercents = coef / 100.0f; // 100 => 1.0

            if (changePercents == 0) changePercents = 0.1f;

            float newYScale = initBarScale.x * changePercents;

            if (newYScale > 0)
            {
                bar.transform.localScale = new Vector3(
                    newYScale,
                    bar.transform.localScale.y,
                    bar.transform.localScale.z
                );
            }
        }
    }
}