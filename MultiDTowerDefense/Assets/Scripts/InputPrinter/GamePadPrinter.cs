
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections.Generic;
public class GamePadPrinter : MonoBehaviour
{
    public static GamePadPrinter Instance { get; private set; }

    private void Awake()
    {
        // Ensure that only one instance of GamePadPrinter exists
        if (Instance == null)
        {
            Instance = this;
 //           DontDestroyOnLoad(gameObject); // Persist this instance across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    public bool SingleControllerMode_Enabled = true;
    public bool MultiControllerMode_Enabled = false;

    public int SelectedController = -1;
    public bool SelectedController_IsXBoxStyle = true;
    public bool SelectedController_IsNintendoStyle = false;

    public Vector2 LeftJoystick;
    public Vector2 RightJoystick;

    public bool IsEnabled(int index) 
    {
        return true;
    }

    public Dictionary<int, bool> EnabledDevices = new Dictionary<int, bool>();
    public Dictionary<int, string> Devices = new Dictionary<int, string>();

    public List<(int, Vector2, Vector2, List<GamePadControllerEnum>)> Actions = new List<(int, Vector2, Vector2, List<GamePadControllerEnum>)>();
    
    
    private void Start()
    {
        SelectedController = -1;
        /*
        // Find all attached peripherals
        var devices = InputSystem.devices;

        foreach (var device in devices)
        {
            Debug.Log($"Device Name: {device.displayName}, Device Type: {device.device.description.deviceClass}");
        }
        */
    }


    private void Update()
    {
        int iCounter = 0;
        foreach (var device in InputSystem.devices)
        {

            if (device is Gamepad gamepad)
            {
                Devices[iCounter] = gamepad.displayName;
                if (SingleControllerMode_Enabled)
                {
                    if (SelectedController == -1)
                    {
                        List<GamePadControllerEnum> actions2 = GetGamepadInputs(gamepad);

                        Vector2 LeftJoystick2= gamepad.leftStick.ReadValue();
                        Vector2 RightJoystick2 = gamepad.rightStick.ReadValue();
                        if (actions2.Count >0 || LeftJoystick2 != new Vector2(0,0) || RightJoystick2 != new Vector2(0, 0))
                        {
                            SelectedController = iCounter;
                            InputSettings.Instance.UseController = true;
                        }
                        else
                        {
                            continue;
                        }
                    }



                    SelectedController = iCounter;
                    if (SelectedController == iCounter)
                    {
                        


                        //               PrintGamepadInputs(gamepad);
                        //       Debug.Log($"Gamepad {gamepad.displayName} - Left Stick: {gamepad.leftStick.ReadValue()}");
                        //       Debug.Log($"Gamepad {gamepad.displayName} - Right Stick: {gamepad.rightStick.ReadValue()}");
                        List<GamePadControllerEnum> actions = GetGamepadInputs(gamepad);
                        LeftJoystick = gamepad.leftStick.ReadValue();
                        RightJoystick = gamepad.rightStick.ReadValue();
                        /*
                        for (int i = 0; i < actions.Count; i++)
                        {
                            Debug.Log(actions[i]);
                        }
                        */
                        Actions.Add((iCounter, LeftJoystick, RightJoystick, actions));
                    }

                }
                else if (MultiControllerMode_Enabled)
                {
                    if (IsEnabled(iCounter))
                    {
                        //    PrintGamepadInputs(gamepad);
                        Actions.Add((iCounter, gamepad.leftStick.ReadValue(), gamepad.rightStick.ReadValue(), GetGamepadInputs(gamepad)));

                    }
                }
                if (InputActionHandler.Instance != null)
                {
                    InputActionHandler.Instance.PushGamepadActions(Actions);
                }
                if (MainMenuInputActionHandler.Instance != null)
                {
                    MainMenuInputActionHandler.Instance.PushGamepadActions(Actions);
                }
                if (ArenaInputActionHandler.Instance != null)
                {
                    ArenaInputActionHandler.Instance.PushGamepadActions(Actions);
                }



                Actions = new List<(int, Vector2, Vector2, List<GamePadControllerEnum>)>();
            }
            /*
            else if (device is Keyboard keyboard)
            {
             //   PrintKeyboardInputs(keyboard);
            }
            */
            // Add more input devices here if needed
            iCounter += 1;
        }
    }

    private void PrintGamepadInputs(Gamepad gamepad)
    {
        // Print all relevant inputs for an Xbox-style controller

        // Sticks
        Debug.Log($"Gamepad {gamepad.displayName} - Left Stick: {gamepad.leftStick.ReadValue()}");
        Debug.Log($"Gamepad {gamepad.displayName} - Right Stick: {gamepad.rightStick.ReadValue()}");

        // Stick Presses
        Debug.Log($"Gamepad {gamepad.displayName} - Left Stick Press: {gamepad.leftStickButton.isPressed}");
        Debug.Log($"Gamepad {gamepad.displayName} - Right Stick Press: {gamepad.rightStickButton.isPressed}");

        // Triggers
        Debug.Log($"Gamepad {gamepad.displayName} - Left Trigger: {gamepad.leftTrigger.ReadValue()}");
        Debug.Log($"Gamepad {gamepad.displayName} - Right Trigger: {gamepad.rightTrigger.ReadValue()}");

        // Bumpers
        Debug.Log($"Gamepad {gamepad.displayName} - Left Bumper: {gamepad.leftShoulder.isPressed}");
        Debug.Log($"Gamepad {gamepad.displayName} - Right Bumper: {gamepad.rightShoulder.isPressed}");

        // Face Buttons (ABXY)
        Debug.Log($"Gamepad {gamepad.displayName} - Button A: {gamepad.buttonSouth.isPressed}");
        Debug.Log($"Gamepad {gamepad.displayName} - Button B: {gamepad.buttonEast.isPressed}");
        Debug.Log($"Gamepad {gamepad.displayName} - Button X: {gamepad.buttonWest.isPressed}");
        Debug.Log($"Gamepad {gamepad.displayName} - Button Y: {gamepad.buttonNorth.isPressed}");

        // Directional Pad (D-pad)
        Debug.Log($"Gamepad {gamepad.displayName} - D-pad Up: {gamepad.dpad.up.isPressed}");
        Debug.Log($"Gamepad {gamepad.displayName} - D-pad Down: {gamepad.dpad.down.isPressed}");
        Debug.Log($"Gamepad {gamepad.displayName} - D-pad Left: {gamepad.dpad.left.isPressed}");
        Debug.Log($"Gamepad {gamepad.displayName} - D-pad Right: {gamepad.dpad.right.isPressed}");

        // Start and Select
        Debug.Log($"Gamepad {gamepad.displayName} - Start: {gamepad.startButton.isPressed}");
        Debug.Log($"Gamepad {gamepad.displayName} - Select: {gamepad.selectButton.isPressed}");
    }
    private List<GamePadControllerEnum> GetGamepadInputs(Gamepad gamepad)
    {
        List<GamePadControllerEnum> actions = new List<GamePadControllerEnum>();



        // Sticks
     //   Debug.Log($"Gamepad {gamepad.displayName} - Left Stick: {gamepad.leftStick.ReadValue()}");
     //   Debug.Log($"Gamepad {gamepad.displayName} - Right Stick: {gamepad.rightStick.ReadValue()}");

        // Stick Presses
    //    Debug.Log($"Gamepad {gamepad.displayName} - Left Stick Press: {gamepad.leftStickButton.isPressed}");

        if (gamepad.leftStickButton.isPressed)
        {
            actions.Add(GamePadControllerEnum.LJ);
        }
        
        
    //    Debug.Log($"Gamepad {gamepad.displayName} - Right Stick Press: {gamepad.rightStickButton.isPressed}");
        if (gamepad.rightStickButton.isPressed) 
        { 
            actions.Add(GamePadControllerEnum.RJ);
        }
        // Triggers
//        Debug.Log($"Gamepad {gamepad.displayName} - Left Trigger: {gamepad.leftTrigger.ReadValue()}");

        if (gamepad.leftTrigger.ReadValue() > 0)
        {
            actions.Add(GamePadControllerEnum.LT);
        }

//        Debug.Log($"Gamepad {gamepad.displayName} - Right Trigger: {gamepad.rightTrigger.ReadValue()}");

        if (gamepad.rightTrigger.ReadValue() > 0)
        {
            actions.Add(GamePadControllerEnum.RT);
        }
        // Bumpers
//        Debug.Log($"Gamepad {gamepad.displayName} - Left Bumper: {gamepad.leftShoulder.isPressed}");


        if (gamepad.leftShoulder.isPressed)
        {
            actions.Add(GamePadControllerEnum.LB);
        }


//        Debug.Log($"Gamepad {gamepad.displayName} - Right Bumper: {gamepad.rightShoulder.isPressed}");



        if (gamepad.rightShoulder.isPressed)
        {
            actions.Add(GamePadControllerEnum.RB);
        }

        // Face Buttons (ABXY)
  //      if (SelectedController_IsXBoxStyle)
  //      {
            if (gamepad.buttonSouth.isPressed)
            {
                actions.Add(GamePadControllerEnum.A);
            }
            if (gamepad.buttonEast.isPressed)
            {
                actions.Add(GamePadControllerEnum.B);
            }
            if (gamepad.buttonWest.isPressed)
            {
                actions.Add(GamePadControllerEnum.X);
            }
            if (gamepad.buttonNorth.isPressed)
            {
                actions.Add(GamePadControllerEnum.Y);
            }
  //      }

        /*
        else if (SelectedController_IsNintendoStyle)
        {
            if (gamepad.buttonSouth.isPressed)
            {
                actions.Add(GamePadControllerEnum.B);
            }
            if (gamepad.buttonEast.isPressed)
            {
                actions.Add(GamePadControllerEnum.A);
            }
            if (gamepad.buttonWest.isPressed)
            {
                actions.Add(GamePadControllerEnum.Y);
            }
            if (gamepad.buttonNorth.isPressed)
            {
                actions.Add(GamePadControllerEnum.X);
            }
        }
        else 
        {
        
        }
        */
 //       Debug.Log($"Gamepad {gamepad.displayName} - Button A: {gamepad.buttonSouth.isPressed}");
 //       Debug.Log($"Gamepad {gamepad.displayName} - Button B: {gamepad.buttonEast.isPressed}");
 //       Debug.Log($"Gamepad {gamepad.displayName} - Button X: {gamepad.buttonWest.isPressed}");
 //       Debug.Log($"Gamepad {gamepad.displayName} - Button Y: {gamepad.buttonNorth.isPressed}");

        // Directional Pad (D-pad)
 //       Debug.Log($"Gamepad {gamepad.displayName} - D-pad Up: {gamepad.dpad.up.isPressed}");
        if (gamepad.dpad.up.isPressed)
        {
            actions.Add(GamePadControllerEnum.Up);
        }
 //       Debug.Log($"Gamepad {gamepad.displayName} - D-pad Down: {gamepad.dpad.down.isPressed}");

        if (gamepad.dpad.down.isPressed)
        {
            actions.Add(GamePadControllerEnum.Down);
        }

 //       Debug.Log($"Gamepad {gamepad.displayName} - D-pad Left: {gamepad.dpad.left.isPressed}");

        if (gamepad.dpad.left.isPressed)
        {
            actions.Add(GamePadControllerEnum.Left);
        }

 //       Debug.Log($"Gamepad {gamepad.displayName} - D-pad Right: {gamepad.dpad.right.isPressed}");
        if (gamepad.dpad.right.isPressed)
        {
            actions.Add(GamePadControllerEnum.Right);
        }
        // Start and Select
  //      Debug.Log($"Gamepad {gamepad.displayName} - Start: {gamepad.startButton.isPressed}");
        
        if (gamepad.startButton.isPressed)
        {
            actions.Add(GamePadControllerEnum.Start);
        }

     //   Debug.Log($"Gamepad {gamepad.displayName} - Select: {gamepad.selectButton.isPressed}");

        if (gamepad.selectButton.isPressed)
        {
            actions.Add(GamePadControllerEnum.Select);
        }

        return actions;
    }

    private void PrintKeyboardInputs(Keyboard keyboard)
    {
        // Print some common keyboard inputs
        if (keyboard.anyKey.isPressed)
        {
            foreach (var key in keyboard.allKeys)
            {
                if (key.isPressed)
                {
                    Debug.Log($"Keyboard Key Pressed: {key.displayName}");
                }
            }
        }
    }
}

