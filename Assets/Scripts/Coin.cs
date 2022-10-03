using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    Manage manage;
    AudioSource aud;

    public AudioClip picksfx;
    // Start is called before the first frame update
    void Start()
    {
        manage = GameObject.Find("GameManager").GetComponent<Manage>();
        aud = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.name == "Player") {
            manage.score += 1;
            
            AudioSource.PlayClipAtPoint(picksfx, this.gameObject.transform.position);

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
