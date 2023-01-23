using System;
using Core.Meta.Analytics;
using NavySpade.Meta.Runtime.Analytics;
using UnityEngine;

namespace NavySpade.Meta.Usage.Analytics.Scripts
{
    public class TrackMaxScoreExample : MonoBehaviour
    {
        [SerializeField] private int _score;

        public event Action<int> ScoreChanged;

       // [TrackingVariable]
        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                ScoreChanged?.Invoke(value);
            }
        }

        private void Start()
        {
            print(Score.GetMemberKey<TrackMaxScoreExample>(nameof(Score)));
        }

        private void OnEnable()
        {
            VariableTracker.StartTrack(Score.GetMemberKey<TrackMaxScoreExample>(nameof(Score)), Score, ref ScoreChanged);
        }

        private void OnDisable()
        {
            VariableTracker.EndTrack(Score.GetMemberKey<TrackMaxScoreExample>(nameof(Score)));
        }

        public void UpdateValue()
        {
            Score = _score;
        }

        public void SaveValue()
        {
            VariableTracker.SaveAllDirty();
        }
    }
}