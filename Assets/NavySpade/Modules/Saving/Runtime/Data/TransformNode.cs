using System;
using System.Collections.Generic;

namespace NavySpade.Modules.Saving.Runtime.Data
{
    [Serializable]
    public struct TransformNode
    {
        public TransformData NodeTransformData;
        public List<TransformNode> ChildrenTransformsData;
    }
}