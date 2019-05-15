using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public GameObject bullet;
	public Transform shootPoint;
	public float bulletSpeed;

	void Start () {
		
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			Fire();
		}
	}

	void Fire()
	{
		GameObject bulletClone = Instantiate(bullet, shootPoint.position, Quaternion.identity);
		bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
	}
}
