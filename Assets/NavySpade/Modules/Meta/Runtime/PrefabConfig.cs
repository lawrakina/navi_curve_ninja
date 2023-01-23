using NavySpade.Modules.Configuration.Runtime.SO;
using UnityEngine;


namespace Core.Meta{
    public class PrefabConfig : ObjectConfig<PrefabConfig>{
        [field: SerializeField]
        public GameObject Fx_HitToObstacle{ get; set; }
        [field: SerializeField]
        public GameObject Fx_HitToEnemy{ get; set; }
    }
}