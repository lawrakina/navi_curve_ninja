using Core.UI.Screens.Base;
using UnityEngine;

namespace Core.UI.Popups.Graph
{
    public class UIGraphExecutor : MonoBehaviour
    {
        public PopupsGraph UIGraph;
        
        [field: SerializeField] public BaseScreen DrawingRoot { get; private set; }

        public bool IsAutoRun = true;

        private void Awake()
        {
            UIGraph.Initialize(DrawingRoot);
        }

        private void Start()
        {
            if(IsAutoRun == false)
                return;
            
            Run();
        }

        public void Run()
        {
            UIGraph.RunFromStart();
        }

        private void OnDestroy()
        {
            UIGraph.Dispose();
        }
    }
}