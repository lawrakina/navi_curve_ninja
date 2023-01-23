using System;
using UnityEngine;

namespace NavySpade._PJ71.Tiles
{
    [ExecuteInEditMode]
    public class TileRenderer : MonoBehaviour
    {
        public Renderer Renderer;
        public Tile Tile;

        public void Start()
        {
            if(Tile.TeamAttachmentData == null)
                return;

            if (Application.isPlaying)
            {
                Tile.TeamChanged += UpdateVisual;
            }

            UpdateVisual();
        }

        private void OnDestroy()
        {
            if (Application.isPlaying)
            {
                Tile.TeamChanged -= UpdateVisual;
            }
        }

        private void UpdateVisual()
        {
            if (Renderer && Tile.TeamAttachmentData != null)
            {
                Vector3 pos = transform.position;
                bool isEven = (pos.x + pos.z) % 1 == 0;
                Renderer.sharedMaterial = isEven ? Tile.TeamAttachmentData.TeamMaterial : Tile.TeamAttachmentData.TeamMaterial2;
            }
        }
    }
}