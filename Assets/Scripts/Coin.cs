using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    Manage manage;
    // Start is called before the first frame update
    void Start()
    {
        manage = GameObject.Find("GameManager").GetComponent<Manage>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        manage.score += 1;
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
