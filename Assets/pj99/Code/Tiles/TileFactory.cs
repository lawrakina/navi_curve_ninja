using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.Tilemaps;

namespace NavySpade._PJ71.Tiles
{
    public struct TileInitializationCtx
    {
        public bool IsInvokeFromRuntime;
        public bool IsPlayAnimation;
    }

    public enum Side
    {
        Left,
        Right,
        Top,
        Bottom,
        LeftTop,
        LeftBottom,
        RightTop,
        RightBottom
    }

    public static class TileFactory
    {
        public static TileInitializationCtx InitializationCtx { get; private set; }

        public static void SetTile(Tilemap tilemap, TilePreset preset, Vector2Int position, bool withAnimation = true)
        {
            var pos = new Vector3Int(position.x, 0, position.y);

            var initializationCtx = InitializationCtx;

            initializationCtx.IsPlayAnimation = withAnimation;
            
            if (Application.isPlaying)
            {
                initializationCtx.IsInvokeFromRuntime = true;
            }

            InitializationCtx = initializationCtx;

            tilemap.SetTile(pos, preset);
            initializationCtx.IsInvokeFromRuntime = false;
            InitializationCtx = initializationCtx;
        }
        
        public static Tile GetTile(Tilemap tilemap, Vector2Int pos, Side side)
        {
            switch (side)
            {
                case Side.Left:
                    return GetTile(tilemap, new Vector2Int(pos.x - 1, pos.y));
                case Side.Right:
                    return GetTile(tilemap, new Vector2Int(pos.x + 1, pos.y));
                case Side.Top:
                    return GetTile(tilemap, new Vector2Int(pos.x, pos.y + 1));
                case Side.Bottom:
                    return GetTile(tilemap, new Vector2Int(pos.x, pos.y - 1));
                case Side.LeftTop:
                    return GetTile(tilemap, new Vector2Int(pos.x - 1, pos.y + 1));
                case Side.LeftBottom:
                    return GetTile(tilemap, new Vector2Int(pos.x - 1, pos.y - 1));
                case Side.RightTop:
                    return GetTile(tilemap, new Vector2Int(pos.x + 1, pos.y + 1));
                case Side.RightBottom:
                    return GetTile(tilemap, new Vector2Int(pos.x + 1, pos.y - 1));
            }

            return null;
        }

        public static Tile GetTile(Tilemap tilemap, Vector2Int position)
        {
            var pos = new Vector3Int(position.x, position.y, 0);
            var obj = tilemap.GetInstantiatedObject(pos);

            if (obj == null)
                return null;

            var tile = obj.GetComponent<Tile>();
            
            return tile == null ? null : tile;
        }
    }
}