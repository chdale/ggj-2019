using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public float speed;
    public Emotions feels;
    public SpriteRenderer portrait;

    public Animator animator;
    public List<Sprite> portraitList;

    public bool typeSentenceActive = false;

    //private Queue<DialogueSentence> sentences;
    private GameController gameController;
    private GameObject continueText;

    // Use this for initialization
    void Start()
    {
        //sentences = new Queue<DialogueSentence>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        continueText = transform.GetChild(0).Find("Canvas").GetChild(0).Find("Continue").gameObject;
    }

    public void StartDialogue(DialogueObject dialogue, bool skipText = true)
    {
        gameController.isInDialogue = true;
        animator.SetBool("IsOpen", true);
        
        if (!skipText)
        {
            continueText.SetActive(false);
        }
        else
        {
            continueText.SetActive(true);
        }

        DisplayNextSentence(dialogue);
    }

    public void DisplayNextSentence(DialogueObject dialogue)
    {
        Sprite image = portraitList.FirstOrDefault(x => x.name.Equals(string.Format("{0}_{1}", dialogue.Speaker.ToString(), dialogue.Feels), StringComparison.InvariantCultureIgnoreCase));
        if (image != null)
        {
            portrait.sprite = image;
        }
        else
        {
            portrait.sprite = portraitList.FirstOrDefault();
        }

        string name = dialogue.Speaker.GetDescription();

        nameText.text = GetName(name);

        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue.Text, dialogue.Speed, dialogue.Sound));
    }

    private string GetName(string name)
    {
        switch (name)
        {
            case "Medic":
                return GameStates.States[GameStates.MEDICNAME] ? "Riley" : name;
            default:
                return name;
        }
    }

    IEnumerator TypeSentence(string sentence, float speed, [CanBeNull] AudioSource clip)
    {
        typeSentenceActive = true;
        dialogueText.text = "";
        for (int i = 0; i < sentence.Length; i++)
        {
            dialogueText.text += sentence[i];
            if (i % 2 == 0 && clip != null)
            {
                clip.Play();
            }

            yield return new WaitForSeconds(speed);
        }
        typeSentenceActive = false;
    }

    public void FinishSentence(DialogueObject dialogue)
    {
        StopAllCoroutines();
        dialogueText.text = dialogue.Text;
        typeSentenceActive = false;
    }

    public void EndDialogue()
    {
        gameController.isInDialogue = false;
        animator.SetBool("IsOpen", false);
        gameController.CancelJumpEvent();
        gameController.EndDialogueEvent();
    }
    public void StartDialogueEvent(GameObject dialogueTarget, bool isStatic = false)
    {
        Camera.main.GetComponent<CameraController>().BDialogue(dialogueTarget, isStatic);
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        gameController.isInDialogue = true;
        gameController.StopCharacter();
    }
    public void EndDialogueEvent()
    {
        gameController.isInDialogue = false;
        Camera.main.GetComponent<CameraController>().EDialogue();
    }
}
