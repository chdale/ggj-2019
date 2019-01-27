using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStates
{
    public const string MEDIC = "MedicRescued";
    public const string ACCESSCODE = "AccessCodeEntered";

    public static Dictionary<string, bool> States = new Dictionary<string, bool>()
    {
        { MEDIC, false },
        { ACCESSCODE, false }
    };

}
