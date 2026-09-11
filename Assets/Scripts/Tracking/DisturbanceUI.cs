using UnityEngine;
using TMPro;

public class DisturbanceUI : MonoBehaviour
{
    public DisturbanceModel disturbance;
    public TMP_Text disturbanceText;

    void Update()
    {
        if (disturbance == null ||
            disturbanceText == null)
            return;

        disturbanceText.text =
            "DISTURBANCES\n" +
            "NOISE: " +
            (disturbance.measurementNoiseEnabled ? "ON" : "OFF") +
            "\n" +
            "JITTER: " +
            (disturbance.angularJitterEnabled ? "ON" : "OFF") +
            "\n" +
            "DROPOUT: " +
            (disturbance.dropoutEnabled ? "ON" : "OFF") +
            "\n" +
            "VIBRATION: " +
            (disturbance.vibrationEnabled ? "ON" : "OFF");
    }
}