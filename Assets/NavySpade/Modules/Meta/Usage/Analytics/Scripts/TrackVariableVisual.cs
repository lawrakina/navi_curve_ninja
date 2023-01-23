using Core.Meta.Analytics;
using NavySpade.Meta.Runtime.Analytics;
using TMPro;
using UnityEngine;

namespace NavySpade.Meta.Usage.Analytics.Scripts
{
    public class TrackVariableVisual : MonoBehaviour
    {
        public TrackingVariable Variable;

        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void Start()
        {
            if (VariableTracker.TryGetData(Variable.Key, out var data))
            {
                VariableTrackerOnDataChange(data);
            }
            else
            {
                print("data not found!");
            }
        }

        private void OnEnable()
        {
            VariableTracker.DataChange += VariableTrackerOnDataChange;
        }

        private void OnDisable()
        {
            VariableTracker.DataChange -= VariableTrackerOnDataChange;
        }

        private void VariableTrackerOnDataChange(VariableData data)
        {
            if (data.Key != Variable.Key)
            {
                return;
            }

            _text.text = $"current:{data.CurrentIntValue} \nmax:{data.MaxIntValue} \nall sum:{data.AddIntValue}";
        }
    }
}