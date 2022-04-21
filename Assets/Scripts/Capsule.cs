using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capsule : MonoBehaviour
{
    // Start is called before the first frame update
    private AudioSource source;
    public AudioClip switchSound;
    void Start()
    {
        source=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Player")){
            source.PlayOneShot(switchSound, 1F);
        }
    }
}
