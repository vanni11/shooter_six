using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum shootType
{
	single_bullet, //단발
	repeater_bullet, //연발
	single_laser, //단발 레이저
	repeater_laser //연발 레이저
}

public class PlayerShooting : MonoBehaviour
{
	Transform shootPoint; //총구위치

	public GameObject bullet; //총알
	public GameObject bullet2; //총알
	public float bulletSpeed; //총알속도 //40
	public float bulletSpeed2; //총알속도 //50
	float repeat_timer = 0f; //연발간격 시간 초기화용 timer
	public float repeat_speed; //연발속도 //10

	Ray shootRay;
	RaycastHit shootHit;
	public LineRenderer gunLine;
	public LineRenderer gunLine2;
	int shootableMask;
	
	public shootType nowShootType = shootType.single_bullet;
	shootType saveBulletMode = shootType.single_bullet;
	shootType saveLaserMode = shootType.single_laser;

	private void Awake()
	{
		shootPoint = GameObject.Find("ShootPoint").transform;
		//gunLine = GetComponentInChildren<LineRenderer>(); //총구에 붙어있는 LineRenderer가져옴 //active상태가 아니라서 가져오지 못함 - public으로 직접참조하게 바꿈
		shootableMask = LayerMask.GetMask("Shootable"); //Inspector에서 Layer를 Shootable로 설정한것들(방해물) 확인
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			nowShootType = saveBulletMode;
			shootPoint = GameObject.Find("ShootPoint").transform;
			RemoveLaser();
		}
		else if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			nowShootType = saveLaserMode;
			shootPoint = GameObject.Find("ShootPoint").transform;
			RemoveLaser();
		}
		else if (Input.GetKeyDown(KeyCode.Space))
		{
			if (nowShootType == shootType.single_bullet || nowShootType == shootType.repeater_bullet)
			{
				nowShootType = nowShootType == shootType.single_bullet ? shootType.repeater_bullet : shootType.single_bullet;
				saveBulletMode = nowShootType;
			}
			else if (nowShootType == shootType.single_laser || nowShootType == shootType.repeater_laser)
			{
				nowShootType = nowShootType == shootType.single_laser ? shootType.repeater_laser : shootType.single_laser;
				saveLaserMode = nowShootType;
			}
			RemoveLaser();
		}

		if (nowShootType == shootType.single_bullet)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Single_Bullet_Fire();
			}
		}
		else if (nowShootType == shootType.repeater_bullet)
		{
			if (Input.GetMouseButton(0))
			{
				Repeater_Bullet_Fire();
			}
		}
		else if (nowShootType == shootType.single_laser)
		{
			if (Input.GetMouseButtonDown(0))
			{
				Single_Laser_Fire();
			}
		}
		else if (nowShootType == shootType.repeater_laser)
		{
			if (Input.GetMouseButton(0))
			{
				Repeat_Laser_Fire();
			}
			else if (Input.GetMouseButtonUp(0))
			{
				RemoveLaser();
			}
		}
	}

	private void Single_Bullet_Fire()
	{
		GameObject bulletClone = Instantiate(bullet, shootPoint.position, gameObject.transform.rotation); //단발 총알의 첫 모양을 위해 player의 rotation 가져와서 적용
		bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
	}

	private void Repeater_Bullet_Fire()
	{
		repeat_timer += Time.deltaTime;
		if (repeat_timer > 1 / repeat_speed)
		{
			GameObject bulletClone = Instantiate(bullet2, shootPoint.position, Quaternion.identity);
			bulletClone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed2;
			repeat_timer = 0f;
		}
	}

	private void Single_Laser_Fire()
	{
		StartCoroutine(LaserOneShot());
	}

	IEnumerator LaserOneShot()
	{
		gunLine.enabled = true; //라인그린다
		gunLine.SetPosition(0, shootPoint.position); //라인의 시작을 총구로
		shootRay.origin = shootPoint.position; //Ray의 시작을 총구로
		shootRay.direction = transform.forward; //Ray의 방향을 총구방향으로

		DrawLaser();
		yield return new WaitForSeconds(Time.deltaTime * 1.5f); //1.5프레임 기다리고
		gunLine.enabled = false; //LineRenderer 끔
	}

	private void Repeat_Laser_Fire()
	{
		gunLine2.enabled = true; //라인그린다
		gunLine2.SetPosition(0, shootPoint.position); //라인의 시작을 총구로
		shootRay.origin = shootPoint.position; //Ray의 시작을 총구로
		shootRay.direction = transform.forward; //Ray의 방향을 총구방향으로

		DrawLaser2();
	}

	void DrawLaser()
	{
		if (Physics.Raycast(shootRay, out shootHit, 100f, shootableMask)) //Ray쏴서 방해물에 맞으면
		{
			gunLine.SetPosition(1, shootHit.point); //라인의 끝을 -> 맞은곳으로
			if (shootHit.transform.name == "EnemyLaser(Clone)")
			{
				EnemyHealthLaser health = shootHit.transform.GetComponent<EnemyHealthLaser>();
				health.TakeDamage(20, health.gameObject.transform.position);
			}
			else if(shootHit.transform.name == "EnemyEvery(Clone)")
			{
				EnemyHealthEvery health = shootHit.transform.GetComponent<EnemyHealthEvery>();
				health.TakeDamage(20, health.gameObject.transform.position);
			}
		}
		else //방해물이 아닌것에 맞으면
		{
			gunLine.SetPosition(1, shootRay.origin + shootRay.direction * 100f); //라인의 끝을 -> 처음위치 + 방향*길이 (끝없이 라인)
		}
	}

	void DrawLaser2()
	{
		if (Physics.Raycast(shootRay, out shootHit, 100f, shootableMask)) //Ray쏴서 방해물에 맞으면
		{
			gunLine2.SetPosition(1, shootHit.point); //라인의 끝을 -> 맞은곳으로
			if (shootHit.transform.name == "EnemyLaser2(Clone)")
			{
				EnemyHealthLaser health = shootHit.transform.GetComponent<EnemyHealthLaser>();
				health.TakeDamage(5, health.gameObject.transform.position);
			}
			else if (shootHit.transform.name == "EnemyEvery(Clone)")
			{
				EnemyHealthEvery health = shootHit.transform.GetComponent<EnemyHealthEvery>();
				health.TakeDamage(5, health.gameObject.transform.position);
			}
		}
		else //방해물이 아닌것에 맞으면
		{
			gunLine2.SetPosition(1, shootRay.origin + shootRay.direction * 100f); //라인의 끝을 -> 처음위치 + 방향*길이 (끝없이 라인)
		}
	}

	private void RemoveLaser()
	{
		gunLine2.enabled = false; //LineRenderer 끔 (연사 레이져 남아있는거 없애기용)
	}
}