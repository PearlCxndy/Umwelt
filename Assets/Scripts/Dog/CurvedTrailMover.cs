using UnityEngine;

public class CurvedTrailMover : MonoBehaviour
{
    private Transform startPoint;
    private Transform endPoint;
    private float timer = 0f;
    private Vector3 controlPointOffset;
    private bool isAnimating = false;

    [SerializeField] private float duration = 2f;
    [SerializeField] private float curveStrength = 1f;
    [SerializeField] private float fadeDuration = 10f; // <-- New fade duration

    private ParticleSystem ps;
    private LineRenderer lineRenderer;
    private bool fading = false;
    private float fadeTimer = 0f;
    private Color startColor;

    public void Initialize(Transform start, Transform end)
    {
        startPoint = start;
        endPoint = end;

        // Start particle system
        ps = GetComponentInChildren<ParticleSystem>();
        if (ps != null)
        {
            ps.Clear();
            ps.Play();
        }

        // Setup curve direction
        Vector3 direction = endPoint.position - startPoint.position;
        Vector3 perpendicular = Vector3.Cross(direction, Vector3.up).normalized;
        float side = Random.value > 0.5f ? 1f : -1f;
        controlPointOffset = perpendicular * curveStrength * side;

        // Position and start animation
        isAnimating = true;
        transform.position = startPoint.position;

        // Draw the permanent trail
        DrawCurve();

        Debug.Log("Initialized from " + startPoint.name + " to " + endPoint.name);
    }

    private void Update()
    {
        if (isAnimating && startPoint != null && endPoint != null)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);

            // Bezier curve
            Vector3 p0 = startPoint.position;
            Vector3 p1 = (startPoint.position + endPoint.position) / 2f + controlPointOffset;
            Vector3 p2 = endPoint.position;

            Vector3 curvedPos = Mathf.Pow(1 - t, 2) * p0 +
                                2 * (1 - t) * t * p1 +
                                Mathf.Pow(t, 2) * p2;

            transform.position = curvedPos;

            if (t >= 1f)
            {
                transform.position = endPoint.position;

                if (ps != null)
                    ps.Stop(true, ParticleSystemStopBehavior.StopEmitting);

                Debug.Log("Reached final destination.");

                // Start fading after reaching destination
                StartFade();
                isAnimating = false;
            }
        }

        if (fading)
        {
            FadeTrail();
        }
    }

    private void DrawCurve()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (!lineRenderer)
        {
            Debug.LogWarning("LineRenderer component missing!");
            return;
        }

        int segments = 4;
        lineRenderer.positionCount = segments;
        lineRenderer.useWorldSpace = true;

        Vector3 p0 = startPoint.position;
        Vector3 p2 = endPoint.position;
        Vector3 p1 = (p0 + p2) * 0.5f + controlPointOffset;

        for (int i = 0; i < segments; i++)
        {
            float t = i / (segments - 1f);
            Vector3 point = Mathf.Pow(1 - t, 2) * p0 +
                            2 * (1 - t) * t * p1 +
                            Mathf.Pow(t, 2) * p2;
            lineRenderer.SetPosition(i, point);
        }

        startColor = lineRenderer.startColor; // Save initial color
        Debug.Log("LineRenderer curve drawn.");
    }

    private void StartFade()
    {
        fading = true;
        fadeTimer = 0f;
    }

    private void FadeTrail()
    {
        fadeTimer += Time.deltaTime;
        float fadeT = Mathf.Clamp01(fadeTimer / fadeDuration);

        Color fadedColor = Color.Lerp(startColor, new Color(startColor.r, startColor.g, startColor.b, 0), fadeT);
        lineRenderer.startColor = fadedColor;
        lineRenderer.endColor = fadedColor;

        if (fadeT >= 1f)
        {
            Destroy(gameObject);
            Debug.Log("Trail fully faded and destroyed.");
        }
    }
}
