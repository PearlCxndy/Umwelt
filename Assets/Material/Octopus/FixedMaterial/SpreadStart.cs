using UnityEngine;

public class FireSpreadTrigger : MonoBehaviour
{
    public Material materialToControl;

    [Header("Timing")]
    public float startingTime = 20f;
    public float timerSpeed = 1f; // Normal speed multiplier
    public float finalSlowDownFactor = 0.2f; // Slowdown multiplier near the end

    [Tooltip("The countdown will slow down when time is under this threshold")]
    public float slowDownThreshold = 1f;

    [Header("Tentacle Layer Detection")]
    public LayerMask tentacleLayer;

    private float countdown;
    private bool isTriggered = false;

    void Start()
    {
        countdown = startingTime;
        materialToControl.SetFloat("_CountdownTime", countdown);
    }

    void Update()
    {
        if (isTriggered && countdown > 0f)
        {
            float delta = Time.deltaTime;

            // If we're below the threshold, apply slow-down multiplier
            if (countdown <= slowDownThreshold)
            {
                delta *= finalSlowDownFactor;
            }

            // Decrease countdown with speed factor
            countdown -= delta * timerSpeed;

            // Clamp to not go below zero
            countdown = Mathf.Max(0f, countdown);

            // Pass to shader
            materialToControl.SetFloat("_CountdownTime", countdown);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & tentacleLayer) != 0 && !isTriggered)
        {
            isTriggered = true;
        }
    }
}