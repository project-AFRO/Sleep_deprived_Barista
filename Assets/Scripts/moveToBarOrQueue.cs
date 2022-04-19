using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToBarOrQueue : MonoBehaviour
{
    //private Vector3 newPosition;
    //public GameObject[] seats;
    //private GameObject assignedSeat;

    public GameObject bar;
    private int costumerNumber;
    //public GameObject queue;
    private List<Vector3> costumerRest;
    [SerializeField] private float speed = 1000.0f;
    //public float maxDistance = 0.1f;
    [SerializeField]private UnityEngine.Vector3 costumerqueueLocation;
    [SerializeField] private UnityEngine.Vector3 barlocation;

    [SerializeField] private GameObject doorSpawner;
    [SerializeField] private doSpawnCostumer spawnCostumer;

    //private void assignSeat()
    //{
    //    seats = GameObject.FindGameObjectsWithTag("seat");
    //    // loop over all seats and assign the costumer to a seat that's not occupied
    //    foreach (GameObject seat in seats)
    //    {
    //        if (seat.GetComponent<seat>().occupied == false)
    //        {
    //            assignedSeat = seat;
    //            break;
    //        }
    //    }

    //    // if no seat is available, assign the costumer to the bar
    //    if (assignedSeat == null)
    //    {
    //        assignedSeat = bar;
    //    }
    //}

    private void Start()
    {
        //spawnCostumer = doorSpawner.GetComponent<doSpawnCostumer>();
        barlocation = new UnityEngine.Vector3(-67.77434f,0, 4.01f);
        costumerNumber = spawnCostumer.getNumOfCostumers();
        costumerqueueLocation = barlocation + new UnityEngine.Vector3(costumerNumber/5,0,-2*spawnCostumer.getNumOfCostumers()%5 -2);
        
        //assignSeat();
        //newPosition = new Vector3(Random.Range(-90.0f, -47.0f), 0.0f, Random.Range(-29.0f, 1.45f));
    }
    
    // Update is called once per frame
    void Update()
    {
        transform.position = UnityEngine.Vector3.MoveTowards(transform.position, costumerqueueLocation, speed * Time.deltaTime);

        // make new transform with random x and z values
        
    }
}
