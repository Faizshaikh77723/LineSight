using UnityEngine;
using TMPro;

public class PIDUI : MonoBehaviour
{
    [Header("References")]
    public PTZAutoController controller;

    public PTZController ptz;

    public TMP_Text pidText;

    void Update()
    {
        if (controller == null ||
            ptz == null ||
            pidText == null)
            return;

        pidText.text =
            "PID CONTROL\n" +
            "PAN ANGLE: " +
            ptz.GetPanAngle().ToString("F2") +
            " deg\n" +
            "TILT ANGLE: " +
            ptz.GetTiltAngle().ToString("F2") +
            " deg";
    }
}