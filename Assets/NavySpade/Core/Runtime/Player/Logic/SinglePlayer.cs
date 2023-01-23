using UnityEngine;

namespace NavySpade.Core.Runtime.Player.Logic
{
    /// <summary>
    /// класс-метка игрока.
    /// лучше всего не менять этот класс, а сразу декомпозировать логику для последующего реюза
    /// </summary>
    public class SinglePlayer : MonoBehaviour
    {
        public static SinglePlayer Instance { get; private set; }

        private void OnEnable()
        {
            Instance = this;
        }
    }
}