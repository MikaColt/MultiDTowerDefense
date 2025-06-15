using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSettings : MonoBehaviour
{
    // Singleton instance
    public static InputSettings Instance { get; private set; }



    public bool DisableKeyboard = false;
    public bool DisableController = false;
    public bool UseKeyboard = true;
    public bool UseController = false;

    private void Awake()
    {
        // Ensure that only one instance of InputSettings exists
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

    public void ActivateKeyboard()
    {
        UseKeyboard = true;
        UseController = false;
    }

    public void ActivateController()
    {
        UseKeyboard = false;
        UseController = true;
    }
}
