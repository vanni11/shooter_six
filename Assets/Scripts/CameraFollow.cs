using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target; // 추적할 타겟
	public float smoothing = 5.0f; // 카메라가 움직이는 지연시간

	Vector3 offset; // 카메라와 플레이어의 거리 동일한 거리를 유지하기 위함

	private void Start()
	{
		offset = transform.position - target.position;
	}

    // 플레이어는 FixedUpdate에서 움직인다.
	private void FixedUpdate()
	{
		Vector3 targetCampos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCampos, smoothing * Time.deltaTime);
	}
}
