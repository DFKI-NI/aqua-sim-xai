// // This code is to control the ROV with the PS4 controller, inspired from:
// // https://www.youtube.com/watch?v=p-3S73MaDP8&ab_channel=Brackeys
// // Author: Ahmed Harbi
// // Date: 2022-12-26
// // Email: ahmedharbii10@gmail.com

using UnityEngine;
using UnityEngine.InputSystem;

public class usv : MonoBehaviour
{
    UsvControls controls;
    Vector2 LeftThrusterUp;
    Vector2 LeftThrusterDown;
    Vector2 RightThrusterUp;
    Vector2 RightThrusterDown;
    Transform _transform;
    Rigidbody _rigidbody;

    private float _thrustGain = 8f;

    void Awake()
    {
        controls = new UsvControls();

        controls.Gameplay.PauseGame.performed += ctx => PauseGame();

        controls.Gameplay.LeftThrusterUp.performed += ctx => LeftThrusterUp.y =
            ctx.ReadValue<float>();
        controls.Gameplay.LeftThrusterUp.canceled += ctx => LeftThrusterUp.y = 0f;

        controls.Gameplay.LeftThrusterDown.performed += ctx => LeftThrusterDown.y =
            ctx.ReadValue<float>();
        controls.Gameplay.LeftThrusterDown.canceled += ctx => LeftThrusterDown.y = 0f;

        controls.Gameplay.RightThrusterUp.performed += ctx => RightThrusterUp.y =
            ctx.ReadValue<float>();
        controls.Gameplay.RightThrusterUp.canceled += ctx => RightThrusterUp.y = 0f;

        controls.Gameplay.RightThrusterDown.performed += ctx => RightThrusterDown.y =
            ctx.ReadValue<float>();
        controls.Gameplay.RightThrusterDown.canceled += ctx => RightThrusterDown.y = 0f;

        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 mLeftUp = _thrustGain * Time.fixedDeltaTime * new Vector3(0, 0, LeftThrusterUp.y);
        Vector3 mLeftDown =
            _thrustGain * Time.fixedDeltaTime * new Vector3(0, 0, LeftThrusterDown.y);
        Vector3 mRightUp = _thrustGain * Time.fixedDeltaTime * new Vector3(0, 0, RightThrusterUp.y);
        Vector3 mRightDown =
            _thrustGain * Time.fixedDeltaTime * new Vector3(0, 0, RightThrusterDown.y);

        // Apply thruster movements
        _transform.Translate(mLeftUp + mRightUp - mLeftDown - mRightDown, Space.Self);

        // Rotate when only one thruster is active: Left or Right movement
        if ((LeftThrusterUp.magnitude > 0 || LeftThrusterDown.magnitude > 0) &&
            (RightThrusterUp.magnitude == 0 && RightThrusterDown.magnitude == 0))
        {
            float rotationAmount = -(LeftThrusterUp.y - LeftThrusterDown.y) * 1f;
            _transform.Rotate(Vector3.up, rotationAmount, Space.Self);
        }
        else if ((RightThrusterUp.magnitude > 0 || RightThrusterDown.magnitude > 0) &&
                 (LeftThrusterUp.magnitude == 0 && LeftThrusterDown.magnitude == 0))
        {
            float rotationAmount = (RightThrusterUp.y - RightThrusterDown.y) * 0.6f;
            _transform.Rotate(Vector3.up, rotationAmount, Space.Self);
        }
        // Rotation: clockwise and anticlockwise around itself
        else if ((LeftThrusterUp.magnitude > 0 && RightThrusterDown.magnitude < 0))
        {
            // float rotationAmount = (LeftThrusterUp.y - RightThrusterDown.y) * 0.6f;
            float rotationAmount = 10f;
            Debug.Log("Rotation: " + rotationAmount);
            _transform.Rotate(Vector3.up, rotationAmount, Space.Self);
        }
        // else if ((RightThrusterUp.magnitude > 0 && LeftThrusterDown.magnitude <
        // 0))
        // {
        //     float rotationAmount = (RightThrusterUp.y - LeftThrusterDown.y) *
        //     0.6f; _transform.Rotate(Vector3.up, rotationAmount, Space.Self);
        // }
    }

    void OnEnable() { controls.Gameplay.Enable(); }

    void OnDisable() { controls.Gameplay.Disable(); }

    void PauseGame()
    {
        if (Time.timeScale == 0f)
            Time.timeScale = 1f;
        else
            Time.timeScale = 0f;

        Debug.Log("Pause!");
    }
}
