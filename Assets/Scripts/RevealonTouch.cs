using UnityEngine;

public class SquidVisionController : MonoBehaviour
{
    public Transform revealTarget; // e.g. tentacle tip
    public Material revealMat;
    public float revealRadius = 1.5f;

    void Update()
    {
        if (revealMat != null && revealTarget != null)
        {
            revealMat.SetVector("_RevealOrigin", revealTarget.position);
            revealMat.SetFloat("_RevealDistance", revealRadius);
        }
    }
}