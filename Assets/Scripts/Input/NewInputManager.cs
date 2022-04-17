using UnityEngine;

public class NewInputManager : BaseUpdateProducer<PlayerInput> {
    private void OnGUI() {
        var e = Event.current;
        if (e == null)
            return;
        if (e.type != EventType.KeyDown)
            return;
        if (Language.IsAlpha((char)e.keyCode)) {
            Emit(PlayerInput.HitKey((char)e.keyCode));
            return;
        }
        if (e.keyCode == KeyCode.Delete || e.keyCode == KeyCode.Backspace) {
            Emit(PlayerInput.Delete());
            return;
        }
        if (e.keyCode == KeyCode.KeypadEnter || e.keyCode == KeyCode.Return) {
            Emit(PlayerInput.Enter());
            return;
        }
    }
}