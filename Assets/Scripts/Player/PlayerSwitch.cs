using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
	public List<GameObject> playerObjects = new List<GameObject>();

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			playerObjects[0].SetActive(true);
			playerObjects[1].SetActive(false);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			playerObjects[0].SetActive(false);
			playerObjects[1].SetActive(true);
		}
	}
}
