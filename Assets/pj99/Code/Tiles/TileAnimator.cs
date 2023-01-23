using UnityEngine;

namespace NavySpade._PJ71.Tiles
{
    public class TileAnimator : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }
        [field: SerializeField] public Tile Tile { get; private set; }
        
        private void Start()
        {
            if (Tile.CreateSource != Tile.CreationSource.SpawnedRightNow)
            {
                Animator.enabled = false;
                return;
            }
            
            Animator.Play("Create Animation");
        }
    }
}