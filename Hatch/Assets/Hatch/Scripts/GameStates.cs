using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStates
{
    public const string MEDIC = "MedicRescued";
    public const string MEDICNAME = "DiscoveredMedicName";
    public const string CARD = "AccessCardFound";
    public const string ACCESSCODE = "AccessCodeEntered";
    public const string DOG = "DogEncountered";

    public static Dictionary<string, bool> States = new Dictionary<string, bool>()
    {
        { MEDIC, false },
        { MEDICNAME, false},
        { CARD, false },
        { ACCESSCODE, false },
        { DOG, false }
    };

}
