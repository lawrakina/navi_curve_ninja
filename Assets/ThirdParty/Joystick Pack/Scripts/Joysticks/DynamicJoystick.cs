using UnityEngine;


public class DynamicJoystick : Joystick{
    public float MoveThreshold{
        get{ return moveThreshold; }
        set{ moveThreshold = Mathf.Abs(value); }
    }

    [SerializeField]
    private float moveThreshold = 1;
    public bool JoystickState => _activeDrag;

    private bool _activeDrag = false;

    protected override void Start(){
        MoveThreshold = moveThreshold;
        base.Start();
        background.gameObject.SetActive(false);
    }

    protected override void OnPointerDown(Vector2 pointerPos){
        _activeDrag = true;
        background.anchoredPosition = ScreenPointToAnchoredPosition(pointerPos);
        background.gameObject.SetActive(true);
        base.OnPointerDown(pointerPos);
    }

    protected override void OnPointerUp(Vector2 pointerPos){
        _activeDrag = false;
        background.gameObject.SetActive(false);
        base.OnPointerUp(pointerPos);
    }

    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam){
        if (magnitude > moveThreshold){
            Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
            background.anchoredPosition += difference;
        }

        base.HandleInput(magnitude, normalised, radius, cam);
    }
}