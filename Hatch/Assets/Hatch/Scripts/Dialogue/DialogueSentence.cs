using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Hatch.Scripts.Dialogue
{
    [System.Serializable]
    public class DialogueSentence
    {
        [TextArea(3, 10)]
        public string Text;
        public string name;
    }
}
