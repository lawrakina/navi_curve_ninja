using NavySpade.Modules.Sound.Runtime.Core;
using UnityEngine;

namespace NavySpade.Modules.Sound.Runtime.Background
{
    /// <summary>
    /// This class manages the background music of the game.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class BackgroundMusic : MonoBehaviour
    {
        public static BackgroundMusic Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
            
            if (SoundPlayer.MusicEnabled == false)
            {
                GetComponent<AudioSource>().mute = true;
            }
        }
    }
}