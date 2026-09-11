using UnityEngine;
using TMPro;

public class KalmanUI : MonoBehaviour
{
    public KalmanTracking kalman;

    public TMP_Text kalmanText;

    void Update()
    {
        if (kalman == null ||
            kalmanText == null)
            return;

        Vector2 raw =
            kalman.rawMeasurement;

        Vector2 filtered =
            kalman.filteredMeasurement;

        Vector2 velocity =
            kalman.estimatedVelocity;

        kalmanText.text =
            "KALMAN FILTER\n" +
            "RAW:      " +
            raw.x.ToString("F3") +
            ", " +
            raw.y.ToString("F3") +
            "\n" +
            "FILTERED: " +
            filtered.x.ToString("F3") +
            ", " +
            filtered.y.ToString("F3") +
            "\n" +
            "VELOCITY: " +
            velocity.x.ToString("F3") +
            ", " +
            velocity.y.ToString("F3");
    }
}