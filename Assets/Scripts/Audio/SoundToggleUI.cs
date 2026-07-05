using UnityEngine;

/// <summary>
/// Simple on-screen button (and hotkey) to turn all game sound on/off.
/// Toggles AudioListener.volume through SoundManager and persists the choice.
/// </summary>
public class SoundToggleUI : MonoBehaviour
{
    public KeyCode toggleKey = KeyCode.M;
    public Vector2 buttonSize = new Vector2(260, 80);
    public Vector2 margin = new Vector2(24, 24);
    public int fontSize = 32;

    private GUIStyle _style;

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleSound();
        }
    }

    void OnGUI()
    {
        if (SoundManager.Instance == null) return;

        if (_style == null)
        {
            _style = new GUIStyle(GUI.skin.button);
        }
        _style.fontSize = fontSize;
        _style.fontStyle = FontStyle.Bold;

        var rect = new Rect(
            Screen.width - buttonSize.x - margin.x,
            margin.y,
            buttonSize.x,
            buttonSize.y);

        string label = SoundManager.Instance.IsMuted ? "Som: OFF" : "Som: ON";
        if (GUI.Button(rect, label, _style))
        {
            ToggleSound();
        }
    }

    void ToggleSound()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.ToggleMute();
        }
    }
}
