using System.Linq;
using UnityEngine;

namespace NavySpade.Modules.Utils.Helpers
{
    public static class LayerMaskHelper
    {
        public static int OnlyIncluding(params int[] layers)
        {
            return MakeMask(layers);
        }

        public static int Everything()
        {
            return -1;
        }

        public static int Default()
        {
            return 1;
        }

        public static int Nothing()
        {
            return 0;
        }

        public static int EverythingBut(params int[] layers)
        {
            return ~MakeMask(layers);
        }

        public static bool ContainsLayer(LayerMask layerMask, int layer)
        {
            return (layerMask.value & 1 << layer) != 0;
        }

        private static int MakeMask(params int[] layers)
        {
            return layers.Aggregate(0, (current, item) => current | 1 << item);
        }
    }
}
