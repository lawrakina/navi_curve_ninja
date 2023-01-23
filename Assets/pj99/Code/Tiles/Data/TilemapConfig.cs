using System.Linq;
using Core.Damagables;
using NavySpade.Modules.Configuration.Runtime.SO;

namespace NavySpade._PJ71.Tiles
{
    public class TilemapConfig : ObjectConfig<TilemapConfig>
    {
        public const string PathToTileTeams = "Assets/_PJ71/SO/Teams";

        public int SelectedIndex;
        public TileTeamAttachment[] TileTeams;

        public TileTeamAttachment GetSelectedTeamData()
        {
            if (SelectedIndex >= TileTeams.Length)
                return null;
            
            return TileTeams[SelectedIndex];
        }

        public TileTeamAttachment GetTeamData(Team team)
        {
            return TileTeams.FirstOrDefault((t) => t.Team == team);
        }
    }
}