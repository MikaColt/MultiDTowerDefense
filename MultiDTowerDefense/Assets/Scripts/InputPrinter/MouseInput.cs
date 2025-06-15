using UnityEngine;
using System.Collections.Generic;

public class MouseInput
{
    public List<string> Actions { get;  set; }
    private bool[] buttonStates;

    public MouseInput()
    {
        Actions = new List<string>();
        buttonStates = new bool[5];
    }

    public void Update()
    {
        // Check for mouse input
        if (Input.mousePresent)
        {
            for (int i = 0; i < buttonStates.Length; i++)
            {
                bool isPressed = Input.GetMouseButton(i);
                if (isPressed && !buttonStates[i])
                {
                    Actions.Add($"Mouse Button {i} Down");
                    buttonStates[i] = true;
                }
                else if (!isPressed && buttonStates[i])
                {
                    Actions.Add($"Mouse Button {i} Up");
                    buttonStates[i] = false;
                }
            }

            float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
            if (scrollWheel != 0f)
            {
                Actions.Add("Mouse Scroll: " + scrollWheel);
            }
        }
    }
}
