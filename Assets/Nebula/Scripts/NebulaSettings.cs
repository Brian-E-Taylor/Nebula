using System.Collections.Generic;
using UnityEngine;

public class NebulaSettings : MonoBehaviour
{
    public bool askForConfirmationBeforeDeletingPreset = true;
    public Vector3 spawnLocation = new Vector3(0f, 0f, 0f);
    public List<StarData> starPresets = new List<StarData>();
}
