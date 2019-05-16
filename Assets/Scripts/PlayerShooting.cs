using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	public GameObject bullet; //총알
	public Transform shootPoint; //총구위치
	public float bulletSpeed; //총알속도 //30
	float repeat_timer = 0f; //연발간격 시간 초기화용 timer
	public float repeat_speed; //연발속도 //5

	enum shootType
	{
		single_shot, //단발
		repeater_shot, //연발
		single_lazer, //단발 레이저
		repeater_lazer //연발 레이저
	}
	shootType nowShootType = shootType.single_shot;

	private void Update () {
		if (nowShootType == shootType.single_shot)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Single_Shot_Fire();
			}
		}

		if (nowShootType == shootType.repeater_shot)
		{
			if (Input.GetMouseButton(0))
			{
				Repeater_Shot_Fire();
			}
		}

		if (Input.GetKeyDown(KeyCode.B))
		{
			nowShootType = (nowShootType == shootType.single_shot) ? shootType.repeater_shot : shootType.single_shot;
		}
	}

	private void Single_Shot_Fire()
	{
		GameObject bulletClone = Instantiate(bullet, shootPoint.position, Quaternion.identity);
		bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
	}
	private void Repeater_Shot_Fire()
	{
		repeat_timer += Time.deltaTime;
		if(repeat_timer > 1 / repeat_speed)
		{
			GameObject bulletClone = Instantiate(bullet, shootPoint.position, Quaternion.identity);
			bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
			repeat_timer = 0f;
		}
	}
}
