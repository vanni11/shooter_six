using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public bool canAttack; //적 맞히고 다시 데미지 못주게
	

	private void Start () {
		canAttack = true;
		Destroy(gameObject, 2f);
	}
}
