using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToBarOrQueue : MonoBehaviour
{
    public GameObject[] seats;
    private GameObject assignedSeat;
    
    public GameObject bar;
    public GameObject queue;
    
    public float speed = 1.0f;
    public float maxDistance = 0.1f;
    private Vector3 barLocation;

    [SerializeField] private GameObject doorSpawner;
    [SerializeField] private doSpawnCostumer spawnCostumer;

    private void assignSeat()
    {
        seats = GameObject.FindGameObjectsWithTag("seat");
        // loop over all seats and assign the costumer to a seat that's not occupied
        foreach (GameObject seat in seats)
        {
            if (seat.GetComponent<seat>().occupied == false)
            {
                assignedSeat = seat;
                break;
            }
        }
        
        // if no seat is available, assign the costumer to the bar
        if (assignedSeat == null)
        {
            assignedSeat = bar;
        }
    }
    
    private void Start()
    {
        spawnCostumer = doorSpawner.GetComponent<doSpawnCostumer>();
        barLocation= bar.transform.position + new Vector3(spawnCostumer.getNumOfCostumers()/5,0,-2*spawnCostumer.getNumOfCostumers()%5 -2);

        assignSeat();
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, assignedSeat.transform.position, speed * Time.deltaTime);
    }
}
