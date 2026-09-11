using UnityEngine;

public class PTZController : MonoBehaviour
{
    [Header("PTZ Axes")]
    public Transform panAxis;
    public Transform tiltAxis;


    [Header("Manual Control")]
    public float panSpeed = 60f;
    public float tiltSpeed = 60f;


    [Header("Pan Limits")]
    public float minPan = -180f;
    public float maxPan = 180f;


    [Header("Tilt Limits")]
    public float minTilt = -45f;
    public float maxTilt = 45f;


    [Header("Control Mode")]
    public bool manualControlEnabled = true;


    private float currentPan;
    private float currentTilt;


    // =========================================================
    // INITIALIZATION
    // =========================================================

    void Start()
    {
        if (panAxis != null)
        {
            currentPan =
                NormalizeAngle(
                    panAxis.localEulerAngles.y
                );
        }

        if (tiltAxis != null)
        {
            currentTilt =
                NormalizeAngle(
                    tiltAxis.localEulerAngles.x
                );
        }

        ClampAngles();
        ApplyRotation();
    }


    // =========================================================
    // UPDATE
    // =========================================================

    void Update()
    {
        if (manualControlEnabled)
        {
            HandleManualInput();
        }

        ApplyRotation();
    }


    // =========================================================
    // MANUAL KEYBOARD CONTROL
    // =========================================================

    private void HandleManualInput()
    {
        float panInput = 0f;
        float tiltInput = 0f;


        if (Input.GetKey(KeyCode.LeftArrow))
        {
            panInput = -1f;
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            panInput = 1f;
        }


        if (Input.GetKey(KeyCode.DownArrow))
        {
            tiltInput = -1f;
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            tiltInput = 1f;
        }


        currentPan +=
            panInput *
            panSpeed *
            Time.deltaTime;


        currentTilt +=
            tiltInput *
            tiltSpeed *
            Time.deltaTime;


        ClampAngles();
    }


    // =========================================================
    // AUTOMATIC PAT CONTROL
    // =========================================================

    public void ApplyAutomaticControl(
        float panCommand,
        float tiltCommand)
    {
        currentPan +=
            panCommand *
            Time.deltaTime;


        currentTilt +=
            tiltCommand *
            Time.deltaTime;


        ClampAngles();
    }


    // =========================================================
    // ANGLE LIMITS
    // =========================================================

    private void ClampAngles()
    {
        currentPan =
            Mathf.Clamp(
                currentPan,
                minPan,
                maxPan
            );


        currentTilt =
            Mathf.Clamp(
                currentTilt,
                minTilt,
                maxTilt
            );
    }


    // =========================================================
    // APPLY ROTATION
    // =========================================================

    private void ApplyRotation()
    {
        if (panAxis != null)
        {
            panAxis.localRotation =
                Quaternion.Euler(
                    0f,
                    currentPan,
                    0f
                );
        }


        if (tiltAxis != null)
        {
            tiltAxis.localRotation =
                Quaternion.Euler(
                    currentTilt,
                    0f,
                    0f
                );
        }
    }


    // =========================================================
    // ANGLE NORMALIZATION
    // =========================================================

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
        {
            angle -= 360f;
        }

        return angle;
    }


    // =========================================================
    // GETTERS
    // =========================================================

    public float GetPanAngle()
    {
        return currentPan;
    }


    public float GetTiltAngle()
    {
        return currentTilt;
    }


    // =========================================================
    // BROWSER / EXTERNAL CONTROL
    // =========================================================

    public void SetPanAngle(float angle)
    {
        currentPan =
            Mathf.Clamp(
                angle,
                minPan,
                maxPan
            );

        ApplyRotation();
    }


    public void SetTiltAngle(float angle)
    {
        currentTilt =
            Mathf.Clamp(
                angle,
                minTilt,
                maxTilt
            );

        ApplyRotation();
    }


    // =========================================================
    // RESET
    // =========================================================

    public void ResetPTZ()
    {
        currentPan = 0f;
        currentTilt = 0f;

        ClampAngles();
        ApplyRotation();
    }
}