using UnityEngine;
using TMPro;

public class test : MonoBehaviour
{
    public TMP_Text text;
    public Vector2 joystick;
    public void Testxd()
    {
        Debug.Log("Correct");
    }

    public void GETJOY(Vector2 joystick)
    {
        this.joystick = joystick;
        text.text = joystick.ToString();
    }
}
