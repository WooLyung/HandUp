using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour {

    public SoundManager sound;

    static DataManager instance;
    #region Singleton
    public static DataManager Instance
    {
        get
        {
            return instance;
        }
    }
    #endregion

    //Best : return BestScore or save in DB
    //Point : return Point(money) or save in DB
    //scoreUp / feverUp / speedUp / tissue : return Upgrade figure or save Upgrade
    //default : 0

    private int best;
    private int point;
    private int scoreUp;
    private int speedUp;
    private int feverUp;
    private int tissue;
    private float bgmSound;
    private float soundEfSound;
    public int Best {
        get
        {
            return best;
        }
        set
        {
            PlayerPrefs.SetInt("Best", value);
            best = value;
        }
    }
    public int Point
    {
        get
        {
            return point;
        }
        set
        {
            PlayerPrefs.SetInt("Point", value);
            point = value;
        }
    }
    public int ScoreUp
    {
        get
        {
            return scoreUp;
        }
        set
        {
            PlayerPrefs.SetInt("Score", value);
            scoreUp = value;
        }
    }
    public int FeverUp
    {
        get
        {
            return feverUp;
        }
        set
        {
            PlayerPrefs.SetInt("Fever", value);
            feverUp = value;
        }
    }
    public int SpeedUp
    {
        get
        {
            return speedUp;
        }
        set
        {
            PlayerPrefs.SetInt("Speed", value);
            speedUp = value;
        }
    }
    public int Tissue
    {
        get
        {
            return tissue;
        }
        set
        {
            PlayerPrefs.SetInt("Tissue", value);
            tissue = value;
        }
    }
    public float BGMSound
    {
        get
        {
            return bgmSound;
        }
        set
        {
            sound.SoundAdjust();
            PlayerPrefs.SetFloat("BGM", value);
            bgmSound = value;
        }
    }
    public float SoundEfSound
    {
        get
        {
            return soundEfSound;
        }
        set
        {
            sound.SoundAdjust();
            PlayerPrefs.SetFloat("SoundEf", value);
            soundEfSound = value;
        }
    }

    private void Awake()
    {
        instance = this;
        best = PlayerPrefs.GetInt("Best");
        Point = PlayerPrefs.GetInt("Point");
        scoreUp = PlayerPrefs.GetInt("Score");
        feverUp = PlayerPrefs.GetInt("Fever");
        speedUp = PlayerPrefs.GetInt("Speed");
        tissue = PlayerPrefs.GetInt("Tissue");
        soundEfSound = PlayerPrefs.GetFloat("SoundEf");
        bgmSound = PlayerPrefs.GetFloat("BGM");

    }
}
