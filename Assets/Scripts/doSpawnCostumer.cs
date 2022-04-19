using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doSpawnCostumer : MonoBehaviour
{
    [SerializeField] private GameObject custumerPrefab;
    [SerializeField] private GameObject cupPrefab;
    [SerializeField] private GameObject Barista;
                     private barista baristaScript;
    [SerializeField] private float spawnRate;
    [SerializeField] private float spawnDelay;
    [SerializeField] private float roundDelay;
    [SerializeField] private static int numOfCostumers;
    [SerializeField] private int roundCount;
    [SerializeField] private AudioClip doorChime;
    [SerializeField] private AudioSource SpawnSoundSource;


    // Start is called before the first frame update
   
    void Start()
    {
        SpawnSoundSource.clip = doorChime;
        numOfCostumers = 0;
        spawnDelay = 1.5f;
        spawnRate = 2.5f;
        roundDelay = 15.0f;
        roundCount = 0;
        baristaScript = Barista.GetComponent<barista>();

        StartCoroutine(SpawnCustumerWithDelay());
    }

    private void SpawnCustumer()
    {
        // new position 
        Vector3 spawnPosition = new Vector3(transform.position.x, 1, transform.position.z);
        GameObject custumer = Instantiate(custumerPrefab, transform.position, transform.rotation);
        numOfCostumers++;
        roundCount++;
        Debug.Log("spawned");
        
    }

    [System.Obsolete]
    IEnumerator SpawnCustumerWithDelay()
    {
        while (true)
        {
            if (numOfCostumers <= baristaScript.getCupsDrank() * roundCount * 3)
             {
                SpawnSoundSource.Play();
                for (int i = 0; i <= Random.RandomRange(baristaScript.getCupsDrank()*roundCount, (baristaScript.getCupsDrank() + 1) *roundCount* spawnRate); i++)
                {
                    yield return new WaitForSeconds(spawnDelay);
                    SpawnCustumer();
                }
                Debug.Log("spawn 'round' loop done");
                yield return new WaitForSeconds(roundDelay);
                Debug.Log("wait done");
            }
            else { yield return new WaitForSeconds(roundDelay); }
        }
       
    }

    public int getNumOfCostumers()
    {
        return numOfCostumers;
    }
    public int getRound()
    {
        return roundCount;
    }
    public void setNumOfCostumers(int x)
    {
        numOfCostumers = x;
    }
}
