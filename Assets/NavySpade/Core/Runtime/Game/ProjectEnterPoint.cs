using Core.Loading.Example;
using Core.Loading.Operations;
using UnityEngine;

namespace NavySpade.Core.Runtime.Game
{
    public class ProjectEnterPoint : MonoBehaviour
    {
        [SerializeField] private ProjectContext _projectContext;

        public void Start()
        {
            _projectContext.Init();

            ILevelLoadAsyncOperation[] operations = new ILevelLoadAsyncOperation[]
            {
                new AcceptEULA(),
                new LoadingUiScene(),
                new LoadingGameScene(),
            };

            ProjectContext.Instance.LoadingScreen.Execute(operations);
        }
    }
}