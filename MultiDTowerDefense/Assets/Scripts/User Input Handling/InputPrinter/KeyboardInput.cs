using UnityEngine;
using System.Collections.Generic;

public class KeyboardInput
{
    public List<string> Actions { get; set; }

    private SpecialCharactersClass specialCharacters;

    public KeyboardInput()
    {
        Actions = new List<string>();
        specialCharacters = new SpecialCharactersClass(this);
    }

    public bool AnyKeyIsPressed()
    {
        return Input.anyKey;
    }

    public string GetPressedKey()
    {
  




        string inputString = Input.inputString;



            if (Input.GetKeyDown(KeyCode.Space))
            {
                return "Space";
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                return "Return";
            }
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                return "Backspace";
            }
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                return "Delete";
            }
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                return "Tab";
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                return "Escape";
            }
        
        if (!string.IsNullOrEmpty(inputString))
        {
            return inputString[inputString.Length - 1].ToString();
        }

        return null;
    }

    public void Update()
    {
        specialCharacters.Update();

        if (AnyKeyIsPressed())
        {
            string pressedKey = GetPressedKey();
            if (!string.IsNullOrEmpty(pressedKey))
            {
                Actions.Add(pressedKey);
            }
        }
    }



    public class SpecialCharactersClass
    {
        private bool _leftCtrlPressed = false;
        private bool _rightCtrlPressed = false;
        private bool _leftAltPressed = false;
        private bool _rightAltPressed = false;
        private bool _leftShiftPressed = false;
        private bool _rightShiftPressed = false;

        private bool leftCtrlPressed
        {
            get 
            {
                return _leftCtrlPressed;
            }
            set
            {

                if (value == true && _leftCtrlPressed == false)
                {
                    OnKeyPress("LeftCtrl");
                }
                else if (value == false && _leftCtrlPressed == true)
                {
                    OnHoldEnd("LeftCtrl");
                }
                _leftCtrlPressed = value;
            }
        }
        private bool rightCtrlPressed
        {
            get
            {
                return _rightCtrlPressed;
            }
            set
            {

                if (value == true && _rightCtrlPressed == false) 
                {
                    OnKeyPress("RightCtrl");
                }
                else if (value == false && _rightCtrlPressed == true)
                {
                    OnHoldEnd("RightCtrl");
                }
                _rightCtrlPressed = value;
            }
        }
        private bool leftAltPressed
        {
            get
            {
                return _leftAltPressed;
            }
            set
            {

                if (value == true && _leftAltPressed == false) 
                {
                    OnKeyPress("LeftAlt");
                }
                else if (value == false && _leftAltPressed == true)
                {
                    OnHoldEnd("LeftAlt");
                }
                _leftAltPressed = value;
            }
        }
        private bool rightAltPressed
        {
            get
            {
                return _rightAltPressed;
            }
            set
            {

                if (value == true && _rightAltPressed == false)
                {
                    OnKeyPress("RightAlt");
                }
                else if (value == false && _rightAltPressed == true)
                {
                    OnHoldEnd("RightAlt");
                }
                    _rightAltPressed = value;
            }
        }
        private bool leftShiftPressed
        {
            get
            {
                return _leftShiftPressed;
            }
            set
            {

                if (value == true && _leftShiftPressed == false)
                {
                    OnKeyPress("LeftShift");
                }
                else if (value == false && _leftShiftPressed == true)
                {
                    OnHoldEnd("LeftShift");
                }
                    _leftShiftPressed = value;
                }
            }
        private bool rightShiftPressed
        {
            get
            {
                return _rightShiftPressed;
            }
            set
            {

                if (value == true && _rightShiftPressed == false)
                {
                    OnKeyPress("RightShift");
                }
                else if (value == false && _rightShiftPressed == true)
                {
                    OnHoldEnd("RightShift");
                }
                    _rightShiftPressed = value;
                }
            }

        private KeyboardInput keyboardInput;

        public SpecialCharactersClass(KeyboardInput input)
        {
            keyboardInput = input;
        }



        public void Update()
        {

                ProcessSpecialInput();


        }

        private void OnKeyPress(string keyName)
        {
            // Add the key press to the Actions list in KeyboardInput class
            switch (keyName)
            {
                case "leftCtrl":
                    if (leftCtrlPressed == true)
                    {
                        return;
                    }
                    break;
                case "rightCtrl":
                    if (rightCtrlPressed == true)
                    {
                        return;
                    }
                    break;
                case "leftAlt":
                    if (leftAltPressed == true)
                    {
                        return;
                    }
                    break;
                case "rightAlt":
                    if (rightAltPressed == true)
                    {
                        return;
                    }
                    break;
                case "leftShift":
                    if (leftShiftPressed == true)
                    {
                        return;
                    }
                    break;
                case "rightShift":
                    if (rightShiftPressed == true)
                    {
                        return;
                    }
                    break;
                default:
                    break;
            }
            keyboardInput.Actions.Add(keyName + " Pressed");
        }

        private void OnHoldEnd(string keyName)
        {
            // Add the key release to the Actions list in KeyboardInput class
            keyboardInput.Actions.Add(keyName + " Released");
        }
        private void OnHoldEnd(string keyName, bool pressed)
        {
            if (pressed == false)
            {
                OnHoldEnd(keyName);
            }

        }

        public void ProcessSpecialInput()
        {
            // Check Left Control key
            if (Input.GetKey(KeyCode.LeftControl))
            {
                leftCtrlPressed = true;
            }
            else
            {

               leftCtrlPressed = false;
            }












            // Check Right Control key
            if (!rightCtrlPressed && Input.GetKey(KeyCode.RightControl))
            {
                rightCtrlPressed = true;
            }
            else if (rightCtrlPressed && !Input.GetKey(KeyCode.RightControl))
            {
                rightCtrlPressed = false;
            }

            // Check Left Alt key
            if (!leftAltPressed && Input.GetKey(KeyCode.LeftAlt))
            {
                leftAltPressed = true;
            }
            else if (leftAltPressed && !Input.GetKey(KeyCode.LeftAlt))
            {
                leftAltPressed = false;
            }

            // Check Right Alt key
            if (!rightAltPressed && Input.GetKey(KeyCode.RightAlt))
            {
                rightAltPressed = true;

            }
            else if (rightAltPressed && !Input.GetKey(KeyCode.RightAlt))
            {
                rightAltPressed = false;
            }

            // Check Left Shift key
            if (!leftShiftPressed && Input.GetKey(KeyCode.LeftShift))
            {
                leftShiftPressed = true;
            }
            else if (leftShiftPressed && !Input.GetKey(KeyCode.LeftShift))
            {
                leftShiftPressed = false;
            }

            // Check Right Shift key
            if (!rightShiftPressed && Input.GetKey(KeyCode.RightShift))
            {
                rightShiftPressed = true;
            }
            else if (rightShiftPressed && !Input.GetKey(KeyCode.RightShift))
            {
                rightShiftPressed = false;
             }
        }

    }




}
