using UnityEngine;

public class Star : MonoBehaviour
{
    public StarData starData;
    private MeshRenderer _renderer;

    void OnValidate()
    {
        starData ??= new StarData();
        _renderer ??= gameObject.GetComponent<MeshRenderer>();
        _renderer.sharedMaterial ??= new Material(Shader.Find("Default"));

        if (!string.IsNullOrEmpty(starData.starName))
            name = starData.starName;
        else
            starData.starName = name;

        transform.localScale = new Vector3(starData.starRadius * 2f, starData.starRadius * 2f, starData.starRadius * 2f);
        _renderer.sharedMaterial.color = starData.starColor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position,  starData.gravityWellRadius);
    }
}
