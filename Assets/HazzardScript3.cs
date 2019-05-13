using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazzardScript3 : MonoBehaviour {

    private Nivel3Script lose_gainGameTimeScript3;
    public GameObject levelcontroller, hazzardTimeClock;
    public AudioSource audioHazzardTime;



    void OnTriggerEnter2D(Collider2D colInfo)
    {
    
        lose_gainGameTimeScript3 = levelcontroller.GetComponent<Nivel3Script>();
        if (lose_gainGameTimeScript3 != null)
        {
           
           audioHazzardTime = audioHazzardTime.GetComponent<AudioSource>();
            audioHazzardTime.PlayDelayed(0f);
           lose_gainGameTimeScript3.LoseTime2();
            hazzardTimeClock.SetActive(false);
            print("Lose TIME");

        }
        else
        {
            print("lose_gainGameTimeScript NO  HAZZARSCRIPT" + "ERROOOOOOOOOOOOO");
        }

        //print("OnCollisionEnter " + gameObject.name);
        //print("Collision Info: I have collided with: " + colInfo.gameObject.name + colInfo.collider.gameObject.name);
        //this.transform.position = originalPos;
    }

    void OnTriggerExit2D(Collider2D colInfo)
    {
        //Debug.Log("OnCollisionEnter " + gameObject.name);
        //Debug.Log("Collision Info" + colInfo.gameObject.name + colInfo.contacts.ToString() + "- " + colInfo.collider.gameObject.name);
        // this.transform.position = originalPos;
    }

    private void Awake()
    {
        hazzardTimeClock.SetActive(true);
    }
}
