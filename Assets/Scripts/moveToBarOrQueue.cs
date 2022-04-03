using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToBarOrQueue : MonoBehaviour
{
    public GameObject bar;
    public GameObject queue;
    public float speed = 1.0f;
    public float maxDistance = 0.1f;
    private Vector3 barLocation;

    private void Start()
    {
       barLocation= bar.transform.position + new Vector3(0,0,-2);
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, barLocation , speed * Time.deltaTime);
    }
}
