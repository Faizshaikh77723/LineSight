using UnityEngine;
using TMPro;

public class PATStatusUI : MonoBehaviour
{
    [Header("References")]
    public PATController patController;
    public BeaconDetector detector;
    public PATMetrics metrics;

    public TMP_Text statusText;
    public TMP_Text lockTimerText;
    public TMP_Text acquisitionTimeText;

    void Update()
    {
        if (patController == null)
            return;

        UpdateStatus();
        UpdateLockTimer();
        UpdateAcquisitionTime();
    }

    private void UpdateStatus()
    {
        if (statusText == null)
            return;

        switch (patController.GetCurrentState())
        {
            case PATState.Searching:

                statusText.text =
                    "PAT: SEARCHING";

                break;

            case PATState.Acquiring:

                statusText.text =
                    "PAT: ACQUIRING";

                break;

            case PATState.Tracking:

                statusText.text =
                    "PAT: TRACKING";

                break;

            case PATState.Locked:

                statusText.text =
                    "PAT: LOCKED";

                break;
        }
    }

    private void UpdateLockTimer()
    {
        if (lockTimerText == null)
            return;

        float timer =
            patController.GetLockTimer();

        lockTimerText.text =
            "LOCK TIMER: " +
            timer.ToString("F2") +
            " s";
    }

    private void UpdateAcquisitionTime()
    {
        if (acquisitionTimeText == null)
            return;

        if (metrics == null)
        {
            acquisitionTimeText.text =
                "ACQUISITION TIME: --";

            return;
        }

        float time =
            metrics.GetAcquisitionTime();

        if (time <= 0f)
        {
            acquisitionTimeText.text =
                "ACQUISITION TIME: --";
        }
        else
        {
            acquisitionTimeText.text =
                "ACQUISITION TIME: " +
                time.ToString("F2") +
                " s";
        }
    }
}