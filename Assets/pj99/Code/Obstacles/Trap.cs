using Core.Meta;
using UnityEngine;


namespace pj99.Code.Obstacles{
    public class Trap : MonoBehaviour{
        private Rigidbody _rigidbody;

        private void Awake(){
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnCollisionEnter(Collision other){
            if (other.collider.TryGetComponent(out Enemy enemy)){
                if (_rigidbody.velocity.magnitude >= GameLogicConfig.Instance.CritilacVelocityForTraps){
                    enemy.PreHit();
                    enemy.Hit(new Vector3(transform.position.x,
                        (transform.position.y + enemy.transform.position.y)/2,
                        0f));
                    // enemy.Hit(((enemy.transform.position + transform.position) / 2));
                }
            }
        }
    }
}