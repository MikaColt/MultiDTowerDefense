using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class MainMenuInputActionHandler : MonoBehaviour
{
    public static MainMenuInputActionHandler Instance { get; private set; }

    // Awake method to implement the singleton pattern
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //            DontDestroyOnLoad(gameObject); // Optional, if you want this instance to persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (MouseActions.Count > 0)
        {
            List<List<string>> _actions0 = new List<List<string>>(MouseActions);
            ProcessMouseActions(_actions0);
            MouseActions = new List<List<string>>();
        }
        if (KeyboardActions.Count > 0)
        {
            List<List<string>> _actions1 = new List<List<string>>(KeyboardActions);
            ProcessKeyboardActions(_actions1);
            KeyboardActions = new List<List<string>>();
        }
        if (GamepadActions.Count > 0)
        {
            List<List<(int, Vector2, Vector2, List<GamePadControllerEnum>)>> _actions2 = new List<List<(int, Vector2, Vector2, List<GamePadControllerEnum>)>>(GamepadActions);
            ProcessGamepadActions(_actions2);
            GamepadActions = new List<List<(int, Vector2, Vector2, List<GamePadControllerEnum>)>>();
        }
    }

    void FixedUpdate()
    {
        List<int> indexesToRemove_Gamepad = new List<int>();


        for (int i = 0; i < OnCooldown_Gamepad.Count; i++)
        {
            GamePadControllerEnum button = OnCooldown_Gamepad[i];
            int remaining = Cooldowns_Gamepad[button].Item1 - 1;
            if (remaining < 1)
            {
                indexesToRemove_Gamepad.Add(i);
            }
            Cooldowns_Gamepad[button] = (remaining, CooldownResetDuration);

        }
        for (int i = 0; i < indexesToRemove_Gamepad.Count; i++)
        {
            try
            {
                OnCooldown_Gamepad.RemoveAt(indexesToRemove_Gamepad[i]);
            }
            catch
            {


            }

        }


        List<int> indexesToRemove_Keyboard = new List<int>();


        for (int i = 0; i < OnCooldown_Keyboard.Count; i++)
        {
            string button2 = OnCooldown_Keyboard[i];
            int remaining = Cooldowns_Keyboard[button2].Item1 - 1;
            if (remaining < 1)
            {
                indexesToRemove_Keyboard.Add(i);
            }
            Cooldowns_Keyboard[button2] = (remaining, CooldownResetDuration);

        }
        for (int i = 0; i < indexesToRemove_Keyboard.Count; i++)
        {
            try
            {
                OnCooldown_Keyboard.RemoveAt(indexesToRemove_Keyboard[i]);
            }
            catch
            {


            }

        }

        List<int> indexesToRemove_Mouse = new List<int>();



        for (int i = 0; i < OnCooldown_Mouse.Count; i++)
        {
            string button2 = OnCooldown_Mouse[i];
            int remaining = Cooldowns_Mouse[button2].Item1 - 1;
            if (remaining < 1)
            {
                indexesToRemove_Mouse.Add(i);
            }
            Cooldowns_Mouse[button2] = (remaining, CooldownResetDuration);

        }
        for (int i = 0; i < indexesToRemove_Mouse.Count; i++)
        {
            try
            {
                OnCooldown_Mouse.RemoveAt(indexesToRemove_Mouse[i]);
            }
            catch
            {


            }

        }
    }

    public async Task ProcessKeyboardActions(List<List<string>> actions)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            for (int j = 0; j < actions[i].Count; j++)
            {

                ProcessAction_Keyboard((actions[i])[j]);
            }
        }
    }

    public void ProcessAction_Keyboard(string action)
    {
        if (IsOnCooldown_Keyboard(action))
        {
            return;
        }



        SetCooldown_Keyboard(action);
        switch (action)
        {
            case "Tab":
                //                Debug.Log(action);
                Action_CycleMenuForward();
                break;
            case "Return":
                //                Debug.Log(action);
                Action_ActivateSelectedButton();
                break;
            case "Backspace":
                //                Debug.Log(action);
                Action_BackOutOfMenu();
                break;
            default:
                break;
        }
    }
    public void ProcessAction_Mouse(string action)
    {

        if (IsOnCooldown_Mouse(action))
        {
            return;
        }

        (int, string) mouseDetection = DynamicMouseSizeDetection(action);


        SetCooldown_Mouse(action);

        switch (mouseDetection.Item2)
        {
            case "Down":
                Action_MouseButtons(mouseDetection.Item1);
                break;
            case "Up":
                Action_MouseButtons_Release(mouseDetection.Item1);
                break;
            default:
                break;
        }



    }


    public async Task ProcessGamepadActions(List<List<(int, Vector2, Vector2, List<GamePadControllerEnum>)>> actions)
    {

        //        await Task.Delay(2000);

        for (int i = 0; i < actions.Count; i++)
        {
            for (int j = 0; j < actions[i].Count; j++)
            {
                for (int k = 0; k < ((actions[i])[j]).Item4.Count; k++)
                {
                    ProcessAction_Gamepad((((actions[i])[j]).Item4)[k]);
                }
            }
        }
    }
    public async Task ProcessMouseActions(List<List<string>> actions)
    {
        for (int i = 0; i < actions.Count; i++)
        {
            for (int j = 0; j < actions[i].Count; j++)
            {

                ProcessAction_Mouse((actions[i])[j]);
            }
        }
    }
    public void ProcessAction_Gamepad(GamePadControllerEnum action)
    {
        if (IsOnCooldown_Gamepad(action))
        {
            return;
        }
        SetCooldown_Gamepad(action);
        switch (action)
        {
            case GamePadControllerEnum.Down:
                //                Debug.Log(action);
                Action_CycleMenuForward();
                break;
            case GamePadControllerEnum.Up:
                //                               Debug.Log(action);
                Action_CycleMenuBackwards();
                break;
            case GamePadControllerEnum.A:
                //                               Debug.Log(action);
                Action_ActivateSelectedButton();
                break;
            case GamePadControllerEnum.B:
                //                               Debug.Log(action);
                Action_BackOutOfMenu();
                break;
            default:
                break;
        }



    }

    public List<List<string>> MouseActions = new List<List<string>>();

    public List<List<string>> KeyboardActions = new List<List<string>>();
    public List<List<(int, Vector2, Vector2, List<GamePadControllerEnum>)>> GamepadActions = new List<List<(int, Vector2, Vector2, List<GamePadControllerEnum>)>>();

    public void PushKeyboardActions(List<string> actions)
    {
        //       Debug.Log(actions.Count);
        KeyboardActions.Add(actions);
    }
    public void PushGamepadActions(List<(int, Vector2, Vector2, List<GamePadControllerEnum>)> actions)
    {
        GamepadActions.Add(actions);
    }
    public void PushMouseActions(List<string> actions)
    {
        //       Debug.Log(actions.Count);
        MouseActions.Add(actions);
    }
    public int CooldownResetDuration = 15;
    public float MouseCooldownModifier = 1.5f;
    //(currentcooldown remaining, cooldown amount)
    public Dictionary<GamePadControllerEnum, (int, int)> Cooldowns_Gamepad = new Dictionary<GamePadControllerEnum, (int, int)>();
    public List<GamePadControllerEnum> OnCooldown_Gamepad = new List<GamePadControllerEnum>();
    public bool IsOnCooldown_Gamepad(GamePadControllerEnum button)
    {
        if (Cooldowns_Gamepad.ContainsKey(button) == false)
        {
            return false;
        }
        else
        {
            int cooldown = Cooldowns_Gamepad[button].Item1;
            return cooldown > 0;

        }
    }
    public void SetCooldown_Gamepad(GamePadControllerEnum button)
    {
        Cooldowns_Gamepad[button] = (CooldownResetDuration, CooldownResetDuration);
        OnCooldown_Gamepad.Add(button);
    }

    //-------------------------------------------------------------------
    public Dictionary<string, (int, int)> Cooldowns_Mouse = new Dictionary<string, (int, int)>();
    public List<string> OnCooldown_Mouse = new List<string>();
    public bool IsOnCooldown_Mouse(string button)
    {
        if (Cooldowns_Mouse.ContainsKey(button) == false)
        {
            return false;
        }
        else
        {
            int cooldown = Cooldowns_Mouse[button].Item1;
            return cooldown > 0;

        }
    }
    public void SetCooldown_Mouse(string button)
    {
        int result = Mathf.RoundToInt(CooldownResetDuration * MouseCooldownModifier);
        Cooldowns_Mouse[button] = (result, result);
        OnCooldown_Mouse.Add(button);
    }
    //-------------------------------------------------------------------


    public Dictionary<string, (int, int)> Cooldowns_Keyboard = new Dictionary<string, (int, int)>();
    public List<string> OnCooldown_Keyboard = new List<string>();
    public bool IsOnCooldown_Keyboard(string button)
    {
        if (Cooldowns_Keyboard.ContainsKey(button) == false)
        {
            return false;
        }
        else
        {
            int cooldown = Cooldowns_Keyboard[button].Item1;
            return cooldown > 0;

        }
    }
    public void SetCooldown_Keyboard(string button)
    {
        Cooldowns_Keyboard[button] = (CooldownResetDuration, CooldownResetDuration);
        OnCooldown_Keyboard.Add(button);
    }

    //-------------------------------------------------------------------
    public void Action_CycleMenuForward()
    {
        StartMenuNavigation.Instance.IncrementSelection();
    }
    public void Action_CycleMenuBackwards()
    {
        StartMenuNavigation.Instance.DecrementSelection();
    }
    public void Action_ActivateSelectedButton()
    {

        StartMenuNavigation.Instance.ActivateSelectedButton();
    }
    public void Action_BackOutOfMenu()
    {

        StartMenuNavigation.Instance.BackOutOfMenu();
    }

    public void Action_MouseButtons(int index) 
    {
        switch (index)
        {
            case 0:
                Action_MouseButton0();
                break;
            case 1:
                Action_MouseButton1();
                break;
            case 2:
                Action_MouseButton2();
                break;
            case 3:
                Action_MouseButton3();
                break;
            case 4:
                Action_MouseButton4();
                break;
            default:
                break;
        }
    }
    public void Action_MouseButton0() 
    {
    
    }
    public void Action_MouseButton1()
    {

    }
    public void Action_MouseButton2()
    {

    }
    public void Action_MouseButton3()
    {

    }
    public void Action_MouseButton4()
    {

    }



    public void Action_MouseButtons_Release(int index)
    {
        switch (index)
        {
            case 0:
                Action_MouseButton0_Release();
                break;
            case 1:
                Action_MouseButton1_Release();
                break;
            case 2:
                Action_MouseButton2_Release();
                break;
            case 3:
                Action_MouseButton3_Release();
                break;
            case 4:
                Action_MouseButton4_Release();
                break;
            default:
                break;
        }
    }
    public void Action_MouseButton0_Release()
    {

    }
    public void Action_MouseButton1_Release()
    {

    }
    public void Action_MouseButton2_Release()
    {

    }
    public void Action_MouseButton3_Release()
    {

    }
    public void Action_MouseButton4_Release()
    {

    }


    //--------------------------------------

    public (int, string) DynamicMouseSizeDetection(string button)
    {
        (int, string) result;
        int startingIndex_ButtonNumber = 13;
        int digits = DynamicMouseSizeDetection_DigitParse(button);
        int endingIndex_ButtonNumber = startingIndex_ButtonNumber + digits + 1;
        string subset = button.Substring(startingIndex_ButtonNumber, digits);
        string postFix_Subset = button.Substring(endingIndex_ButtonNumber);
        int buttonNumber = DynamicMouseSizeDetection_Int(subset);
        result = (buttonNumber, postFix_Subset);
        return result;
    }



    public int DynamicMouseSizeDetection_DigitParse(string button)
    {

        string subset = button.Substring(13, 2);
        if (int.TryParse(button, out int number))
        {

            return 2;
        }
        else
        {

            return 1;
        }

    }
    public int DynamicMouseSizeDetection_Int(string button)
    {
        if (int.TryParse(button, out int number))
        {
            //            Debug.Log($"Converted number: {number}");
            return number;
        }
        else
        {
            Debug.Log("Invalid number string.");
            return -1;
        }
    }

}
