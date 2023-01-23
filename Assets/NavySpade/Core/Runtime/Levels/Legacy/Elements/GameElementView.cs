using UnityEngine;

[RequireComponent(typeof(GameElement))]
public class GameElementView : MonoBehaviour
{
    [SerializeField] private Renderer _background;

    private GameElement _element;

    private void OnAwake()
    {
        _element = GetComponent<GameElement>();
    }
}
