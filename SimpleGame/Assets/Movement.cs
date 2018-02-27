using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour 
{
	public float movementTime = 2;
	public float lifeTime = 20;
	public float matureAfter = 10;
	public float reproductionPause = 2;

	float deathTime;
	float startMovementTime;
	float matureTime;
	float nextReproduction;

	bool isMoving = false;
	bool isMature = false;
	bool readyToReproduce = false;

	Vector3 direction;
	
	void Start () 
	{
		deathTime = Time.time + lifeTime;
		matureTime = Time.time + matureAfter;
	}
	
	void Update () 
	{
		if (Time.time > deathTime)
			Destroy(gameObject);

		if (!isMature)
			if (Time.time > matureTime)
			{
				isMature = true;
				readyToReproduce = true;
				GetComponent<MeshRenderer>().material.color = Color.blue;
			}
		
		//if (readyToReproduce ) 

		Move(); 
	}

	void Move()
	{
		if (Time.time - startMovementTime > movementTime)
					isMoving = false;

		if (isMoving)
		{
			var nextPos = transform.position + (direction * Time.deltaTime);
			if (CheckFieldBorders(nextPos))
				isMoving = false;
			else
				transform.Translate(direction * Time.deltaTime);
		}
		else
		{
			var dir = Random.insideUnitCircle;
			dir.Normalize();
			direction = new Vector3(dir.x, 0, dir.y);
			direction.y = 0;
			startMovementTime = Time.time;
			isMoving = true;
			movementTime = 2 + Random.RandomRange(-0.5f, 0.5f);
		}
	}

	bool CheckFieldBorders(Vector3 nextPos)
	{
		if (Mathf.Abs(nextPos.x) > 20f || Mathf.Abs(nextPos.z) > 20f)
			return true;
		return false;
	}
}
