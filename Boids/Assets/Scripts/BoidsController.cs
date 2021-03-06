﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidsController : MonoBehaviour
{
	// The prefab GameObject to be spawned.
	public GameObject prefabBoid;
	// Total number of boids belonging to this BoidsController.
	public int numBoids;
	// List of all GameObjects managed by this BoidsController.
	[HideInInspector] public List<GameObject> allBoidGameObjects;
	// Max spawn distance from this BoidsController transform position.
	public int spawnRadius;
	// Reference to in-scene goal GameObject for Boid objects in this BoidsController.
	public GameObject goalObject;

	private void Awake()
	{
		allBoidGameObjects = new List<GameObject>();
		SpawnInitialBoids(numBoids, spawnRadius);
	}

	/// Spawn the starting Boids.
	/// numToSpawn - Total instances of prefabBoid to be instantiated
	/// spawnRadius - Maximum distance from this BoidController object's transform position
	private void SpawnInitialBoids(int numToSpawn, int spawnRadius)
	{
		Vector3 spawnOffset;
		for (int i = 0; i < numToSpawn; i++)
		{
			spawnOffset = new Vector3(Random.Range(-spawnRadius, spawnRadius),
								Random.Range(-spawnRadius, spawnRadius),
								Random.Range(-spawnRadius, spawnRadius));

			GameObject boidObject = Instantiate(prefabBoid, transform.position + spawnOffset, Quaternion.identity);
			allBoidGameObjects.Add(boidObject);
			boidObject.transform.parent = transform;
			Boids boid = boidObject.GetComponent<Boids>();
			if (boid)
			{
				boid.parentBoidsController = this;
			}
		}
	}

	/// Return the position the goal GameObject of this BoidsController.
	/// return - position of the goal GameObject
	public Vector3 GetGoalPosition()
	{
		return goalObject.transform.position;
	}
}