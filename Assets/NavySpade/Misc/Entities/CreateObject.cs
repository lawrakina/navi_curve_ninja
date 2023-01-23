using UnityEngine;

namespace Misc.Entities
{
    public class CreateObject : MonoBehaviour
    {
        public GameObject Prefab;

        public void Invoke()
        {
            Instantiate(Prefab, transform.position, Quaternion.identity);
        }
    }
}