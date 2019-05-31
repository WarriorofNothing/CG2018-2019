using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sync : MonoBehaviour{
    public AudioClip heartbeat;

    public AudioSource source;

    void Start(){
        
    }

    void Update(){
        
    }

    void Beat(){
        source.PlayOneShot(heartbeat);
    }
}


