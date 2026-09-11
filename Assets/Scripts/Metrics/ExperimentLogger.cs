using System.Text;
using UnityEngine;

public class ExperimentLogger : MonoBehaviour
{
    [Header("References")]
    public ExperimentMetrics metrics;
    public BeaconDetector detector;
    public PATController patController;
    public FSOCChannelModel fsoc;

    [Header("Logging")]
    [Tooltip("Samples recorded per second.")]
    public float loggingFrequency = 10f;

    public bool logging;

    private float loggingTimer;

    private StringBuilder csv;

    public void StartLogging()
    {
        csv = new StringBuilder();

        csv.AppendLine(
        "Scenario,Time,Detected,PATState,PointingErrorDeg," +
        "DetectionConfidence,RangeM,ReceivedPowerW," +
        "LinkQuality,LinkAvailable"
    );

        loggingTimer = 0f;
        logging = true;
    }

    public void StopLogging()
    {
        logging = false;
    }

    private void Update()
    {
        if (!logging)
            return;

        loggingTimer += Time.deltaTime;

        float interval =
            1f /
            Mathf.Max(loggingFrequency, 0.1f);

        if (loggingTimer >= interval)
        {
            loggingTimer -= interval;

            RecordSample();
        }
    }

    private void RecordSample()
    {
        string scenario =
        metrics != null
        ? metrics.scenarioName
        : "Unknown";

        if (detector == null ||
            patController == null ||
            fsoc == null)
            return;

        string time =
            Time.time.ToString("F3");

        string detected =
            detector.IsDetected()
            ? "1"
            : "0";

        string patState =
            patController.GetState().ToString();

        string pointingError =
            detector.GetAngularError().ToString("F4");

        string confidence =
            detector.GetConfidence().ToString("F4");

        string range =
            fsoc.GetRange().ToString("F3");

        string receivedPower =
            fsoc.GetReceivedPower().ToString("F8");

        string linkQuality =
            fsoc.GetLinkQuality().ToString("F5");

        string linkAvailable =
            fsoc.linkAvailable
            ? "1"
            : "0";

        csv.AppendLine(
            scenario + "," +
            time + "," +
            detected + "," +
            patState + "," +
            pointingError + "," +
            confidence + "," +
            range + "," +
            receivedPower + "," +
            linkQuality + "," +
            linkAvailable
        );
    }

    public string GetCSV()
    {
        if (csv == null)
            return "";

        return csv.ToString();
    }
}