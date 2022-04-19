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

    public StarData()
    {
    }

    public StarData(StarData other)
    {
        starName = other.starName;
        starColor = other.starColor;
        starRadius = other.starRadius;
        gravityWellRadius = other.gravityWellRadius;

        // Don't copy GUID because it needs to be unique
        guid = System.Guid.NewGuid().ToString();
    }
}
