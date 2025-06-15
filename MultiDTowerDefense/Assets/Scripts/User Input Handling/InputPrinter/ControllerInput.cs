using UnityEngine;
using System.Collections.Generic;

public class ControllerInput
{
    public List<string> Actions { get;  set; }
    private bool[,] buttonStates;

    public ControllerInput()
    {
        Actions = new List<string>();
        buttonStates = new bool[8, 20];
    }

    public void Update()
    {
        // Check for joystick button input
        for (int joystickIndex = 1; joystickIndex <= 8; joystickIndex++)
        {
            for (int button = 0; button < 20; button++)
            {
                bool isPressed = Input.GetKey("joystick " + joystickIndex + " button " + button);
                if (isPressed && !buttonStates[joystickIndex - 1, button])
                {
                    string action = "Joystick " + joystickIndex + " Button " + button + " Down";
                    Actions.Add(action);
                    buttonStates[joystickIndex - 1, button] = true;
                }
                else if (!isPressed && buttonStates[joystickIndex - 1, button])
                {
                    string action = "Joystick " + joystickIndex + " Button " + button + " Up";
                    Actions.Add(action);
                    buttonStates[joystickIndex - 1, button] = false;
                }
            }
        }
    }
}
