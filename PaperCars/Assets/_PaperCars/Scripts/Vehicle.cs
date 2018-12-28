using UnityEngine;

public class Vehicle : MonoBehaviour
{
    [Header("Movement")]
    public float speedForward;
    public float speedBackward;
    public float speedRotation;

    public float jumpForce;

    [Header("Health")]
    public float maxHealth = 100f;
    public float invulnerableTime;
    public float impactDamage = 5f;
    public float overturnedDamage = 2f;
}