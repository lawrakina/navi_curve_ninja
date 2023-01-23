using Core.Visual;
using NavySpade.Modules.Saving.Runtime;
using UnityEngine;

namespace NavySpade.Modules.Visual.Runtime.Data
{
    [CustomSerializeReferenceName("Unlock")]
    [CreateAssetMenu(fileName = "Unlockable Visual", menuName = "Game/Unlockable Visual", order = 51)]
    public class UnlockableVisualData : VisualData
    {
        [field: SerializeField] public string ID { get; private set; }
        [field: SerializeField] public bool IsLockedAtStart { get; private set; }

        [Min(0)] [SerializeField] private int levelsToUnlock = 8;

        public Sprite Sprite;
        public Sprite SpriteBackground;

        public string CurrentSelectedID
        {
            get => SaveManager.Load("STATIC_Selected_ID_InProgress", "-1");
            set => SaveManager.Save("STATIC_Selected_ID_InProgress", value);
        }

        public int Progress
        {
            get => SaveManager.Load("PROGRESS_SELECTED_" + ID, 0);
            set => SaveManager.Save("PROGRESS_SELECTED_" + ID, value);
        }

        public bool IsLocked
        {
            get => bool.Parse(SaveManager.Load(ID + "_locked",
                IsLockedAtStart.ToString()));
            set => SaveManager.Save(ID + "_locked", value.ToString());
        }
    }
}