using UnityEngine;

public class NewInputManager : MonoBehaviour {
    private void OnGUI() {
        var e = Event.current;
        if (e == null)
            return;
        if (e.type != EventType.KeyDown)
            return;
        if (Language.IsAlpha((char)e.keyCode)) {
            PlayerInput.HitKey((char)e.keyCode).Emit(gameObject);
            return;
        }
        if (e.keyCode == KeyCode.Delete || e.keyCode == KeyCode.Backspace) {
            PlayerInput.Delete().Emit(gameObject);
            return;
        }
        if (e.keyCode == KeyCode.KeypadEnter || e.keyCode == KeyCode.Return) {
            PlayerInput.Enter().Emit(gameObject);
            return;
        }
    }
}