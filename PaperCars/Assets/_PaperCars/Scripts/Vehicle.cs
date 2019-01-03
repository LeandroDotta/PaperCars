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

    [Header("Components")]
    [SerializeField] private VehicleHealth health;
    [SerializeField] private PlayerController controller;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Finish"))
        {
            controller.enabled = false;
            StageManager.Current.Win();
        }
    }
}