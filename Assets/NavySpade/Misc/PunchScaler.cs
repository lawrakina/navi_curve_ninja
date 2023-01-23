using DG.Tweening;
using UnityEngine;

//todo: delete it after plug new ui scripts
public class PunchScaler : MonoBehaviour
{
    [SerializeField] private Transform _target = default;
    [SerializeField] private Vector2 _strength = new Vector2(0.5f, 0.5f);
    [Range(0f, 2f)]
    [SerializeField] private float _animationDuration = 0.2f;

    private Vector3 _initialScale;

    private void Start()
    {
        _initialScale = _target.transform.localScale;
    }

    private void OnDestroy()
    {
        _target.transform.DOKill();
    }

    public void Scale()
    {
        _target.transform.localScale = _initialScale;
        _target.transform.DOKill();
        _target.transform.DOPunchScale(_strength, _animationDuration, 0);
    }
}