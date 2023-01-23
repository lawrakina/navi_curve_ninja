using UnityEngine;

namespace Utils.TriggerAction.Triggers.Mono
{
    /// <summary>
    /// When a button is pressed down/up/held, fires off the attached action
    /// </summary>
    public class ButtonMonoTrigger : Base.MonoTrigger
    {
        private enum Mode
        {
            Up, // Good for things like clipping a jump short.
            Down, // Good for things like jump.
            Hold
        }

        [SerializeField] private string _buttonName = "Fire 1";
        [SerializeField] private Mode _mode;

        private void Update()
        {
            if (IsButtonUp() || IsButtonDown() || IsButtonHeld())
                FireAction();
        }

        private bool IsButtonUp() => _mode == Mode.Up && Input.GetButtonUp(_buttonName);

        private bool IsButtonDown() => _mode == Mode.Down && Input.GetButtonDown(_buttonName);

        private bool IsButtonHeld() => _mode == Mode.Hold && Input.GetButton(_buttonName);
    }
}