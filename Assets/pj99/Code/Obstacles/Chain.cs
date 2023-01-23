using System;
using Core.Meta;
using pj99.Code.Extensions;
using pj99.Code.Launcher;
using UnityEngine;


namespace pj99.Code.Obstacles{
    public class Chain : MonoBehaviour{
        public void OffJoints(){
            foreach (var joint in transform.parent.GetComponentsInChildren<HingeJoint>()){
                Destroy(joint);
            }
            foreach (var collider in transform.parent.GetComponentsInChildren<Collider>())
            {
                collider.enabled = true;
            }

            ResourceLoader.InstantiateObject(PrefabConfig.Instance.Fx_HitToObstacle, transform.position,
                Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}