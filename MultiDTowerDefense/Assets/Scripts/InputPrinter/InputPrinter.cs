using UnityEngine;
using System.Collections.Generic;

public class InputPrinter : MonoBehaviour
{
    public bool ControllerMode_Enabled = false;
    public bool SteamControllerMode_Enabled = false;
    public bool KeyboardControllerMode_Enabled = true;


    public GamePadPrinter GamePads;
    private ControllerInput controllerInput;
    private MouseInput mouseInput;
    private KeyboardInput keyboardInput;
    private JoystickInput joystickInput;

    private List<string> Actions;
    private List<string> Actions_Mouse;


    // Interval for printing joystick positions in seconds
    public float joystickPrintInterval = 1;
    private float nextJoystickPrintTime;

    private void Start()
    {
        controllerInput = new ControllerInput();
        mouseInput = new MouseInput();
        keyboardInput = new KeyboardInput();
        joystickInput = new JoystickInput();

        Actions = new List<string>();
        Actions_Mouse = new List<string>();
        joystickPrintInterval = 5f;
        nextJoystickPrintTime = Time.time + joystickPrintInterval;
    }

    private void Update()
    {
        
 //       _GetControllerActions();
        _GetMouseActions();
        _GetKeyboardActions();

        //       _JoystickUpdate();
        //       _PrintActions();

        if (Actions.Count > 0)
        {
            if (InputActionHandler.Instance != null)
            {
                InputActionHandler.Instance.PushKeyboardActions(Actions);
            }
            if (MainMenuInputActionHandler.Instance != null)
            {
                MainMenuInputActionHandler.Instance.PushKeyboardActions(Actions);
            }
            if (ArenaInputActionHandler.Instance != null)
            {
                ArenaInputActionHandler.Instance.PushKeyboardActions(Actions);
            }

        }
        if (Actions_Mouse.Count > 0)
        {

            if (InputActionHandler.Instance != null)
            {
                InputActionHandler.Instance.PushMouseActions(Actions_Mouse);
            }
            if (MainMenuInputActionHandler.Instance != null)
            {
                MainMenuInputActionHandler.Instance.PushMouseActions(Actions_Mouse);
            }
            if (ArenaInputActionHandler.Instance != null)
            {
                ArenaInputActionHandler.Instance.PushMouseActions(Actions_Mouse);
            }

        }



        Actions = new List<string>();
        Actions_Mouse = new List<string>();






    }

    public void _PrintActions() 
    {
        foreach (string action in Actions)
        {
            Debug.Log(action);
        }
        _JoystickPrint();
    }
    public void _GetGamepadActions()
    {
        keyboardInput.Update();
        Actions.AddRange(keyboardInput.Actions);


        keyboardInput.Actions = new List<string>();
    }
    public void _GetMouseActions() 
    {
        mouseInput.Update();
        Actions_Mouse.AddRange(mouseInput.Actions);
 //       Debug.Log(Actions_Mouse.Count);
        mouseInput.Actions = new List<string>();
    }
    public void _GetKeyboardActions()
    {
        keyboardInput.Update();
        Actions.AddRange(keyboardInput.Actions);


        keyboardInput.Actions = new List<string>();
    }
    public void _GetControllerActions()
    {
        controllerInput.Update();
        Actions.AddRange(controllerInput.Actions);

        controllerInput.Actions = new List<string>();
    }
    public void _JoystickUpdate() 
    {
        joystickInput.Update();
        

    }
    public void _JoystickPrint() 
    {
        if (Time.time >= nextJoystickPrintTime)
        {

            Debug.Log("Left Joystick Position: " + joystickInput.LeftJoystickPosition);
            Debug.Log("Right Joystick Position: " + joystickInput.RightJoystickPosition);
            nextJoystickPrintTime = Time.time + joystickPrintInterval;
        }
    }
}
