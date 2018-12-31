using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField] private bool breakable = true;
    [SerializeField] private float forceToBreak;

    [Header("Components")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Collider2D coll;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem particle;

    private void Reset() 
    {
        breakable = true;
        forceToBreak = 25;

        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        particle = GetComponent<ParticleSystem>();
    }

    protected void Break()
    {
        sprite.enabled = false;
        coll.enabled = false;

        audioSource.Play();
        particle.Play();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!breakable)
            return;

        float impactForce = other.relativeVelocity.magnitude;

        Debug.Log("On Crate Collision: " + impactForce);

        if(impactForce >= forceToBreak)
        {
            Break();
        }
    }

    private void OnParticleSystemStopped()
    {
        Debug.LogWarning("OnParticleSystemStopped");
        Destroy(gameObject);
    }
}
