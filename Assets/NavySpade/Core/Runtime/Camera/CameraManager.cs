using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using EventSystem.Runtime.Core.Dispose;
using EventSystem.Runtime.Core.Managers;

public class CameraManager : MonoBehaviour
{
    #region Static

    public static CameraManager Instance { get; private set; }

    #endregion

    #region Field

    [field: SerializeField] public List<CinemachineVirtualCameraBase> Cameras { get; private set; }
    public CinemachineBrain Brain { get; private set; }

    #endregion

    private readonly EventDisposal _disposal = new EventDisposal();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Init();
    }

    private void Init()
    {
        Camera.main.gameObject.TryGetComponent<CinemachineBrain>(out var brain);
        if (brain == null)
        {
            brain = Camera.main.gameObject.AddComponent<CinemachineBrain>();
        }

        Brain = brain;


        if (Cameras == null)
        {
            Cameras = new List<CinemachineVirtualCameraBase>();
        }
    }

    public void AddCamera(CinemachineVirtualCameraBase camera)
    {
        if (Cameras.Contains(camera))
        {
            return;
        }

        Cameras.Add(camera);
    }

    public CinemachineVirtualCamera GetActiveCamera()
    {
        return Brain.ActiveVirtualCamera as CinemachineVirtualCamera;
    }

    public void SetFollowTarget(Transform target)
    {
        var currentCamera = GetActiveCamera();
        currentCamera.Follow = target;
    }

    public void SetLookAtTarget(Transform target)
    {
        var currentCamera = GetActiveCamera();
        currentCamera.LookAt = target;
    }

    public void SwitchCamera(CinemachineVirtualCameraBase camera)
    {
        var current = GetActiveCamera();
        var priority = current.Priority;
        camera.Priority = priority;
        current.Priority -= 1;
    }

    private void OnDestroy()
    {
        Instance = null;
    }
}
