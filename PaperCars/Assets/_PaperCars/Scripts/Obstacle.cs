using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private bool breakable = true;
    [SerializeField] private float forceToBreak;
    public float damagePerSecond;

    protected void Break()
    {
        Destroy(this.gameObject);
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
}
