using System.Runtime.InteropServices;
using UnityEngine;

public class CSVExporter : MonoBehaviour
{
    public ExperimentLogger logger;

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void DownloadCSV(
        string data,
        string filename
    );
#endif

    public void DownloadExperimentCSV()
    {
        if (logger == null)
        {
            Debug.LogError(
                "CSVExporter: Logger reference missing."
            );

            return;
        }

        string csv = logger.GetCSV();

        if (string.IsNullOrEmpty(csv))
        {
            Debug.LogWarning(
                "CSVExporter: No experiment data available."
            );

            return;
        }

        string filename =
            "FSOC_PAT_Experiment_" +
            System.DateTime.Now.ToString("yyyyMMdd_HHmmss") +
            ".csv";

#if UNITY_WEBGL && !UNITY_EDITOR

        DownloadCSV(csv, filename);

#else

        Debug.Log(
            "CSV generated successfully.\n\n" +
            csv
        );

#endif
    }
}