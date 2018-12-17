using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public GameObject pause;
    public GameObject GameResult;
    public Text BestScoreText;
    public Text ResultScore;
    public GameObject newScore;

    private bool gameOver = false;
    public bool GameOver
    {
        get
        {
            return gameOver;
        }
        set
        {
            if (value)
            {
                ShowGameResult();
            }
            gameOver = value;
        }
    }
    [Header("GameData")]
    public float maxEnergy;
    public float Energy;
    public int score;
    public float speed;
    public int scoreUp=1;


    [Header("GameUI")]
    public Text ScoreText;
    public Image EnergyImage;

    [Header("TutorialObj")]
    public GameObject Target1;
    public GameObject Target2;
    public GameObject Tutorial;

    [Header("GameOver")]
    public GameObject Black_Panel;


    public static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }

    public void Start()
    {
        StartCoroutine(ShakeTarget());
    }

    private void Update()
    {
        if (!gameOver && Time.timeScale == 1)
        {
            if(Energy >= 0 && Energy <= maxEnergy)
            {
                EnergyImage.fillAmount = Energy / maxEnergy;
                Energy -= speed;
            }
            else
            {
                gameOver = true;
            }
        }

        ScoreText.text = "" + score;
    }


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void Pause() {
        pause.SetActive(true);
        Time.timeScale = 0;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        pause.SetActive(false);
    }
    public void Replay()
    {
        Time.timeScale = 1;
    }
    public void MainBtn()
    {
        SceneMove.Instance.Move("Main");
    }

    public void gameStart()
    {
        gameOver = false;

    }

    public void GameOverOn()
    {
        Black_Panel.SetActive(true);
    }

    IEnumerator ShakeTarget()
    {
        gameOver = true;
        while (gameOver)
        {
            yield return null;
        }
            Tutorial.SetActive(false);

        StopCoroutine(ShakeTarget());
    }

    public void ShowGameResult()
    {
        Time.timeScale = 0;
        GameResult.SetActive(true);

        if(DataManager.Instance.Best < score)
        {
            newScore.SetActive(true);
            DataManager.Instance.Best = score;
        }

        ResultScore.text = "" + score;
        BestScoreText.text = "" + DataManager.Instance.Best;
        DataManager.Instance.Point += score;

    }
}
