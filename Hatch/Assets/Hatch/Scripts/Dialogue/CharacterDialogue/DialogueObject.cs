using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class DialogueObject {
    public DialogueTarget Speaker;
    public string Text;
    public float Speed;
    public Emotions Feels;
    [CanBeNull] public AudioSource Sound;

    public DialogueObject(DialogueTarget speaker, string text, float speed, Emotions emotion, [CanBeNull] AudioSource sound)
    {
        Speaker = speaker;
        Text = text;
        Speed = speed;
        Feels = emotion;
        Sound = sound;
    }
}
