using UnityEngine;

public class Star : MonoBehaviour
{
    public StarData starData;
    private MeshRenderer _renderer;

    public void OnValidate()
    {
        starData ??= new StarData();
        _renderer ??= gameObject.GetComponent<MeshRenderer>();
        _renderer.sharedMaterial ??= new Material(Shader.Find("Default"));

        name = starData.starName;

        transform.localScale = new Vector3(starData.starRadius * 2f, starData.starRadius * 2f, starData.starRadius * 2f);
        _renderer.sharedMaterial.color = starData.starColor;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position,  starData.gravityWellRadius);
    }
}
