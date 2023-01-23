using NavySpade.Modules.Configuration.Runtime.SO;
using UnityEngine;


namespace Core.Meta{
    public class GameLogicConfig : ObjectConfig<GameLogicConfig>{
        [field: SerializeField]
        public float SensitivityOffset{ get; set; } = 0.2f;
        [field: SerializeField]
        public float SensitivityToInputMultiplier{ get; set; } = 6f;
        [field: SerializeField]
        public float MissileSpeed{ get; set; } = 20f;
        [field: SerializeField]
        public float CritilacVelocityForTraps{ get; set; } = 0.05f;
        [field: SerializeField]
        public float SlowMoScale{ get; set; } = 0.3f;
    }
}