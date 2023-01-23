using UnityEngine;

namespace Misc.Fadeable
{
    public class LevelFadeableProvider : ComponentProvider<FadeableRenderer>
    {
        [field: SerializeField] public override FadeableRenderer[] Components { get; protected set; }
    }
}