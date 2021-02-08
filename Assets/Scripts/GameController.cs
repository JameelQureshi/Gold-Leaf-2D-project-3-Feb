using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] Boxes;
    public static GameController instance;

    public GameObject Header, Timer, tutorialPanel, VideoPanel, WinPanel1, WinPanel2, EndTimePanel;

    public Timer Timeref;

    public Text winName, LosseName;

    public Text ScoreShow, ScoreshowatEnd;

    public SceneLoader loaderref;

    public AudioSource Alltime;
    public AudioSource GamePlay;
    public AudioSource win;

    public GameObject[] scenes;

    public GameObject[] winCanvas;

    public GameObject[] Winboxes;

    public AudioSource buttonPop;

    bool iswon = false;
    int index = 0;
    public void StartTheGameBUddy()
    {

        Alltime.Pause();
        GamePlay.Play();
        VideoPanel.SetActive(false);
        tutorialPanel.SetActive(false);
        Header.SetActive(true);
        Timer.SetActive(true);
        Timeref.StartTimer();
    }


    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        Boxes = GameObject.FindGameObjectsWithTag("boxes");

         index = UnityEngine.Random.Range(0,4);

        for(int i =0; i<4; i ++)
        {
            scenes[i].SetActive(false);
        }

        scenes[index].SetActive(true);



    }

    public void CheckStatusofBoxes()
    {
        for(int i=0; i<Boxes.Length; i++)
        {
            if(Boxes[i].GetComponent<CollisionChecker>().IsInTheBOX)
            {
                return;
            }

            
        }
        print("Its Done");

        iswon = true;
        winCanvas[index].SetActive(true);
        
    }

    void  countScore ()
    {
        string score = "";
        if(Timeref.currentTime >= 10f)
        {
            ScoreShow.text = "Score : "+"100";
            score = "100";
        }
        else if (Timeref.currentTime >= 7f)
        {
            ScoreShow.text = "Score : " + "70";
            score = "70";
        }
        else if (Timeref.currentTime >= 4f)
        {
            ScoreShow.text = "Score : " + "50";
            score = "50";
        }
        else if (Timeref.currentTime >= 1f)
        {
            ScoreShow.text = "Score : " + "30";
            score = "30";
        }


        PlayerPrefs.SetString("User_Score",score);
        PlayerPrefs.SetString("User_Time",((int)Timeref.currentTime).ToString());

        PlayerPrefs.SetString("StartAppend", "true");

    }

    IEnumerator waitforlastScreen()
    {
        yield return new WaitForSeconds(8f);
        Header.SetActive(false);
        Timer.SetActive(false);
        VideoPanel.SetActive(true);
        WinPanel1.SetActive(false);
        WinPanel2.SetActive(true);

    }
    public void GameEnded()
    {
        GamePlay.Pause();
        Alltime.Play();
        Header.SetActive(false);
        Timer.SetActive(false);
        VideoPanel.SetActive(true);
        EndTimePanel.SetActive(true);
        Time.timeScale = 1f;

        PlayerPrefs.SetString("User_Score", "0");
        PlayerPrefs.SetString("User_Time", "00");

        PlayerPrefs.SetString("StartAppend", "true");
    }


    public void ExitButton()
    {
        loaderref.LoadScene(0);
    }

    public void CheckWin()
    {

        if(iswon == true)
        {
            //Winboxes[index].SetActive(false);
            buttonPop.Play();
            Winboxes[index].GetComponent<Animator>().Play("boxfader");
            GamePlay.Pause();
            win.Play();

            WinPanel1.SetActive(true);
            Timeref.enabled = false;

            countScore();

            winName.text = "" + PlayerPrefs.GetString("Player_UserName");
            ScoreshowatEnd.text = "" + (int)Timeref.currentTime + " Sec";

            
            StartCoroutine(waitforlastScreen());
        }

        
    }
}
