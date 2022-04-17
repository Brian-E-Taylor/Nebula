using System.Collections.Generic;
using UnityEngine;

public class NebulaSettings : MonoBehaviour
{
    public bool askForConfirmationBeforeDeletingPreset;
    public Vector3 spawnLocation;
    public List<StarData> starPresets;

    void OnEnable()
    {
        askForConfirmationBeforeDeletingPreset = true;
        spawnLocation = new Vector3(0f, 0f, 0f);
        starPresets ??= new List<StarData>();
    }
}
