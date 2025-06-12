using UnityEngine;
using UnityEngine.Animations.Rigging;

public class TentacleStretchBlend : MonoBehaviour
{
    public Transform tipBone;
    public Transform target;
    public ChainIKConstraint chainIK;
    public float maxStretchDistance = 5f;
    public float weightSmoothSpeed = 5f;

    void Update()
    {
        float dist = Vector3.Distance(tipBone.position, target.position);
        float t = Mathf.Clamp01(dist / maxStretchDistance);
        chainIK.weight = Mathf.Lerp(chainIK.weight, t, Time.deltaTime * weightSmoothSpeed);
    }
}