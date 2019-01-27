using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour {
    public DialogueTarget Speaker;
    public string Text;
    public float Speed;
    public Emotions Feels;

    public DialogueObject(DialogueTarget speaker, string text, float speed, Emotions emotion)
    {
        Speaker = speaker;
        Text = text;
        Speed = speed;
        Feels = emotion;
    }
}
