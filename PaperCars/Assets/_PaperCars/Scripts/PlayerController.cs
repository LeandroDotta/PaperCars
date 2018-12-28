using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private Wheel wheelBack;
    [SerializeField] private Wheel wheelFront;

    // INPUTs
    private bool jump;
    private float axisHorizontal;
    private bool rotateRight;
    private bool rotateLeft;

    // STATES
    private bool grounded;

    private Vehicle vehicle;

    private void Start()
    {
        vehicle = GetComponent<Vehicle>();
    }

    private void FixedUpdate()
    {
        if (vehicle == null)
            return;

        if (jump)
        {
            jump = false;
            rb2d.AddForce(Vector2.up * vehicle.jumpForce, ForceMode2D.Impulse);
        }

        if (axisHorizontal > 0)
        {
            wheelBack.joint.useMotor = true;

            JointMotor2D motorBack = wheelBack.joint.motor;
            motorBack.motorSpeed = -vehicle.speedForward;
            wheelBack.joint.motor = motorBack;

            JointMotor2D motorFront = wheelFront.joint.motor;
            motorFront.motorSpeed = -vehicle.speedForward;
            wheelFront.joint.motor = motorFront;
        }
        else if (axisHorizontal < 0)
        {
            wheelBack.joint.useMotor = true;

            JointMotor2D motorBack = wheelBack.joint.motor;
            motorBack.motorSpeed = vehicle.speedBackward;
            wheelBack.joint.motor = motorBack;

            JointMotor2D motorFront = wheelFront.joint.motor;
            motorFront.motorSpeed = vehicle.speedBackward;
            wheelFront.joint.motor = motorFront;
        }
        else
        {
            wheelBack.joint.useMotor = false;

            JointMotor2D motorBack = wheelBack.joint.motor;
            motorBack.motorSpeed = 0;
            wheelBack.joint.motor = motorBack;

            JointMotor2D motorFront = wheelFront.joint.motor;
            motorFront.motorSpeed = 0;
            wheelFront.joint.motor = motorFront;
        }

        if (rotateRight)
        {
            rb2d.angularVelocity = -vehicle.speedRotation;
        }
        else if (rotateLeft)
        {
            rb2d.angularVelocity = vehicle.speedRotation;
        }
    }

    private void Update()
    {
        axisHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }

        rotateRight = Input.GetButton("RotateRight");
        rotateLeft = Input.GetButton("RotateLeft");

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            grounded = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ground"))
            grounded = false;
    }

    // private void OnGUI()
    // {
    //     GUI.color = Color.black;

    //     float y = 20;
    //     Rect rect = new Rect(10, 10, 700, 20);
    //     GUI.Label(rect, "Grounded: " + grounded); rect.y += y;
    //     GUI.Label(rect, "Wheel B Grounded: " + wheelBack.IsGrounded); rect.y += y;
    //     GUI.Label(rect, "Wheel F Grounded: " + wheelFront.IsGrounded); rect.y += y;
    //     GUI.Label(rect, "Velocity: " + rb2d.velocity); rect.y += y;
    //     GUI.Label(rect, "Angular Velocity: " + rb2d.angularVelocity); rect.y += y;
    //     GUI.Label(rect, "Velocity Magnitude: " + rb2d.velocity.magnitude); rect.y += y;

    //     GUI.Label(rect, "INPUTS:"); rect.y += y;
    //     GUI.Label(rect, "Horizontal Axis: " + axisHorizontal); rect.y += y;
    //     GUI.Label(rect, "Jump: " + jump); rect.y += y;
    //     GUI.Label(rect, "RotateLeft: " + rotateLeft); rect.y += y;
    //     GUI.Label(rect, "RotateRight: " + rotateRight); rect.y += y;
    //     GUI.Label(rect, "Health: " + health); rect.y += y;
    // }
}