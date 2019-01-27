using Assets.Hatch.Scripts.Dialogue;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
    public float speed;
    public Emotions feels;

	public Animator animator;

	//private Queue<DialogueSentence> sentences;
    private GameController gameController;

	// Use this for initialization
	void Start () {
		//sentences = new Queue<DialogueSentence>();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}

	public void StartDialogue (DialogueObject dialogue)
	{
		animator.SetBool("IsOpen", true);

		DisplayNextSentence(dialogue);
	}

	public void DisplayNextSentence (DialogueObject dialogue)
	{
        nameText.text = dialogue.Speaker.ToString();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(dialogue.Text, dialogue.Speed));
	}

	IEnumerator TypeSentence (string sentence, float speed)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
			yield return new WaitForSeconds(speed);
		}
	}

	public void EndDialogue()
	{
		animator.SetBool("IsOpen", false);
        gameController.CancelJumpEvent();
        gameController.EndDialogueEvent();
	}

}
