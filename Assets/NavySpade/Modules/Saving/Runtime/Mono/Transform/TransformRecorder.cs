using System.Collections.Generic;
using NaughtyAttributes;
using NavySpade.Modules.Saving.Runtime.Data;
using UnityEngine;

namespace NavySpade.Modules.Saving.Runtime.Mono.Transform
{
    public class TransformRecorder : MonoBehaviour
    {
        [SerializeField] private TransformDataContainer _container;

        [Button]
        public void RecordTransforms()
        {
            if (_container)
            {
                _container.ParentNode = RecordTransformsRecursively(transform);
            }
        }

        [Button]
        public void LoadTransforms()
        {
            if (_container)
            {
                LoadTransformsRecursively(transform, _container.ParentNode);
            }
        }

        private TransformNode RecordTransformsRecursively(UnityEngine.Transform parent)
        {
            var directChildren = GetDirectChildrenOfTransform(parent);

            var nodeData = new TransformNode
            {
                NodeTransformData = new TransformData(parent)
            };

            if (directChildren.Count > 0)
            {
                nodeData.ChildrenTransformsData = new List<TransformNode>();
                for (var i = 0; i < directChildren.Count; i++)
                {
                    nodeData.ChildrenTransformsData.Add(RecordTransformsRecursively(directChildren[i]));
                }
            }
            else
            {
                nodeData.ChildrenTransformsData = null;
            }

            return nodeData;
        }

        private void LoadTransformsRecursively(UnityEngine.Transform parent, TransformNode node)
        {
            var directChildren = GetDirectChildrenOfTransform(parent);

            if (directChildren.Count > 0)
            {
                for (var nodeIndex = 0; nodeIndex < node.ChildrenTransformsData.Count; nodeIndex++)
                {
                    LoadTransformsRecursively(directChildren[nodeIndex], node.ChildrenTransformsData[nodeIndex]);
                }
            }
            
            node.NodeTransformData.ApplyTo(parent);
        }

        private List<UnityEngine.Transform> GetDirectChildrenOfTransform(UnityEngine.Transform parent)
        {
            var directChildren = new List<UnityEngine.Transform>();
            for (var i = 0; i < parent.childCount; i++)
            {
                directChildren.Add(parent.GetChild(i));
            }

            return directChildren;
        }
    }
}