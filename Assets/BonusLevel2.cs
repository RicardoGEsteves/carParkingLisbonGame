using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLevel2 : MonoBehaviour {


    private Nivel2Script lose_gainGameTimeScript2;
    public GameObject levelcontroller, bonusTimeClock;
    public AudioSource audioBonusTime;


    AudioSource asource;
    public void Start()
    {
      //  asource = GetComponent<AudioSource>();


        //  GetComponent<AudioSource>().playOnAwake = false;

    }

    void OnTriggerEnter2D(Collider2D colInfo)
    {
        lose_gainGameTimeScript2 = levelcontroller.GetComponent<Nivel2Script>();
        if (lose_gainGameTimeScript2 != null)
        {
           audioBonusTime = audioBonusTime.GetComponent<AudioSource>();
            audioBonusTime.PlayDelayed(0f);
            lose_gainGameTimeScript2.BonusTime();
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
