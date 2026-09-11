using UnityEngine;
using TMPro;

public class DetectionUI : MonoBehaviour
{
    [Header("References")]
    public BeaconDetector detector;

    public TMP_Text detectionText;
    public TMP_Text errorText;
    public TMP_Text confidenceText;

    void Update()
    {
        if (detector == null)
            return;

        UpdateDetectionText();
        UpdateErrorText();
        UpdateConfidenceText();
    }

    private void UpdateDetectionText()
    {
        if (detectionText == null)
            return;

        if (detector.IsDetected())
        {
            detectionText.text =
                "BEACON DETECTED";
        }
        else
        {
            detectionText.text =
                "BEACON NOT DETECTED";
        }
    }

    private void UpdateErrorText()
    {
        if (errorText == null)
            return;

        Vector2 error =
            detector.GetImageError();

        float angle =
            detector.GetAngularError();

        errorText.text =
            "IMAGE ERROR\n" +
            "X: " + error.x.ToString("F3") + "\n" +
            "Y: " + error.y.ToString("F3") + "\n" +
            "ANGLE: " + angle.ToString("F2") + " deg";
    }

    private void UpdateConfidenceText()
    {
        if (confidenceText == null)
            return;

        float confidence =
            detector.GetConfidence();

        confidenceText.text =
            "CONFIDENCE: " +
            confidence.ToString("F2");
    }
}