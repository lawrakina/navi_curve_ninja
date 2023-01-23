using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace NavySpade._PJ71.Tiles
{
    [Serializable]
    [CreateAssetMenu(menuName = "Game/PJ71/tile")]
    public class TilePreset : TileBase
    {
        public Sprite m_DefaultSprite;
        public Tile TilePrefab;
        
        private Color Color;

        public TileCreationInfo TileInformation;

        public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject instantiatedGameObject)
        {
            if (instantiatedGameObject == null)
                return false;

            var tmpMap = tilemap.GetComponent<Tilemap>();
            var orientMatrix = tmpMap.orientationMatrix;

            var gameObjectTranslation = new Vector3();
            var gameObjectRotation = new Quaternion();
            var gameObjectScale = new Vector3();

            // Fallback to just using the orientMatrix for the translation, rotation, & scale values.
            gameObjectTranslation = new Vector3(orientMatrix.m03, orientMatrix.m13, orientMatrix.m23);
            gameObjectRotation = Quaternion.LookRotation(
                new Vector3(orientMatrix.m02, orientMatrix.m12, orientMatrix.m22),
                new Vector3(orientMatrix.m01, orientMatrix.m11, orientMatrix.m21));
            gameObjectScale = orientMatrix.lossyScale;

            instantiatedGameObject.transform.localPosition = gameObjectTranslation +
                                                             tmpMap.CellToLocalInterpolated(position +
                                                                 tmpMap.tileAnchor);
            
            instantiatedGameObject.transform.localRotation = gameObjectRotation;
            //instantiatedGameObject.transform.localScale = gameObjectScale;

            //var tileInformation = new TileCreationInfo();
            //tileInformation.TeamData = TilemapConfig.Instance.GetSelectedTeamData();
            
            instantiatedGameObject.GetComponent<Tile>()
                .InitTileInTilemap(TileInformation, TileFactory.InitializationCtx, position);
            
            // instantiatedGameObject.hideFlags = HideFlags.None;
            
            return true;
        }
        
        public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
        {
            var iden = Matrix4x4.identity;

            tileData.sprite = m_DefaultSprite;
            tileData.color = Color;
            tileData.gameObject = TilePrefab.gameObject;
            tileData.flags = TileFlags.LockTransform;
            tileData.transform = iden;
        }
    }
}