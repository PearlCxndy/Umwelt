using UnityEngine;

public class SpringFollower : MonoBehaviour
{
    public Transform target;
    SpringJoint spring;

    void Start()
    {
        spring = GetComponent<SpringJoint>();
    }

    void Update()
    {
        if (spring != null && target != null)
            spring.connectedAnchor = target.position;
    }
}