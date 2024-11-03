using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject V1; // The victim prefab to spawn
    public float xPos; // X position for spawning
    public float zPos; // Z position for spawning
    public int maxVictims = 10; // Maximum number of victims to spawn
    private int currentVictimCount; // Current count of spawned victims

    private void Start()
    {
        StartCoroutine(SpawnVictims()); // Start the coroutine
    }

    private IEnumerator SpawnVictims()
    {
        while (currentVictimCount < maxVictims) // Check against maximum limit
        {
            xPos = Random.Range(-3.3f, -2.1f); // Random X position
            zPos = Random.Range(-2.6f, 5.4f); // Random Z position
            Instantiate(V1, new Vector3(xPos, -2.4f, zPos), Quaternion.identity); // Spawn the victim
            currentVictimCount++; // Increment the current victim count
            yield return new WaitForSeconds(7); // Wait for 7 seconds before spawning the next
        }
    }
}
