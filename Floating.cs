using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{

	[SerializeField] private float speedFloat;

	[SerializeField] private float speedRotate;

	[SerializeField] private float heightRange;
	
	[SerializeField] private float acceleration;

	private int acsent=1;

	[SerializeField] private float heightOriginal;

	private float speed;
	// Use this for initialization
	void Start ()
	{
		speed = speedFloat;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(acsent>0 && speed<speedFloat || acsent<0 && speed>-speedFloat)
			speed += acsent * Time.deltaTime*acceleration;
		transform.position += new Vector3(0, Time.deltaTime * speed, 0);
		transform.Rotate(Vector3.up, Time.deltaTime * speedRotate);
		
		if (acsent < 0 && transform.position.y < heightOriginal)
		{
			acsent = 1;
		}
		else if (acsent > 0 && transform.position.y > heightOriginal + heightRange)
		{
			acsent = -1;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			transform.root.gameObject.active = false;
		}
	}
}
