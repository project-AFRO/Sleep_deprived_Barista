using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doSpawnCostumer : MonoBehaviour
{
    [SerializeField] private GameObject custumerPrefab;
    [SerializeField] private GameObject Barista;
                     private barista baristaScript;
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnDelay;
    [SerializeField]private float roundDelay;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        spawnDelay = 2.0f;
        spawnRate = 1.0f;
        roundDelay = 15.0f;

        baristaScript = Barista.GetComponent<barista>();

        StartCoroutine(SpawnCustumerWithDelay());
    }

    private void SpawnCustumer()
    {
        // new position 
        Vector3 spawnPosition = new Vector3(transform.position.x, 1, transform.position.z);
        GameObject custumer = Instantiate(custumerPrefab, transform.position, transform.rotation) as GameObject;
        Debug.Log("spawned");
    }

    [System.Obsolete]
    IEnumerator SpawnCustumerWithDelay()
    {
        while (true)
        {
            for (int i = 0; i <= Random.RandomRange(1, baristaScript.getCupsDrank() + 1); i++)
            {
                yield return new WaitForSeconds(spawnDelay);
                SpawnCustumer();
            }
            Debug.Log("spawn 'round' loop done");
            yield return new WaitForSeconds(roundDelay);
            Debug.Log("wait done");
        }
       
    }
}
