using System;
using Core;
using Core.Player;
using NavySpade.Core.Runtime.Player.Logic;
using NavySpade.Modules.Extensions.UnityStructs;
using UnityEngine;

namespace p21.Entities
{
    public class Booster : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [Range(30, 89)] [SerializeField] private float _angle = 45f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent<SinglePlayer>(out var player) == false)
                return;

            var playerRb = player.GetComponent<Rigidbody>();

            if (playerRb == null)
                throw new NullReferenceException(
                    $"у {player.gameObject.name} отсутсвтует риджетбоди, без него бустер работать не может");
            
            Jump(playerRb);
        }

        public void Jump(Rigidbody rb)
        {
            var velocity = Vector3Extensions.CalculateVelocity(rb.position, _target.transform.position, _angle);

            //cannot be distonate
            if (velocity.IsNaN())
            {
                return;
            }
            
            rb.velocity = velocity;
        }

        private void OnDrawGizmosSelected()
        {
            if(_target == null)
                return;
            
            Gizmos.color = Color.red;
            
            Gizmos.DrawSphere(_target.position, .2f);
            Gizmos.DrawLine(transform.position, _target.position);
        }
    }
}