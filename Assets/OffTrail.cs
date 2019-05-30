using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffTrail : MonoBehaviour
{
	public TrailRenderer trailRenderer;

	private void Awake()
	{
		trailRenderer = GetComponentInChildren<TrailRenderer>();
	}

	private void OnCollisionEnter(Collision col)
	{
		trailRenderer.enabled = false;
	}
}
