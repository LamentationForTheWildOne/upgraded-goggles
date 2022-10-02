using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manage : MonoBehaviour
{
    public float score;
    public GameObject End;
    TextMesh txt;
    
    // Start is called before the first frame update
    void Start()
    {
        txt = End.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (score == 5) {
            txt.text = "Congrats, guess ya win";
        }
    }
}
