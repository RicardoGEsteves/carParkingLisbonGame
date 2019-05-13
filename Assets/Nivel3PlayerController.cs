using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nivel3PlayerController : MonoBehaviour {

	public GameObject Nivel3Script;
    public Vector2 move;
    public Vector2 velocity;
    public float maxSpeed = 5.0f;
    public float acceleration = 5.0f;
    public float brake = 5.0f;
    public float turnSpeed = 5.0f;
    private float speed = 0.0f;
    private Nivel3Script anotherscript3;
    float i = 0;
    Vector3 originalPos;
    public AudioSource audioCollision;
    public bool StopCar;

     public void StopCarOff(){
        StopCar = false;
    }

    public void StopCarOn(){
        StopCar = true;
    }


    void OnCollisionEnter2D(Collision2D colInfo)
    {
       if (StopCar==false){
       }
       else{
            if (Time.time - i > 2)
            {
                audioCollision = audioCollision.GetComponent<AudioSource>();
                audioCollision.PlayDelayed(0f);
                anotherscript3.LoseTime();
                i = Time.time;
            }
        }




        //  while (i < 10)
        //  {
        //     i += Time.deltaTime;
        //    }

        //   if (i >= 10)
        //  {
        //     anotherscript.LoseTime();
        // }

        print("OnCollisionEnter " + gameObject.name);
        print("Collision Info: I have collided with: " + colInfo.gameObject.name + colInfo.collider.gameObject.name);
        //this.transform.position = originalPos;
    }

    void OnCollisionExit2D(Collision2D colInfo)

    {
        //Debug.Log("OnCollisionEnter " + gameObject.name);
        //Debug.Log("Collision Info" + colInfo.gameObject.name + colInfo.contacts.ToString() + "- " + colInfo.collider.gameObject.name);
        //this.transform.position = originalPos;
        //this.transform.position.Set(5.78f, -7.1f, 0.1015625f);
    }

    void Start()
    {
    	StopCar = true;
        i = Time.time;

        
        anotherscript3 = Nivel3Script.GetComponent<Nivel3Script>();
        // originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        //alternatively, just: originalPos = gameObject.transform.position;


    }
    void Update()
    {
    	 if (Input.GetKeyDown("escape")) {
             Application.Quit();
        }

    	if (Input.GetKeyDown("space")){
            brake = 50f;
        }
        else {
        	brake = 5f;
        }

        float turn = Input.GetAxis("Horizontal");


        float forwards = Input.GetAxis("Vertical");

        if(StopCar == false){


        }
        else{
        if (forwards < 0)
        {
            speed = speed + acceleration * Time.deltaTime;
        }

        else if (forwards > 0)
        {
            speed = speed - acceleration * Time.deltaTime;
        }
        else
        {
            if (speed > 0)
            {
                speed = speed - brake * Time.deltaTime;
            }
            else
            {
                speed = speed + brake * Time.deltaTime;
            }
        }



        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

        Vector2 velocity = Vector2.up * speed;
        transform.Translate(velocity * Time.deltaTime, Space.Self);
        


        if (turn < 0)
        {

            //gameObject.transform.position = gameObject.transform.position + Vector3.up * speed * Time.deltaTime;
            //   Debug.Log(gameObject.transform.position + Vector3.up * speed * Time.deltaTime);
           gameObject.transform.Rotate(0, 0, 2, Space.Self);
           

        }

        else if (turn > 0)
        {

            // gameObject.transform.position = gameObject.transform.position + Vector3.down * speed * Time.deltaTime;

            gameObject.transform.Rotate(0, 0, -2, Space.Self);
          
        }

		}
    }//end Update
}
