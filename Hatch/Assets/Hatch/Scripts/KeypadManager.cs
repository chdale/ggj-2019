using System.Collections;
using System.Linq;
using Assets.Hatch.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;

public class KeypadManager : MonoBehaviour
{
    public Text displayText;
    public string keyCode = "12345";
    public int maxKeys = 5;

    private string _input;
    private OpenModal openKeypad;
    private AudioSource buttonPress;
    private GameObject numPad;
    private bool keypadActive;

    //public OpenKeypad openKeypad;

    void Start()
    {
        numPad = transform.GetChild(0).GetChild(0).GetChild(2).gameObject;
        displayText = transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponentInChildren<Text>();
        displayText.text = string.Empty;
        _input = string.Empty;
        openKeypad = GameObject.Find("OpenKeypad").GetComponent<OpenModal>();
        buttonPress = GetComponent<AudioSource>();
        GameController.FinishModal += DeactivateKeypad;
    }

    // Update is called once per frame
    void Update()
    {
        if (displayText.text.Length < maxKeys && !GameStates.States[GameStates.ACCESSCODE])
        {
            if (Input.GetKeyDown(KeyCode.Keypad0) || Input.GetKeyDown(KeyCode.Alpha0))
            {
                UpdateInput("0");
            }
            if (Input.GetKeyDown(KeyCode.Keypad1) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                UpdateInput("1");
            }
            if (Input.GetKeyDown(KeyCode.Keypad2) || Input.GetKeyDown(KeyCode.Alpha2))
            {
                UpdateInput("2");
            }
            if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Alpha3))
            {
                UpdateInput("3");
            }
            if (Input.GetKeyDown(KeyCode.Keypad4) || Input.GetKeyDown(KeyCode.Alpha4))
            {
                UpdateInput("4");
            }
            if (Input.GetKeyDown(KeyCode.Keypad5) || Input.GetKeyDown(KeyCode.Alpha5))
            {
                UpdateInput("5");
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.Alpha6))
            {
                UpdateInput("6");
            }
            if (Input.GetKeyDown(KeyCode.Keypad7) || Input.GetKeyDown(KeyCode.Alpha7))
            {
                UpdateInput("7");
            }
            if (Input.GetKeyDown(KeyCode.Keypad8) || Input.GetKeyDown(KeyCode.Alpha8))
            {
                UpdateInput("8");
            }
            if (Input.GetKeyDown(KeyCode.Keypad9) || Input.GetKeyDown(KeyCode.Alpha9))
            {
                UpdateInput("9");
            }

            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Clear();
            }
        }
        else
        {
            if (_input.Equals(keyCode))
            {
                Debug.Log("Congratulations.  That's the correct code!");
                Clear();
                transform.gameObject.SetActive(false);
                openKeypad.Success();
                
            }
            else
            {
                Debug.Log("Wow, try again...");
                Clear();
            }
        }
    }

    private void UpdateInput(string keyEntered)
    {
        ToggleButtonUI(keyEntered);
        buttonPress.Play();
        displayText.text += keyEntered;
        _input += keyEntered;
        Invoke("KeyEntered", .2f);
    }

    void KeyEntered()
    {
        if (!string.IsNullOrEmpty(displayText.text))
        {
            var enteredCharacters = displayText.text.ToCharArray();
            enteredCharacters[displayText.text.Length - 1] = '*';
            displayText.text = new string(enteredCharacters);
        }
    }

    public void Clear()
    {
        displayText.text = string.Empty;
        _input = string.Empty;
    }

    private void ToggleButtonUI(string keyEntered)
    {
        var numbers = numPad.GetComponentsInChildren<Button>();
        var selectedButton = numbers.First(x => x.name.Contains(keyEntered));
        var sprite = selectedButton.image.sprite;
        selectedButton.image.sprite = selectedButton.spriteState.pressedSprite;
        StartCoroutine(SwapSprite(sprite, selectedButton, .1f));
    }

    private void DeactivateKeypad()
    {
        Clear();
        transform.gameObject.SetActive(false);
    }

    IEnumerator SwapSprite(Sprite sprite, Button button, float time)
    {
        yield return new WaitForSeconds(time);
        button.image.sprite = sprite;
    }
}
