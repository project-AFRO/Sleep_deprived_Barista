using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private Vector3 playerLocation;
    private Vector3 newlocation;
    [SerializeField] private float yoffset;
    // Start is called before the first frame update
    void Start()
    {
        playerLocation = player.transform.position;
        yoffset = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = player.transform.position;
        newlocation = playerLocation + new Vector3(0,yoffset,0);
        gameObject.transform.localPosition = newlocation;
    }
}
