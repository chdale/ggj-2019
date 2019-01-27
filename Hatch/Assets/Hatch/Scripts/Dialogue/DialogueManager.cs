using Assets.Hatch.Scripts.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {

	public Text nameText;
	public Text dialogueText;
    public float speed;
    public Emotions feels;
    public SpriteRenderer portrait;

	public Animator animator;
    public List<Sprite> portraitList;

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
        Sprite image = portraitList.FirstOrDefault(x => x.name.Equals(string.Format("{0}_{1}", dialogue.Speaker.ToString(), dialogue.Feels), StringComparison.InvariantCultureIgnoreCase));
        if (image != null)
        {
            portrait.sprite = image;
        }
        else
        {
            portrait.sprite = portraitList.FirstOrDefault();
        }

        nameText.text = dialogue.Speaker.GetDescription();

		StopAllCoroutines();
		StartCoroutine(TypeSentence(dialogue.Text, dialogue.Speed, dialogue.Sound));
	}

	IEnumerator TypeSentence (string sentence, float speed, [CanBeNull] AudioSource clip)
	{
		dialogueText.text = "";
		foreach (char letter in sentence.ToCharArray())
		{
			dialogueText.text += letter;
		    if (clip != null)
		    {
		        clip.Play();
		    }
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
