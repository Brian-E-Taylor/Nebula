using UnityEngine;

[System.Serializable]
public class StarData
{
    // Custom Inspector visible values
    public string starName = "Star";
    public Color starColor;
    public float starRadius;
    public float gravityWellRadius;

    // Non-visible values
    public string guid = System.Guid.NewGuid().ToString();
}
