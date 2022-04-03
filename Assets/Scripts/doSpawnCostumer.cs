using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doSpawnCostumer : MonoBehaviour
{
    public GameObject custumerPrefab;
    public float spawnRate = 1.0f;
    public float spawnDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCustumerWithDelay());
    }

    private void SpawnCustumer()
    {
        // new position 
        Vector3 spawnPosition = new Vector3(transform.position.x, 1, transform.position.z);
        GameObject custumer = Instantiate(custumerPrefab, transform.position, transform.rotation) as GameObject;
    }

    IEnumerator SpawnCustumerWithDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            SpawnCustumer();
        }
    }
}
