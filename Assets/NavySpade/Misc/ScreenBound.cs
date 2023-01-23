using System;
using NavySpade.Modules.Extensions;
using UnityEngine;

/// <summary>
/// использвует рейкаст из камеры для определения границ экрана
/// работает в топ даун играх
/// </summary>
public class ScreenBound : MonoBehaviour
{
    public struct Bound
    {
        public Vector3 LeftDownBound;
        public Vector3 RightDownBound;
        public Vector3 LeftUpBound;
        public Vector3 RightUpBound;

        /// <summary>
        /// ui сверху экрана перекрывает 
        /// </summary>
        public Vector3 LeftUpBoundUnscaled;

        public Vector3 RightUpBoundUnscaled;
    }

    private enum ZOffsetMode
    {
        MinusPos,
        Pos,
        Value
    }

    [SerializeField] private Camera _mainCamera;

    [SerializeField] private float _offset = .85f;

    [Header("Camera Z Offset")] [SerializeField]
    private ZOffsetMode _cameraZOffsetMode = ZOffsetMode.MinusPos;

    [SerializeField] private float _cameraZOffsetValue;

    private Bound? _bound;
    private static ScreenBound _instance;

    public static Bound GetBound
    {
        get
        {
            if (_instance == null)
            {
                throw new Exception("Screen bound instacne not found");
            }

            if (_instance._bound == null)
            {
                RecalculateBound();
            }

            return _instance._bound.Value;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        RecalculateBound();
    }

    private void OnDrawGizmos()
    {
        if (_bound == null)
            return;

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(_bound.Value.LeftDownBound, 1f);
        Gizmos.DrawSphere(_bound.Value.LeftUpBound, 1f);
        Gizmos.DrawSphere(_bound.Value.RightDownBound, 1f);
        Gizmos.DrawSphere(_bound.Value.RightUpBound, 1f);

        Gizmos.DrawLine(_bound.Value.LeftDownBound, _bound.Value.LeftUpBound);
        Gizmos.DrawLine(_bound.Value.LeftUpBound, _bound.Value.RightUpBound);
        Gizmos.DrawLine(_bound.Value.RightUpBound, _bound.Value.RightDownBound);
        Gizmos.DrawLine(_bound.Value.RightDownBound, _bound.Value.LeftDownBound);
    }

    public static void RecalculateBound()
    {
        if (_instance == null)
        {
            throw new Exception("Screen bound instance not found");
        }

        _instance._bound = new Bound
        {
            LeftDownBound = ScreenToWorld(new Vector2(), _instance._mainCamera),
            RightDownBound = ScreenToWorld(new Vector2(Screen.width, 0), _instance._mainCamera),
            LeftUpBound = ScreenToWorld(new Vector2(0, Screen.height * _instance._offset), _instance._mainCamera),
            RightUpBound = ScreenToWorld(new Vector2(Screen.width, Screen.height * _instance._offset),
                _instance._mainCamera),

            LeftUpBoundUnscaled = ScreenToWorld(new Vector2(0, Screen.height), _instance._mainCamera),
            RightUpBoundUnscaled = ScreenToWorld(new Vector2(Screen.width, Screen.height), _instance._mainCamera),
        };
    }

    /// <summary>
    /// ignore Y axis
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public static bool IsContainTopDown(Vector3 pos)
    {
        var bound = GetBound;
        var pos2D = new Vector2(pos.x, pos.z);
        var leftDown2D = new Vector2(bound.LeftDownBound.x, bound.LeftDownBound.z);
        var leftUp2D = new Vector2(bound.LeftUpBound.x, bound.LeftUpBound.z);
        var rightDown2D = new Vector2(bound.RightDownBound.x, bound.RightDownBound.z);
        var rightUp2D = new Vector2(bound.RightUpBound.x, bound.RightUpBound.z);
        
        /*
            LeftUp * ------------- * RightUp
                    |             |
                     |           |
                      |         |
              LeftDown * ------* RightDown 
            
            check intersection on any triangles
        */
        
        var triangleDownResult = pos2D.IsPointInTriangle(leftUp2D, leftDown2D, rightDown2D);
        var triangleUpResult = pos2D.IsPointInTriangle(leftUp2D, rightUp2D, rightDown2D);
        
        return triangleDownResult || triangleUpResult;
    }

    private static Vector3 ScreenToWorld(Vector2 screenPos, Camera cam)
    {
        var ray = cam.ScreenPointToRay(new Vector3(screenPos.x, screenPos.y, _instance.GetCameraZOffset(cam)));
        var plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out var enter))
        {
        }

        return ray.GetPoint(enter);
    }

    private float GetCameraZOffset(Camera cam)
    {
        switch (_cameraZOffsetMode)
        {
            case ZOffsetMode.MinusPos:
                return -cam.transform.position.z;
            case ZOffsetMode.Pos:
                return cam.transform.position.z;
            case ZOffsetMode.Value:
                return _cameraZOffsetValue;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}