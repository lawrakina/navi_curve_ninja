using Core.Damagables;
using UnityEngine;

namespace Misc.Damagables
{
    /// <summary>
    /// Создаёт эффект при нанесении урона
    /// </summary>
    [RequireComponent(typeof(Damageble))]
    public class DamagebleEffect : MonoBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        [SerializeField] private float _effectLifeTime;
        
        private Damageble _damagable;

        private void Awake()
        {
            _damagable = GetComponent<Damageble>();
        }

        private void OnEnable()
        {
            if (_damagable == null)
            {
                _damagable = GetComponent<Damageble>();
            }

            _damagable.TakeDamage += OnTakeDamage;
        }

        private void OnDisable()
        {
            _damagable.TakeDamage -= OnTakeDamage;
        }

        private void OnTakeDamage(float damage)
        {
            GameObject blood = Instantiate(_effectPrefab, transform.position, Quaternion.identity);
            Destroy(blood, _effectLifeTime);
        }
    }
}