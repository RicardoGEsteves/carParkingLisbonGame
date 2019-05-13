using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Level_Controll : MonoBehaviour {

    enum GameState { INIT, RUN, END, END_LOST};
    static int WinCondition = 0 ;
    public GameObject player, menu, button_Start, button_Restart, button_NextLevel, TimmerLevel, InfoImage_final, star1, star2, star3, HazzardScript, bonusScript, hazzardScript2, NameLevel, trophy, PlayerScript, ImgHowtoPlay, buttonHowtoPlay;
    Vector3 end_position;
    Quaternion end_rotation, startrotation;
    GameState estadoJogo;
    float timmerLevel1 = 300f;
    float loseTime = 10f;
    float bonustime = 30f;
    Vector3 originalPos;
    public AudioSource myaudio, audioRestart, audioNextLevel, audioInfoImage, audioCity;
    private Player_Controller playerController;


   //Activate image how to play
    public void Imghowtoplay(){
        ImgHowtoPlay.SetActive(true);

    }

 //Disable the final image with stars, trophy and plays the music menu
    public void InfoImage_Onclick()
    {
        audioInfoImage = audioInfoImage.GetComponent<AudioSource>();
        audioInfoImage.PlayDelayed(0f);
        
        InfoImage_final.SetActive(false);
        ImgHowtoPlay.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        trophy.SetActive(false);

    }

    //when player collides
    public void LoseTime()
    {
        timmerLevel1 -= loseTime;
       // Debug.Log("timmer" + timmerLevel1);
    }

    //time lost when player triggered hazzard clock red
    public void LoseTime2()
    {
      
     
        timmerLevel1 -= 60f;
      
    }


    //time gain when player triggered hazzard clock green
    public void BonusTime()
    {
       
  
        timmerLevel1 += bonustime;
        print(timmerLevel1 + "TEMPOOO ATUALLLLL");

    }
   

   //when player clicks the button Start Game
    public void StartGame()
    {

        myaudio = myaudio.GetComponent<AudioSource>();
        myaudio.PlayDelayed(0f);
        audioCity = audioCity.GetComponent<AudioSource>();
        audioCity.PlayDelayed(0.5f);
        buttonHowtoPlay.SetActive(false);
        estadoJogo = GameState.RUN;

        //Button button_Start = GetComponent<Button>();
        //AudioSource audio = GetComponent<AudioSource>();
        //button_Start.onClick.AddListener(delegate () { audio.Play(); });
        menu.SetActive(false);
       

    }

    //when player clicks the button Next Level, load the second level
    public void NextLevel()
    {
        audioNextLevel = audioNextLevel.GetComponent<AudioSource>();
        audioNextLevel.PlayDelayed(0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
     
        menu.SetActive(false);
    }

    //when player clicks the button Try Again and restart the position, timmer, hazzards

    public void ReStart()
    {
        //lose_gainGameTimeScript = .GetComponent<BonusHazardScript>();
        //lose_gainGameTimeScript.ResetBonus();
        playerController.StopCarOn();
        audioRestart = audioRestart.GetComponent<AudioSource>();
        audioRestart.PlayDelayed(0f);
        audioCity.Play();
        estadoJogo = GameState.RUN;
        menu.SetActive(false);
        HazzardScript.SetActive(true);
        hazzardScript2.SetActive(true);
        bonusScript.SetActive(true);
        timmerLevel1 = 300f;

        //player.transform.parent.position = new Vector3(3.7f, 0f, -0.1015625f);
        player.transform.position = new Vector3(-3.5f, -6.55f, -0.1015625f);
        player.transform.rotation = new Quaternion(0.0f, 0.0f, 0.7f, 0.7f);
        print("POSIÇÃO" + player.transform.position);
        
    }

    // Use this for initialization
    //Turn on menu and initialize player controller
    void Start () {

        playerController = PlayerScript.GetComponent<Player_Controller>();
        NameLevel.GetComponent<TextMeshProUGUI>().text = "Level: 1";
        menu.SetActive(true);
        //originalPos = new Vector3(-5.78F, -4f, -0.1015625f);
        estadoJogo = GameState.INIT;

        
     //   print("ORGINAL" + originalPos);

    }
	
	// Update is called once per frame
	void Update () {
       // Debug.Log(timmerLevel1);
        print(estadoJogo);
        //print("Position level 2" + player.transform.position + end_position);
        //print("Rotation level 2" + player.transform.rotation + end_rotation);
        switch (estadoJogo)
        {
        	//set values end position and end rotation
            case GameState.INIT:
                //  estadoJogo = GameState.RUN;
                
                    timmerLevel1 = 180f;
                    end_position = new Vector3(-8.44f, 7.24f, 0.101f);
                    end_rotation = transform.rotation;
                    end_rotation.Set(0.0f, 0.0f, 0.4f, 0.9f);
               
             
                break;
                //update the timmer and detect if the win condition is met
                case GameState.RUN:
                //print("end-rotation" + player.transform.rotation);
                //print("position" + player.transform.position);

               
                timmerLevel1 -= Time.deltaTime;
                int min = Mathf.FloorToInt(timmerLevel1 / 60);
                int sec = Mathf.FloorToInt(timmerLevel1 % 60);
                TimmerLevel.GetComponent<TextMeshProUGUI>().text = (min.ToString("00") + ":" + sec.ToString("00"));
                TimmerLevel.SetActive(true);
                NameLevel.SetActive(true);


                if (timmerLevel1 <= 0)
                {
                    estadoJogo = GameState.END_LOST;
                }



                if (WinCondition != 2)
                {
                    if (Vector3.Distance(player.transform.position, end_position) < 0.5f)
                    {
                        if (Quaternion.Angle(player.transform.rotation, end_rotation) < 0.5f)
                        {
                            WinCondition = 2;
                        }
                    }

                }

                else
                {
                     
                
                   print("YOU WIN mivel 1");
                    WinCondition = 0;
                    InfoImage_final.SetActive(true);

                    estadoJogo = GameState.END;
                }
                break;

                //if the player wins the level determines the number of stars given
            case GameState.END:
                
                playerController.StopCarOff();
                menu.SetActive(true);
                star1.SetActive(true);
             

                if (timmerLevel1 >= 180)
                {
                    
                    star2.SetActive(true);
                }

                if (timmerLevel1 >= 240) {
                  
                    star2.SetActive(true);
                    star3.SetActive(true);
                    trophy.SetActive(true);
                }

                audioCity.Pause();
                TimmerLevel.SetActive(false);
                NameLevel.SetActive(false);
                button_NextLevel.SetActive(true);
                button_Restart.SetActive(true);
                button_Start.SetActive(false);
                break;

                //when the players loses the game 
            case GameState.END_LOST:
                playerController.StopCarOff();
                audioCity.Pause();
                TimmerLevel.SetActive(false);
                NameLevel.SetActive(false);
                menu.SetActive(true);
                button_NextLevel.SetActive(false);
                button_Restart.SetActive(true);
                button_Start.SetActive(false);
                break;
        }

    }
}
