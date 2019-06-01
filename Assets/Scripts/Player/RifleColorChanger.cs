using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleColorChanger : MonoBehaviour
{
	public List<Material> m = new List<Material>();
	public SkinnedMeshRenderer smr;
	bool isSingleMode = true;

	private void Awake()
	{
		smr = GetComponent<SkinnedMeshRenderer>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//smr.material = smr.material.ToString().Contains(m[0].ToString()) ? m[1] : m[0]; //앞메테리얼은 instance라고 붙어서 이렇게 했는데 이거도 안됨...
			smr.material = isSingleMode == true ? m[1] : m[0];
			isSingleMode = isSingleMode == true ? false : true;
		}
	}
}
