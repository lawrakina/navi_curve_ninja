using NavySpade.Modules.Sound.Runtime.Core;
using UnityEngine;

namespace Utils.Triggers.Actions
{
    /// <summary>
    /// Plays sound by key. Uses SoundPlayer.
    /// </summary>
    public class PlaySoundAction : ActionBase
    {
        [SerializeField] private string _clipName;

        public override void Fire()
        {
            SoundPlayer.PlaySoundFx(_clipName);
        }
    }
}
