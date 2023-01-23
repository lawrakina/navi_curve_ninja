using System;
using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    public class RagdollVisuals : MonoBehaviour
    {

    }

    [Serializable]
    public class RagdollVisual
    {
        public bool needSetVisual = true;

        public List<RagdollVisualContainer> visualContainers = new List<RagdollVisualContainer>()
        {
            new RagdollVisualContainer{VisualType = RagdollVisualType.Visual1},
            new RagdollVisualContainer{VisualType = RagdollVisualType.Visual2},
        };
    }

    [Serializable]
    public class RagdollVisualContainer
    {
        public RagdollVisualType VisualType;
        public GameObject VisualObject;
    }

    public enum RagdollVisualType
    {
        Visual1,
        Visual2,
    }
}