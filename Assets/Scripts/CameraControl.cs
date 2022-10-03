using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public GameObject player;

    private Vector3 velocity = Vector3.zero;
    private Vector3 offset;            

    // Start is called before the first frame update
    void Start()
    {
        
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + offset, ref velocity, 0.6f);
    }
}
