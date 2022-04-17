using System.Collections.Generic;
using UnityEngine;

public class NebulaSettings : MonoBehaviour
{
    public bool askForConfirmationBeforeDeletingPreset;
    public List<StarData> starPresets;

    void OnEnable()
    {
        askForConfirmationBeforeDeletingPreset = true;
        starPresets ??= new List<StarData>();
    }
}
