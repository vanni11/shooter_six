using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 6f;

	Vector3 movement;
	Rigidbody playerRigidbody;
	int floorMask;
	float camRayLength = 100f;

	private void Awake()
	{
		floorMask = LayerMask.GetMask("Floor");
		playerRigidbody = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move(h, v);
		Turning();
	}
	
	//플레이어를 움직인다.
	private void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition(transform.position + movement);
	}
	
	//마우스 포인터를 입력으로 받아 Ray를 쏴서 그 방향으로 플레이어를 회전시킨다.
	private void Turning()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;

		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
		{
			Vector3 playerToMouse = floorHit.point - transform.position;
			playerToMouse.y = 0f;

			Quaternion newRotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(newRotation);

			Debug.DrawLine(Camera.main.transform.position, floorHit.point, Color.yellow); //쏘는 Ray를 Scene뷰에서 확인용
		}
	}
}
