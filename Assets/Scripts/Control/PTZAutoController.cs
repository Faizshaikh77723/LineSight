using UnityEngine;

public class PTZAutoController : MonoBehaviour
{
    [Header("References")]
    public KalmanTracking kalman;

    public PTZController ptzController;

    [Header("Control")]
    public bool automaticTracking = true;

    [Header("Pan PID")]
    public PIDController panPID =
        new PIDController();

    [Header("Tilt PID")]
    public PIDController tiltPID =
        new PIDController();

    [Header("Dead Zone")]
    public float deadZone = 0.002f;

    [Header("Control Scaling")]
    public float panScale = 1f;
    public float tiltScale = 1f;

    void Update()
    {
        if (!automaticTracking)
            return;

        if (kalman == null)
            return;

        if (ptzController == null)
            return;

        if (!kalman.IsInitialized())
            return;

        Vector2 error =
            kalman.GetFilteredError();

        error =
            ApplyDeadZone(error);

        float panCommand =
            panPID.Update(
                error.x,
                Time.deltaTime
            );

        float tiltCommand =
            tiltPID.Update(
                error.y,
                Time.deltaTime
            );

        panCommand *=
            panScale;

        tiltCommand *=
            tiltScale;

        ptzController.ApplyAutomaticControl(
            panCommand,
            tiltCommand
        );
    }

    private Vector2 ApplyDeadZone(
        Vector2 error)
    {
        float x =
            Mathf.Abs(error.x) <
            deadZone
            ? 0f
            : error.x;

        float y =
            Mathf.Abs(error.y) <
            deadZone
            ? 0f
            : error.y;

        return new Vector2(x, y);
    }

    public void ResetController()
    {
        panPID.Reset();

        tiltPID.Reset();
    }
}