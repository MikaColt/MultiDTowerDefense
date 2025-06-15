using UnityEngine;

public class JoystickInput
{
    public Vector2 LeftJoystickPosition { get; private set; }
    public Vector2 RightJoystickPosition { get; private set; }

    public JoystickInput()
    {
        LeftJoystickPosition = Vector2.zero;
        RightJoystickPosition = Vector2.zero;
    }

    public void Update()
    {
        float leftJoystickX = Input.GetAxis("Horizontal");
        float leftJoystickY = Input.GetAxis("Vertical");
        float rightJoystickX = Input.GetAxis("Mouse X");
        float rightJoystickY = Input.GetAxis("Mouse Y");

        // Update the joystick positions
        LeftJoystickPosition = new Vector2(leftJoystickX, leftJoystickY);
        RightJoystickPosition = new Vector2(rightJoystickX, rightJoystickY);
    }
}
