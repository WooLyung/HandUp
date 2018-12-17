using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource[] BGM;
    public AudioSource[] SoundEf;
    
    private void Start()
    {
        if(PlayerPrefs.GetInt("First") == 0)
        {
            DataManager.Instance.BGMSound = 0.6f;
            DataManager.Instance.SoundEfSound = 0.6f;
            PlayerPrefs.SetInt("First", 1);
        }
        SoundAdjust();
        
    }

    public void SoundAdjust()
    {
        for (int i = 0; i < BGM.Length; i++)
        {
            BGM[i].volume = DataManager.Instance.BGMSound;
        }
        for (int i = 0; i < SoundEf.Length; i++)
        {
            SoundEf[i].volume = DataManager.Instance.SoundEfSound;
        }
    }

}
