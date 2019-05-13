using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusHazardScript : MonoBehaviour
{
    private Level_Controll lose_gainGameTimeScript;
    public GameObject levelcontroller, bonusTimeClock;
    public AudioSource audioBonusTime;

    
    public void Start()
    {

      
    }
   
    void OnTriggerEnter2D(Collider2D colInfo)
    {
        lose_gainGameTimeScript = levelcontroller.GetComponent<Level_Controll>();
        if (lose_gainGameTimeScript != null)
        {
         audioBonusTime = audioBonusTime.GetComponent<AudioSource>();
            audioBonusTime.PlayDelayed(0f);
            lose_gainGameTimeScript.BonusTime();
            bonusTimeClock.SetActive(false);
            //print("BONUS TIME");

        }
        else
        {
            //print("lose_gainGameTimeScript NO BONUS HAZZARSCRIPT" + "ERROOOOOOOOOOOOO");
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

    

}

