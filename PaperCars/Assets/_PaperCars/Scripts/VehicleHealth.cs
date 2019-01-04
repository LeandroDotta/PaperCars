using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class VehicleHealth : MonoBehaviour
{
    [SerializeField] private Collider2D hitbox;
    [SerializeField] private HealthBar healthBar;

    private Vehicle vehicle;

    public float CurrentHealth { get; private set; }
    public bool IsInvulnerable { get; private set; }
    public bool IsOverturned { get; private set; }

    // Events
    public UnityAction<float> OnHealthChanged;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();

        if (vehicle != null)
        {
            CurrentHealth = vehicle.maxHealth;
        }

        StartCoroutine("OverturnedDamageCoroutine");

        if (healthBar == null)
        {
            healthBar = GameObject.FindObjectOfType<HealthBar>();
        }
    }

    private void OnEnable()
    {
        OnHealthChanged += OnHealthChangedCallback;
    }

    private void OnDisable()
    {
        OnHealthChanged -= OnHealthChangedCallback;
    }

    private void CheckImpactDamage(Collision2D other)
    {
        if (!IsInvulnerable)
        {
            foreach (ContactPoint2D contact in other.contacts)
            {
                if (hitbox.OverlapPoint(contact.point))
                {
                    Debug.Log("Relative Velocity: " + other.relativeVelocity.magnitude);
                    Debug.Log("Deal Damage!");

                    int damageMultiplier = Mathf.FloorToInt(other.relativeVelocity.magnitude / 10);
                    CurrentHealth -= vehicle.impactDamage * damageMultiplier;

                    if (OnHealthChanged != null)
                        OnHealthChanged.Invoke(CurrentHealth);

                    healthBar.SetHealth(CurrentHealth);

                    StartCoroutine("InvulnerableCoroutine");
                    break;
                }
            }
        }
    }

    private void CheckConstantDamage(Collision2D other)
    {
        float zRotation = transform.rotation.eulerAngles.z;
        if (zRotation > 120 && zRotation < 240) // Is overturned
        {
            IsOverturned = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        CheckImpactDamage(other);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        CheckConstantDamage(other);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        IsOverturned = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Hazard hazard = other.GetComponent<Hazard>();

        if (hazard == null)
            return;

        if (hazard.type == HazardType.DamagePerSecond)
        {
            StopCoroutine("DamagePerSecondCoroutine");
            StartCoroutine("DamagePerSecondCoroutine", hazard.damage);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Hazard hazard = other.GetComponent<Hazard>();

        if (hazard == null)
            return;

        if (hazard.type == HazardType.DamagePerSecond)
        {
            StopCoroutine("DamagePerSecondCoroutine");
        }
    }

    private IEnumerator InvulnerableCoroutine()
    {
        IsInvulnerable = true;

        yield return new WaitForSeconds(vehicle.invulnerableTime);

        IsInvulnerable = false;
    }

    private IEnumerator OverturnedDamageCoroutine()
    {
        while (true)
        {
            yield return null;

            if (IsOverturned)
            {
                CurrentHealth -= vehicle.overturnedDamage * Time.deltaTime;
                healthBar.SetHealth(CurrentHealth);

                if (OnHealthChanged != null)
                    OnHealthChanged.Invoke(CurrentHealth);
            }
        }
    }

    private IEnumerator DamagePerSecondCoroutine(float damagePerSecond)
    {
        while (true)
        {
            yield return null;

            CurrentHealth -= damagePerSecond * Time.deltaTime;
            healthBar.SetHealth(CurrentHealth);

            if (OnHealthChanged != null)
                OnHealthChanged.Invoke(CurrentHealth);
        }
    }

    private void OnHealthChangedCallback(float health)
    {
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
