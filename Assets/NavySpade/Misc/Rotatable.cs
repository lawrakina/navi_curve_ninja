using UnityEngine;
using DG.Tweening;

public class Rotatable : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float _speed = 0.2f;
    [SerializeField] private Vector3 _direction = new Vector3(0, 360, 0);

    private void OnEnable()
    {
        var duration = 1f / _speed;

        transform.DOLocalRotate(_direction, duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1);
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
