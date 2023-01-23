using System;
using UnityEngine;

namespace NavySpade._PJ71.Tiles
{
    public interface ITilesHolder
    {
        Tile[] GetOccupiedTiles(Vector3 position, Vector3 size);

        void SetStateForNeighboringTiles(Tile tile, Tile.TileState state);

        Tile[] GetNeighboringTiles(Tile tile, bool withCorners = false);
        
        Tile GetNeighboringTile(Tile tile, Side side);
        
        void OnUnlockTile(Tile tile);
        
        T[] GetContent<T>(Func<T, bool> predicate) where T : Component;
        
        Tile[] GetTiles(Func<Tile, bool> func);

        void OpenNeighboringTiles(Tile tile);
    }
}