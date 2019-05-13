using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Nivel3Script : MonoBehaviour {


    enum GameState { INIT, RUN, END, END_LOST };
    static int WinCondition = 0;
    public GameObject player, menu, button_Start, button_Restart, button_NextLevel, TimmerLevel, InfoImage_final, star1, star2, star3, HazzardScript, bonusScript, hazzardScript2, bonusScript2, NameLevel, ImageEndGame, trophy, nivel3Script;
    Vector3 end_position;
    Quaternion end_rotation, startrotation;
    GameState estadoJogo;
    float timmerLevel1 = 120f;
    float loseTime = 10f;
    float bonustime = 30f;
    private Nivel3PlayerController playerController;
    
    public AudioSource myaudio, audioRestart, audioNextLevel, audioInfoImage, audioCity;

    public void InfoImage_Onclick()
    {
        audioInfoImage = audioInfoImage.GetComponent<AudioSource>();
        audioInfoImage.PlayDelayed(0f);
        InfoImage_final.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        trophy.SetActive(false);

    }

    //when the player clicks the last Image close the game
    public void ImageEndGame_Onclick(){
    	audioInfoImage = audioInfoImage.GetComponent<AudioSource>();
        audioInfoImage.PlayDelayed(0f);
        ImageEndGame.SetActive(false);
        Application.Quit();

    }

    public void LoseTime()
    {
        timmerLevel1 -= loseTime;
        Debug.Log("timmer" + timmerLevel1);
    }

    public void LoseTime2()
    {
        timmerLevel1 -= 60f;

    }

    public void BonusTime()
    {
        timmerLevel1 += bonustime;
        //print(timmerLevel1 + "TEMPOOO ATUALLLLL");

    }

    public void StartGame()
    {

        myaudio = myaudio.GetComponent<AudioSource>();
        myaudio.PlayDelayed(0f);
        audioCity = audioCity.GetComponent<AudioSource>();
        audioCity.PlayDelayed(0.5f);
        estadoJogo = GameState.RUN;
        menu.SetActive(false);



    }

    //when the player ends the game he can click the button Start Again to go back to the first level
    public void StartAgain(){
    	myaudio = myaudio.GetComponent<AudioSource>();
        myaudio.PlayDelayed(0f);
    	 SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        audioNextLevel = audioNextLevel.GetComponent<AudioSource>();
        audioNextLevel.PlayDelayed(0f);
        audioCity.Pause();
        ImageEndGame.SetActive(true);

       
    }

    public void ReStart()
    {
    	  playerController.StopCarOn();
        audioRestart = audioRestart.GetComponent<AudioSource>();
        audioRestart.PlayDelayed(0f);
        estadoJogo = GameState.RUN;
        menu.SetActive(false);
        HazzardScript.SetActive(true);
        hazzardScript2.SetActive(true);
        bonusScript.SetActive(true);
        bonusScript2.SetActive(true);
        
        timmerLevel1 = 120f;
        //player.transform.parent.position = new Vector3(3.7f, 0f, -0.1015625f);
        player.transform.position = new Vector3(-4.1f, -4.4f, 0.0f);
        player.transform.rotation = new Quaternion(0.0f, 0.0f, 0.7f, 0.7f);
       // print("POSIÇÃO" + player.transform.position);
    }

    // Use this for initialization
    void Start()
    {
    	playerController = nivel3Script.GetComponent<Nivel3PlayerController>();
    	NameLevel.GetComponent<TextMeshProUGUI>().text = "Level: 3";
        menu.SetActive(true);
        //originalPos = new Vector3(-5.78F, -4f, -0.1015625f);
        estadoJogo = GameState.INIT;

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(timmerLevel1);
        print(estadoJogo);
       //print("Position level 3" + player.transform.position + end_position);
       //print("Rotation level 3" + player.transform.rotation + end_rotation);
        switch (estadoJogo)
        {
            case GameState.INIT:
                //  estadoJogo = GameState.RUN;
               
                   end_position = new Vector3(-3.0f, 2.6f, 0f);
                   end_rotation = transform.rotation;
                   end_rotation.Set(0f, 0f, 0.7f, 0.7f);

                StartGame();
                break;

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
                    if (Vector3.Distance(player.transform.position, end_position) < 0.3f)
                    {
                        if (Quaternion.Angle(player.transform.rotation, end_rotation) < 20f)
                        {
                            WinCondition = 2;
                        }
                    }

                }

                else
                {


                   // print("YOU WIN nivel3 ****************************************************");
                    WinCondition = 0;
                    InfoImage_final.SetActive(true);

                    estadoJogo = GameState.END;
                }
                break;
            case GameState.END:

                //print("Veio aqui  ****************************************************");
                
            	playerController.StopCarOff();
                menu.SetActive(true);
                
                star1.SetActive(true);


                if (timmerLevel1 >= 60)
                {

                    star2.SetActive(true);
                }

                if (timmerLevel1 >= 90)
                {
                    star2.SetActive(true);
                    star3.SetActive(true);
                    trophy.SetActive(true);
                }


                TimmerLevel.SetActive(false);
                NameLevel.SetActive(false);
                button_NextLevel.SetActive(true);
                button_Restart.SetActive(true);
                button_Start.SetActive(true);
                break;

            case GameState.END_LOST:
            	playerController.StopCarOff();
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
