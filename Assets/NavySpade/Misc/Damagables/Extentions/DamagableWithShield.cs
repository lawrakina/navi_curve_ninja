using System.Collections;
using Core.Damagables;
using Main.Levels;
using NavySpade.Core.Runtime.Levels;
using UnityEngine;

namespace Misc.Damagables
{
    public class DamagableWithShield : Damageble
    {
        [SerializeField] private GameObject _shieldVisual;
        [SerializeField] private float _duration;

        private Coroutine _shieldTimer;

        protected override void OnAwake()
        {
            _shieldVisual.SetActive(false);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            LevelManager.LevelLoaded += ResetData;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            LevelManager.LevelLoaded -= ResetData;
        }

        private void ResetData()
        {
            _shieldVisual.SetActive(false);
            
            if (_shieldTimer != null)
            {
                StopCoroutine(_shieldTimer);
                _shieldTimer = null;
            }
        }

        public override void DealDamage(float damage, Team team, params IDamageParameter[] damageParameters)
        {
            if (_shieldTimer != null)
                return;

            damage = 1;

            base.DealDamage(damage, team, damageParameters);

            if(IsAlive)
                _shieldTimer = StartCoroutine(ShieldTimer());
        }

        private IEnumerator ShieldTimer()
        {
            _shieldVisual.SetActive(true);
            
            yield return new WaitForSeconds(_duration);
            
            _shieldVisual.SetActive(false);
            _shieldTimer = null;
        }
    }
}