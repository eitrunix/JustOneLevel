using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	public Transform target;
	private Vector3 velocity = Vector3.zero;
	public float smoothSpeed = 0.125f;
	public Vector3 offset;


	[SerializeField] CameraBounds2D bounds;
	Vector2 maxXPositions, maxYPositions;

	void Awake()
	{
		bounds.Initialize(GetComponent<Camera>());
		maxXPositions = bounds.maxXlimit;
		maxYPositions = bounds.maxYlimit;
	}
	void FixedUpdate()
	{
		Vector3 currentPosition = transform.position;
		Vector3 targetPosition = new Vector3(Mathf.Clamp(target.position.x, maxXPositions.x, maxXPositions.y), Mathf.Clamp(target.position.y, maxYPositions.x, maxYPositions.y), currentPosition.z);
		transform.position = Vector3.Lerp(currentPosition, targetPosition, Time.deltaTime * smoothSpeed);
	}

}
