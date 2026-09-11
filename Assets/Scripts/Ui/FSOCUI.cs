using UnityEngine;
using TMPro;

public class FSOCUI : MonoBehaviour
{
    [Header("References")]
    public FSOCChannelModel fsoc;
    public TMP_Text fsocText;
void Update()
{
    if (fsoc == null ||
        fsocText == null)
        return;

    string linkStatus;

    if (!fsoc.linkDegraded &&
        !fsoc.linkAvailable)
    {
        linkStatus = "LOST";
    }
    else if (fsoc.linkDegraded)
    {
        linkStatus = "DEGRADED";
    }
    else
    {
        linkStatus = "AVAILABLE";
    }

    fsocText.text =
        "FSOC LINK\n" +
        "RANGE: " +
        fsoc.GetRange().ToString("F1") +
        " m\n" +
        "POINTING ERROR: " +
        fsoc.GetPointingError().ToString("F2") +
        " deg\n" +
        "POINTING LOSS: " +
        fsoc.pointingLoss.ToString("F3") +
        "\n" +
        "ATM. LOSS: " +
        fsoc.atmosphericLoss.ToString("F3") +
        "\n" +
        "TURBULENCE: " +
        fsoc.turbulenceFactor.ToString("F3") +
        "\n" +
        "RX POWER: " +
        fsoc.GetReceivedPower().ToString("F6") +
        " W\n" +
        "LINK QUALITY: " +
        (fsoc.GetLinkQuality() * 100f).ToString("F1") +
        "%\n" +
        "LINK STATUS: " +
        linkStatus;
}
}