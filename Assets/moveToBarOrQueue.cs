using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveToBarOrQueue : MonoBehaviour
{
    public GameObject bar;
    public GameObject queue;
    public float speed = 1.0f;
    public float maxDistance = 0.1f;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, bar.transform.position, speed * Time.deltaTime);
    }
}
