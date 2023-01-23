using System;
using UnityEngine;

namespace NavySpade.UI.SafeArea
{
    public class SafeArea : MonoBehaviour
    {
        private static readonly float ROUNDED_CORNER_COEFFICIENT = 0.5f;

        [SerializeField] private Type _reactTo = Type.Notch;
        [SerializeField] private Alignment _align = Alignment.None;
        [SerializeField] private Strategies _strategy = Strategies.MoveEverything;
        [SerializeField] private float _factor = 1;
        [SerializeField] private float _offset = 0;

        private bool _apply = true;
        private ScreenOrientation _currentOrientation = default;
        private RectTransform _canvasTransform = null;
        private RectTransform _rectTransform = null;
        private Vector2 _originalOffsetMin;
        private Vector2 _originalOffsetMax;
    
        void Awake()
        {
            _canvasTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();

            _rectTransform = GetComponent<RectTransform>();
            _originalOffsetMin = _rectTransform.offsetMin;
            _originalOffsetMax = _rectTransform.offsetMax;
        }

        //For some reason inside OnAwake Screen.width reports wrong values. So we are applying the notch on the first Update
        void Update()
        {
            ScreenOrientation newOrientation = Screen.orientation;
            if (_apply || _currentOrientation != newOrientation)
            {
                _apply = false;
                _currentOrientation = newOrientation;
                Apply();
            }
        }

        public void Apply()
        {
            var safeArea = SafeAreaController.Instance.Area;
            if (_reactTo == Type.Notch)
            {
                switch (Screen.orientation)
                {
                    case ScreenOrientation.LandscapeLeft:
                        safeArea.Right = 0.0f;
                        break;
                    case ScreenOrientation.LandscapeRight:
                        safeArea.Left = 0.0f;
                        break;
                }
            }
            else if (_reactTo == Type.RoundedCorners)
            {
                safeArea.Left *= ROUNDED_CORNER_COEFFICIENT;
                safeArea.Right *= ROUNDED_CORNER_COEFFICIENT;
            }

            var canvasScale = new Vector2(_canvasTransform.rect.width / Screen.width, _canvasTransform.rect.height / Screen.height);

            var offsetMin = _originalOffsetMin;
            var offsetMax = _originalOffsetMax;

            if (_align.HasFlag(Alignment.Left) && 0 < safeArea.Left)
            {
                var delta = Mathf.Max(safeArea.Left * _factor * canvasScale.x, 0.0f) + _offset;
                if (_strategy == Strategies.MoveEverything || _strategy == Strategies.MoveCloseSide)
                    offsetMin.x = _originalOffsetMin.x + delta;
                if (_strategy == Strategies.MoveEverything || _strategy == Strategies.MoveFarSide)
                    offsetMax.x = _originalOffsetMax.x + delta;
            }
            if (_align.HasFlag(Alignment.Top) && 0 < safeArea.Top)
            {
                var delta = Mathf.Max(safeArea.Top * _factor * canvasScale.y, 0.0f) + _offset;
                if (_strategy == Strategies.MoveEverything || _strategy == Strategies.MoveCloseSide)
                    offsetMin.y = _originalOffsetMin.y - delta;
                if (_strategy == Strategies.MoveEverything || _strategy == Strategies.MoveFarSide)
                    offsetMax.y = _originalOffsetMax.y - delta;
            }
            if (_align.HasFlag(Alignment.Right) && 0 < safeArea.Right)
            {
                var delta = Mathf.Max(safeArea.Right * _factor * canvasScale.x, 0.0f) + _offset;
                if (_strategy == Strategies.MoveEverything || _strategy == Strategies.MoveCloseSide)
                    offsetMax.x = _originalOffsetMax.x - delta;
                if (_strategy == Strategies.MoveEverything || _strategy == Strategies.MoveFarSide)
                    offsetMin.x = _originalOffsetMin.x - delta;
            }
            if (_align.HasFlag(Alignment.Bottom) && 0 < safeArea.Bottom)
            {
                var delta = Mathf.Max(safeArea.Bottom * _factor * canvasScale.y, 0.0f) + _offset;
                if (_strategy == Strategies.MoveEverything || _strategy == Strategies.MoveCloseSide)
                    offsetMin.y = _originalOffsetMin.y + delta;
                if (_strategy == Strategies.MoveEverything || _strategy == Strategies.MoveFarSide)
                    offsetMax.y = _originalOffsetMax.y + delta;
            }

            _rectTransform.offsetMin = offsetMin;
            _rectTransform.offsetMax = offsetMax;
        }
    
        [Flags]
        public enum Alignment
        {
            None = 0,
            Left = 1,
            Right = 2,
            Top = 4,
            Bottom = 8,
        }

        public enum Type
        {
            Notch,
            RoundedCorners,
            SafeArea,
        }

        public enum Strategies
        {
            MoveEverything,
            MoveCloseSide,
            MoveFarSide,
        }
    }
}
