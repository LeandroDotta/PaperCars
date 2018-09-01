using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    private Collider2D coll;

	public WheelJoint2D joint;
	public bool IsGrounded { get; private set; }

    private void Start()
    {
        coll = GetComponent<Collider2D>();
    }

	private void OnCollisionEnter2D(Collision2D other) 
	{
		if(other.collider.CompareTag("Ground"))
			IsGrounded = true;
	}

	private void OnCollisionExit2D(Collision2D other) 
	{
		if(other.collider.CompareTag("Ground"))	
			IsGrounded = false;
	}
}
