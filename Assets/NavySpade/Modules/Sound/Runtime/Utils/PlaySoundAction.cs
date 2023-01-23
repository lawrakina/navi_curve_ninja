using NavySpade.Modules.Sound.Runtime.Core;
using UnityEngine;
using Utils.Triggers.Actions;

namespace NavySpade.Modules.Sound.Runtime.Utils
{
    /// <summary>
    /// Plays sound by key. Uses <see cref="SoundPlayer"/>.
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
