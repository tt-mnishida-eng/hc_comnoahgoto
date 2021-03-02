using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfettiPlayer : MonoBehaviour
{
    [SerializeField]
    ParticleSystem[] confetti;

    void Awake()
    {
        foreach (var particle in confetti)
        {
            particle.Stop();
            particle.gameObject.SetActive(false);
        }
    }

    public void Play()
    {
        foreach (var particle in confetti)
        {
            particle.gameObject.SetActive(true);
            particle.Play();
        }
    }

}
