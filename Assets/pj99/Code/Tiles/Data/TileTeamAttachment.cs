using Core.Damagables;
using UnityEngine;

namespace NavySpade._PJ71.Tiles
{
    [CreateAssetMenu(menuName = "Game/PJ71/tile team data")]
    public class TileTeamAttachment : ScriptableObject
    {
        public Team Team;
        public Material TeamMaterial;
        public Material TeamMaterial2;
    }
}