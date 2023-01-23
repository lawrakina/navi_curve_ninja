using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameElement : MonoBehaviour
{
    [SerializeField] private Transform _point;
    [SerializeField] private Vector3 _size = new Vector3(10, 20, 10);
    [SerializeField] private Color _gizmosColor;

    public Transform Point => _point;
    public Vector3 Size => _size;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmosColor;
        Gizmos.DrawCube(transform.position, _size);
    }
}

#if UNITY_EDITOR

[CustomEditor(typeof(GameElement))]
[CanEditMultipleObjects]
public class GameElementEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var curr = target as GameElement;
        EditorGUILayout.HelpBox("BoxSize standard 10x10x10", MessageType.Warning);
        base.DrawDefaultInspector();
    }
}

#endif