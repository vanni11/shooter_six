using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 6.0f;

    Vector3 movement;
    Rigidbody playerRigidbody;
    int floorMask;
    float camRayLength = 100.0f;
	public Animator anim;

    // Start()와 유사하지만 스크립트 활성에 상관없이 호출되기 때문에 참조 설정에 유리하다
    private void Awake()
    {
        floorMask = LayerMask.GetMask("Floor");
        playerRigidbody = GetComponent<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
	}

	// 모든 Physics Update를 유발한다.
	// Update는 렌더링과 함께 실행되지만 FixedUpdate는 물리효과와 함께 실행된다.
	// 물리효과 캐릭터를 이동시키고 있으므로 Update가 아닌 FixedUpdate를 이용한다.
	private void FixedUpdate()
    {
        // 원래 입력은 -1 ~ 1 사이의 값을 가지지만 다음의 코드를 쓰면 -1, 0, 1 세가지 값 외에는 가지지 않는다.
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animating(h, v);
    }

    //플레이어를 움직인다.
    void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;
		playerRigidbody.MovePosition(transform.position + movement);
	}
	
	//마우스 포인터를 입력으로 받아 Ray를 쏴서 그 방향으로 플레이어를 회전시킨다.
	void Turning()
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

    void Animating(float h, float v)
    {
		bool walking = h != 0.0f || v != 0.0f;
        anim.SetBool("IsWalking", walking);
    }
}
