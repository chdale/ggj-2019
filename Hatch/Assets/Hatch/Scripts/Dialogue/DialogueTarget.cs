﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public enum DialogueTarget
{
    [Description("You")]
    Player,
    [Description("Medic")]
    Medic,
    [Description("Ex-Engineer")]
    Engineer
}
