using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour 
{
	public int population = 20;
	public Vector2 fieldSize = new Vector2(20, 20);

	public GameObject individual;

	void Start () 
	{
		for (int i = 0; i < population; i++)
		{
			var pos = new Vector3(Random.Range(-fieldSize.x, fieldSize.x), 0, Random.Range(-fieldSize.y, fieldSize.y));

			GameObject.Instantiate(individual, pos, Quaternion.identity);
		}
	}
}
