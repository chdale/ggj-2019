using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeypadController : MonoBehaviour
{
    public static string AccessCode = "12345";
    private string _input = string.Empty;
    public int MaxKeys = 5;
    public Text inputDisplay;

    //private int[] input = new int[5];
    //private int[] display = new int[5];

	// Use this for initialization
	void Start ()
	{
	    inputDisplay = this.GetComponentInChildren<Text>();
	    inputDisplay.text = string.Empty;
	}
	
	// Update is called once per frame
	void Update () {

	    if (inputDisplay.text.Length < MaxKeys)
	    {
	        if (Input.GetKey(KeyCode.Keypad0))
	        {
	            UpdateInput("0");
	        }
	        if (Input.GetKey(KeyCode.Keypad1))
	        {
	            UpdateInput("1");
            }
	        if (Input.GetKey(KeyCode.Keypad2))
	        {
	            UpdateInput("2");
            }
	        if (Input.GetKey(KeyCode.Keypad3))
	        {
	            UpdateInput("3");
            }
	        if (Input.GetKey(KeyCode.Keypad4))
	        {
	            UpdateInput("4");
            }
	        if (Input.GetKey(KeyCode.Keypad5))
	        {
	            UpdateInput("5");
            }
	        if (Input.GetKey(KeyCode.Keypad6))
	        {
	            UpdateInput("6");
            }
	        if (Input.GetKey(KeyCode.Keypad7))
	        {
	            UpdateInput("7");
            }
	        if (Input.GetKey(KeyCode.Keypad8))
	        {
	            UpdateInput("8");
            }
	        if (Input.GetKey(KeyCode.Keypad9))
	        {
	            UpdateInput("9");
            }
        }
	    else
	    {
	        if (_input.Equals(AccessCode))
	        {
	            Debug.Log("Congratulations.  That's the correct code!");
	        }
	        else
	        {
	            Debug.Log("Wow, try again...");
                Clear();
	        }
	    }
	    
	}

    public void Clear()
    {
        inputDisplay.text = string.Empty;
        _input = string.Empty;
    }

    IEnumerator KeyEntered()
    {
        var enteredCharacters = inputDisplay.text.ToCharArray();
        enteredCharacters[inputDisplay.text.Length - 1] = '*';
        inputDisplay.text = enteredCharacters.ToString();

        yield return null;
    }

    private void UpdateInput(string keyEntered)
    {
        inputDisplay.text += keyEntered;
        _input += keyEntered;
        StopAllCoroutines();
        StartCoroutine(KeyEntered());
    }
}
