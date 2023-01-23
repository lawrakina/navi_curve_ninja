using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Sets a target GameObject to active or disabled depending on the setting.
    /// </summary>
    public class SetActiveAction : ActionBase
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private bool _value;

        public override void Fire()
        {
            _gameObject.SetActive(_value);
        }
    }
}
