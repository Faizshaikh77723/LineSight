using UnityEngine;
using TMPro;

public class TrackingStatusUI : MonoBehaviour
{
    [Header("References")]
    public TrackingMeasurement measurement;
    public TMP_Text statusText;

    [Header("Lock Threshold")]
    public float lockAngleThreshold = 1.0f;

    void Update()
    {
        if (measurement == null)
        {
            return;
        }

        if (statusText == null)
        {
            return;
        }

        if (!measurement.IsTargetInFieldOfView())
        {
            statusText.text = "TARGET LOST";
            return;
        }

        float error =
            measurement.GetAngularError();

        if (error <= lockAngleThreshold)
        {
            statusText.text = "TRACKING LOCK";
        }
        else
        {
            statusText.text = "ACQUIRING";
        }
    }
}