using DG.Tweening;
using NavySpade.Modules.Extensions.UnityTypes;
using UnityEngine;

public class UpDownMovement : ExtendedMonoBehavior
{
    public enum Direction
    {
        Up = 0,
        Down = 1,
    }

    [Min(0f)]
    [SerializeField] private float _offset = 1f;
    [Min(0f)]
    [SerializeField] private float _speed = 1f;
    [SerializeField] private bool _randomDelay = true;
    [Range(0f, 5f)]
    [SerializeField] private float _maxDelay = 3f;
    [SerializeField] private Direction _startDirection = Direction.Up;
    [SerializeField] private Ease _ease = Ease.Linear;

    private float _initialPositionY;

    private void OnEnable()
    {
        _initialPositionY = transform.localPosition.y;

        if (_randomDelay == false)
        {
            StartMoveUpDown();
            return;
        }

        var delay = Random.Range(0f, _maxDelay);
        InvokeAtTime(delay, StartMoveUpDown);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }

    private void StartMoveUpDown()
    {
        if (_startDirection == Direction.Up)
            MoveUp();
        else
            MoveDown();
    }

    private void MoveUp()
    {
        var yPosition = _initialPositionY + _offset;
        var duration = (yPosition - transform.localPosition.y) / _speed;
        
        transform.DOMoveY(yPosition, duration)
            .SetEase(_ease)
            .OnComplete(MoveDown);
    }

    private void MoveDown()
    {
        var yPosition = _initialPositionY - _offset;
        var duration = (transform.localPosition.y - yPosition) / _speed;
       
        transform.DOMoveY(yPosition, duration)
            .SetEase(_ease)
            .OnComplete(MoveUp);
    }
}
