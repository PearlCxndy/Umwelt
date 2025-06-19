using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AudioSlowdown : MonoBehaviour
{
    [Header("Player & Audio")]
    public Transform playerTransform;
    private AudioSource currentSource;
    private float normalPitch = 1f;

    [Header("Playback Control")]
    [Range(0.01f, 1f)]
    public float playbackSpeed = 1f;

    [Header("UI Display")]
    public TextMeshProUGUI pitchText; // Optional UI to show playback speed

    void Update()
    {
        AudioSource nearest = FindClosestAudioSource();

        // If the closest source changed, reset the previous one
        if (nearest != currentSource)
        {
            ResetOtherSources();
            currentSource = nearest;
        }

        // Handle key inputs
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playbackSpeed = Mathf.Max(0.1f, playbackSpeed - 0.1f);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playbackSpeed = Mathf.Min(1.0f, playbackSpeed + 0.1f);
        }

        // Apply pitch to current source
        if (currentSource != null)
        {
            currentSource.pitch = playbackSpeed;
            UpdatePitchUI(playbackSpeed);
        }
    }

    private AudioSource FindClosestAudioSource()
    {
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        AudioSource closest = null;
        float minDist = float.MaxValue;

        foreach (AudioSource src in sources)
        {
            if (!src.isPlaying) continue;

            float dist = Vector3.Distance(playerTransform.position, src.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                closest = src;
            }
        }

        return closest;
    }

    private void ResetOtherSources()
    {
        foreach (AudioSource src in FindObjectsOfType<AudioSource>())
        {
            if (src != currentSource)
            {
                src.pitch = normalPitch;
            }
        }
    }

    private void UpdatePitchUI(float pitch)
    {
        if (pitchText != null)
        {
            pitchText.text = $"Speed: {pitch:F1}x";
        }
    }
}

