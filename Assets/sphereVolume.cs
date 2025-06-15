using UnityEngine;

public class SphereVolumeController : MonoBehaviour
{
    public Transform target; // your dog transform
    public Material sphereMaterial;
    public float sphereRadius = 5f;

    void Update()
    {
        if (target == null || sphereMaterial == null) return;

        sphereMaterial.SetVector("_SphereCenter", target.position);
        sphereMaterial.SetFloat("_SphereRadius", sphereRadius);
    }
}
