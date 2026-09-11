using UnityEngine;

public class TrackingUI : MonoBehaviour
{
    [Header("References")]
    public RectTransform crosshair;
    public Camera opticalCamera;
    public Transform target;

    void Update()
    {
        if (crosshair == null)
        {
            return;
        }

        if (opticalCamera == null)
        {
            return;
        }

        if (target == null)
        {
            return;
        }

        UpdateCrosshair();
    }

    private void UpdateCrosshair()
    {
        Vector3 screenPosition =
            opticalCamera.WorldToScreenPoint(
                target.position
            );

        if (screenPosition.z <= 0f)
        {
            crosshair.gameObject.SetActive(false);
            return;
        }

        crosshair.gameObject.SetActive(true);

        crosshair.position =
            screenPosition;
    }
}