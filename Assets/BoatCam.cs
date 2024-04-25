using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatCam : MonoBehaviour
{
    public float Sensitivity;
    public GameObject Boat;
    public Vector3 focusdist;
    private const float Y_ANGLE_MIN = -50f;
    private const float Y_ANGLE_MAX = 0f;
    private float Y_angle_min;
    private float Y_angle_max;
    public float JoyX { get; set; }
    public float JoyY { get; set; }
    public InputManager input;
    public float distance;

    Quaternion camRot;
    Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        JoyX += input.RstickHorizontal.getAxis() * Sensitivity * Time.deltaTime;
        JoyY += input.RstickVertical.getAxis() * Sensitivity * Time.deltaTime;
        JoyX = Mathf.Clamp(JoyX, Y_ANGLE_MIN, Y_ANGLE_MAX);
        camRot = Quaternion.Euler(JoyX, JoyY, 0);
        Vector3 dir = new(0, 0, -distance);
        transform.position = Vector3.SmoothDamp(transform.position, Boat.transform.position + camRot * dir, ref velocity, Time.deltaTime * 3f);
        transform.LookAt(Boat.transform.position);
    }
}
