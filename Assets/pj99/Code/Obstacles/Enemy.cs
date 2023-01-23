using System.Linq;
using System.Threading.Tasks;
using Core.Meta;
using EventSystem.Runtime.Core.Managers;
using pj99.Code.Extensions;
using pj99.Code.Launcher;
using UnityEngine;


namespace pj99.Code.Obstacles{
    internal class Enemy : TargetForThrow{
        [SerializeField]
        private Animator _animator;
        [field: SerializeField]
        public SkeletonMeshSlicer Slicer{ get; set; }
        public bool IsAlive{ get; set; } = true;

        [SerializeField]
        public bool IsBossLevel;
        [SerializeField]
        private Collider _selfCollider;
        
        public void PreHit(){
            _animator.enabled = false;
            IsAlive = false;
        }
        
        private void OnDrawGizmos(){
            if(IsBossLevel)
                DebugExtension.DebugCylinder(transform.position, transform.position + Vector3.up * 3, Color.red, 3f);
        }

        public async void Hit(Vector3 yPos){
            ResourceLoader.InstantiateObject(PrefabConfig.Instance.Fx_HitToEnemy, transform.position + yPos,
                Quaternion.identity);
            foreach (var rigidbody in GetComponentsInChildren<Rigidbody>()){
                rigidbody.isKinematic = false;
            }

            foreach (var collider in GetComponentsInChildren<Collider>()){
                collider.enabled = true;
            }

            _selfCollider.enabled = false;

            if (!IsBossLevel) return;
            Time.timeScale = Level.Instance.TimeScaleAfterWin;
            await Task.Delay(Level.Instance.DelayAfterCuclResult);
            
            if (FindObjectsOfType<Enemy>().Any(enemy => enemy.IsAlive)){
                EventManager.Invoke(MainEnumEvent.Fail);
                return;
            }

            EventManager.Invoke(MainEnumEvent.Win);
        }
    }
}