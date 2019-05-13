using System.Collections; using System.Collections.Generic; using UnityEngine;
using UnityEngine.UI; using TMPro;
using UnityEngine.SceneManagement;

public class Nivel2Script : MonoBehaviour {


    enum GameState { INIT, RUN, END, END_LOST };
    static int WinCondition = 0;
    public GameObject player, menu, button_Start, button_Restart, button_NextLevel, TimmerLevel, InfoImage_final, star1, star2, star3, HazzardScript, bonusScript, hazzardScript2, HazzardScript3, NameLevel, Trophy, Nivel2Script;
    Vector3 end_position;
    Quaternion end_rotation, startrotation;
    GameState estadoJogo;
    float timmerLevel1 = 300f;
    float loseTime = 10f;
    float bonustime = 30f;
    public AudioSource myaudio, audioRestart, audioNextLevel, audioInfoImage, audioCity;
    private Nivel2PlayerController playerController;

    public void InfoImage_Onclick()
    {
        audioInfoImage = audioInfoImage.GetComponent<AudioSource>();
        audioInfoImage.PlayDelayed(0f);
        InfoImage_final.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
        Trophy.SetActive(false);

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
        print(timmerLevel1 + "TEMPOOO ATUALLLLL");

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

    public void NextLevel()
    {
        audioNextLevel = audioNextLevel.GetComponent<AudioSource>();
        audioNextLevel.PlayDelayed(0f);
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        menu.SetActive(false);
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
        HazzardScript3.SetActive(true);
        timmerLevel1 = 180f;
        //player.transform.parent.position = new Vector3(3.7f, 0f, -0.1015625f);
        player.transform.position = new Vector3(-1.4f, -11.7f, 0.0f);
        player.transform.rotation = new Quaternion(0.0f, 0.0f, 0.7f, 0.7f);
       // print("POSIÇÃO" + player.transform.position);
    }

    // Use this for initialization
    void Start()
    {
        playerController = Nivel2Script.GetComponent<Nivel2PlayerController>();
        NameLevel.GetComponent<TextMeshProUGUI>().text = "Level: 2";
        menu.SetActive(true);
        //originalPos = new Vector3(-5.78F, -4f, -0.1015625f);
        estadoJogo = GameState.INIT;

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(timmerLevel1);
        print(estadoJogo);
        //print("Position level 2" + player.transform.position + end_position);
       // print("Rotation level 2" + player.transform.rotation + end_rotation);
        switch (estadoJogo)
        {
            //set values end position, end rotation and run the function StartGame()
            case GameState.INIT:
                //  estadoJogo = GameState.RUN;
            
                
                    timmerLevel1 = 180f;
                    end_position = new Vector3(-6.9f, 9.9f, 0.0f);
                    end_rotation = transform.rotation;
                    end_rotation.Set(0.0f, 0.0f, 0.4f, 0.9f);

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
                    if (Vector3.Distance(player.transform.position, end_position) < 0.2f)
                    {
                        if (Quaternion.Angle(player.transform.rotation, end_rotation) < 30f)
                        {
                            WinCondition = 2;
                        }
                    }

                }

                else
                {


                    print("YOU WIN nivel2 ****************************************************");
                    WinCondition = 0;
                    InfoImage_final.SetActive(true);

                    estadoJogo = GameState.END;
                }
                break;
            case GameState.END:

                print("Veio aqui  ****************************************************");
                menu.SetActive(true);
                
                star1.SetActive(true);

                playerController.StopCarOff();
                if (timmerLevel1 >= 60)
                {

                    star2.SetActive(true);
                }

                if (timmerLevel1 >= 120)
                {
                    star2.SetActive(true);
                    star3.SetActive(true);
                    Trophy.SetActive(true);
                }


                TimmerLevel.SetActive(false);
                NameLevel.SetActive(false);
                button_NextLevel.SetActive(true);
                button_Restart.SetActive(true);
                button_Start.SetActive(false);
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