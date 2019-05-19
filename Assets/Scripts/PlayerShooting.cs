using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	enum shootType
	{
		single_shot, //단발
		repeater_shot, //연발
		single_lazer, //단발 레이저
		repeater_lazer //연발 레이저
	}
	shootType nowShootType = shootType.single_shot;

	private void Update()
	{
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
		if (nowShootType == shootType.single_lazer)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Single_Shot_Lazer();
			}
		}
		if (nowShootType == shootType.repeater_lazer)
		{
			if (Input.GetMouseButton(0))
			{
				Repeat_Shot_Lazer();
			}
			else if(Input.GetMouseButtonUp(0))
			{
				Stop_Repeat_Shot_Lazer();
			}
		}

		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			gunLine.enabled = false; //LineRenderer 끔 (연사 레이져 남아있는거 없애기용)
			nowShootType = shootType.single_shot;
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			gunLine.enabled = false; //LineRenderer 끔 (연사 레이져 남아있는거 없애기용)
			nowShootType = shootType.repeater_shot;
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			gunLine.enabled = false; //LineRenderer 끔 (연사 레이져 남아있는거 없애기용)
			nowShootType = shootType.single_lazer;
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			nowShootType = shootType.repeater_lazer;
		}
	}


	public Transform shootPoint; //총구위치

	#region *bullet shooting*
	public GameObject bullet; //총알
	public float bulletSpeed; //총알속도 //30
	float repeat_timer = 0f; //연발간격 시간 초기화용 timer
	public float repeat_speed; //연발속도 //5
	
	

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
	#endregion

	#region *lazer*
	Ray shootRay;
	RaycastHit shootHit;
	LineRenderer gunLine;
	int shootableMask;

	private void Awake()
	{
		gunLine = GetComponentInChildren<LineRenderer>(); //총구에 붙어있는 LineRenderer가져옴
		gunLine.startWidth = 0.05f; //LineRenderer 굵기 설정
		shootableMask = LayerMask.GetMask("Shootable"); //Inspector에서 Layer를 Shootable로 설정한것들(방해물) 확인 
	}

	private void Single_Shot_Lazer()
	{
		StartCoroutine(LazerOneShot());
	}

	IEnumerator LazerOneShot()
	{
		gunLine.enabled = true; //라인그린다
		gunLine.SetPosition(0, shootPoint.position); //라인의 시작을 총구로
		shootRay.origin = shootPoint.position; //Ray의 시작을 총구로
		shootRay.direction = transform.forward; //Ray의 방향을 총구방향으로
		if (Physics.Raycast(shootRay, out shootHit, 100f, shootableMask)) //Ray쏴서 방해물에 맞으면
		{
			gunLine.SetPosition(1, shootHit.point); //라인의 끝을 -> 맞은곳으로
		}
		else //Ray쏴서 방해물에 맞지 않으면
		{
			gunLine.SetPosition(1, shootRay.origin + shootRay.direction * 100f); //라인의 끝을 -> 처음위치 + 방향*길이 (끝없이 라인)
		}
		yield return new WaitForSeconds(Time.deltaTime * 1.5f); //1.5프레임 기다리고
		gunLine.enabled = false; //LineRenderer 끔
	}

	private void Repeat_Shot_Lazer()
	{
		gunLine.enabled = true; //라인그린다
		gunLine.SetPosition(0, shootPoint.position); //라인의 시작을 총구로
		shootRay.origin = shootPoint.position; //Ray의 시작을 총구로
		shootRay.direction = transform.forward; //Ray의 방향을 총구방향으로
		if (Physics.Raycast(shootRay, out shootHit, 100f, shootableMask)) //Ray쏴서 방해물에 맞으면
		{
			gunLine.SetPosition(1, shootHit.point); //라인의 끝을 -> 맞은곳으로
		}
		else //방해물이 아닌것에 맞으면
		{
			gunLine.SetPosition(1, shootRay.origin + shootRay.direction * 100f); //라인의 끝을 -> 처음위치 + 방향*길이 (끝없이 라인)
		}
	}
	private void Stop_Repeat_Shot_Lazer()
	{
		gunLine.enabled = false; //LineRenderer 끔
	}
	#endregion
}
