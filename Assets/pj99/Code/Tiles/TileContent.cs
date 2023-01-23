using System;
using UnityEngine;
using UnityEngine.Events;

namespace NavySpade._PJ71.Tiles
{
    public class TileContent : MonoBehaviour
    {
        [SerializeField] private Tile.TileContentType _contentType;
        [SerializeField] private Vector3 size = Vector3.one;
        [SerializeField] private UnityEvent _onOpen;
        [SerializeField] private UnityEvent _onBlocked;
        
        private ITilesHolder _tilesHolder;
        private Tile[] _occupiedTiles;
        
        public void Init(ITilesHolder tilesHolder)
        {
            _tilesHolder = tilesHolder;
            
            _occupiedTiles = tilesHolder.GetOccupiedTiles(transform.position, size);
            
            Array.ForEach(_occupiedTiles, (t) =>
            {
                t.SetTileContent(_contentType);
                t.StateChanged += OnTileOpened;
            });
            
            OnTileOpened(_occupiedTiles[0].CurrentState);
        }

        private void OnTileOpened(Tile.TileState tileState)
        {
            bool canUnlockZone = true;
            foreach (var tile in _occupiedTiles)
            {
                canUnlockZone &= tile.CurrentState == Tile.TileState.Open;
            }
            
            if (canUnlockZone)
            {
                _onOpen?.Invoke();
            }
            else
            {
                _onBlocked?.Invoke();
            }
        }
        
        public void OpenNeighboringTiles()
        {
            Array.ForEach(_occupiedTiles, (t) =>
            {
                _tilesHolder.OpenNeighboringTiles(t);
            });
        }
    }
}