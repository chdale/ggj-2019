using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Inventory/List")]
public class DialogueTargetClass : ScriptableObject {
    public DialogueTarget dialogueTargetName = DialogueTarget.Player;
}
