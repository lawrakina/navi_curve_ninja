using System;
using Core.Damagables;
using UnityEngine;
using UnityEngine.Events;

namespace NavySpade._PJ71.Tiles
{
    public class Tile : MonoBehaviour
    {
        public enum CreationSource
        {
            FromLevelEditor,
            FromRuntimeEditor,
            SpawnedRightNow
        }
        
        public enum TileState
        {
            Open,
            CanUnlock,
            Hidden
        }
        
        public enum TileContentType
        {
            Empty,
            Building,
            Trees,
        }
        
        
        [SerializeField] private TileCreationInfo _info;
        [SerializeField] private GameObject[] _borders;
        // [SerializeField] private TileUnlocker _tileUnlocker;
        [SerializeField] private UnityEvent _onCanUnlock;
        [SerializeField] private UnityEvent _onUnlock;
        [SerializeField] private TileContentPreset[] _contentPresets;
        [SerializeField] private GameObject _tilePreview;

        private ITilesHolder _tilesHolder;
        private TileTeamAttachment _runtimeTeam;
        
        public TileTeamAttachment TeamAttachmentData => _runtimeTeam;
        
        public CreationSource CreateSource { get; private set; }
        
        public TileState CurrentState { get; private set; }
        
        public Vector2Int Pos { get; private set; }

        public Team Team => _runtimeTeam.Team;

        public event Action TeamChanged;

        public event Action<TileState> StateChanged;
        
        public void Init(ITilesHolder tilesHolder)
        {
            _tilesHolder = tilesHolder;
            // _tileUnlocker.Init(this);
            SetState(Team == Team.Neutral ? TileState.Hidden : TileState.Open);
            SetTileContent(TileContentType.Empty);
        }
        
        public virtual void InitTileInTilemap(TileCreationInfo info, TileInitializationCtx ctx, Vector3Int pos)
        {
            if (ctx.IsInvokeFromRuntime == false)
            {
                CreateSource = CreationSource.FromLevelEditor;
            }
            else
            {
                CreateSource = ctx.IsPlayAnimation ? CreationSource.SpawnedRightNow : CreationSource.FromRuntimeEditor;
            }

            _info = info;
            _runtimeTeam = _info.TeamData;
            Pos = new Vector2Int(pos.x, pos.y);
        }

        public void UpdateTeamData(TileTeamAttachment teamAttachment)
        {
            _runtimeTeam = teamAttachment;
            TeamChanged?.Invoke();
        }

        public void TrySetState(TileState state)
        {
            if(CurrentState == TileState.Open)
                return;
            
            SetState(state);
        }
        
        private void SetState(TileState state)
        {
            if(CurrentState == state)
                return;
            
            switch (state)
            {
                case TileState.Open:
                    gameObject.SetActive(true);
                    _onUnlock.Invoke();
                    break;
                case TileState.CanUnlock:
                    gameObject.SetActive(true);
                    _onCanUnlock.Invoke();
                    Array.ForEach(_borders, (b) => b.SetActive(false));
                    break;
                default:
                    gameObject.SetActive(false);
                    Array.ForEach(_borders, (b) => b.SetActive(false));
                    break;
            }
            
            CurrentState = state;
            StateChanged?.Invoke(CurrentState);
        }
        
        public void UpdateBorders()
        {
            _borders[0].SetActive(IsOpen(_tilesHolder.GetNeighboringTile(this, Side.Top)) == false);
            _borders[1].SetActive(IsOpen(_tilesHolder.GetNeighboringTile(this, Side.Right)) == false);
            _borders[2].SetActive(IsOpen(_tilesHolder.GetNeighboringTile(this, Side.Bottom)) == false);
            _borders[3].SetActive(IsOpen(_tilesHolder.GetNeighboringTile(this, Side.Left)) == false);
        }

        private bool IsOpen(Tile tile)
        {
            return tile != null && tile.CurrentState == TileState.Open;
        }

        public void UnlockTile()
        {
            SetState(TileState.Open);
            _tilesHolder.OnUnlockTile(this);
            UpdateTeamData(TilemapConfig.Instance.GetTeamData(Team.Player));
            UpdateBorders();
            
            Tile[] neighboringTiles = _tilesHolder.GetNeighboringTiles(this);
            Array.ForEach(neighboringTiles, (t) =>
            {
                t.TrySetState(TileState.CanUnlock);
                t.UpdateBorders();
            });
        }
        
        public void SetTileContent(TileContentType contentType)
        {
            foreach (var contentPreset in _contentPresets)
            {
                foreach (var visual in contentPreset.Visuals)
                {
                    visual.SetActive(contentType == contentPreset.ContentType);
                }
            }
        }
        
        [Serializable]
        public struct TileContentPreset
        {
            public TileContentType ContentType;
            public GameObject[] Visuals;
        }

        public void ShowTileContentPreview(bool isShow)
        {
            _tilePreview.SetActive(isShow);
        }
    }
}