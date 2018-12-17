using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmpitCtrl : MonoBehaviour {

    public Sprite[] armImgLeft;
    public Sprite[] armImgRight;
    public Sprite[] faceImg;

    public GameObject armLeft;
    public GameObject armRight;
    public GameObject face;

    public float armpitDownPower=1f;
    public float armpitHealthLeft=100;
    public float armpitHealthRight = 100;


    public static ArmpitCtrl instance;

    private bool isfeverTime = false;
    private bool flashCheck = true;
    private int check = 0;


    SpriteRenderer armL_SR;
    SpriteRenderer armR_SR;
    SpriteRenderer face_SR;

    public static ArmpitCtrl Instance
    {
        get
        {
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(armpitDownPowerUp());
        armL_SR = armLeft.GetComponent<SpriteRenderer>();
        armR_SR = armRight.GetComponent<SpriteRenderer>();
        face_SR = face.GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update () {
        if(Time.timeScale == 1)
        {
            if (GameManager.Instance.Energy >= GameManager.Instance.maxEnergy - 1 && !isfeverTime)
            {
                SoundManager1.Instance.StopSound("BGSound");
                SoundManager1.Instance.PlaySound("FeverSound");
                GameManager.Instance.Black_Panel.SetActive(flashCheck);
                flashCheck = !flashCheck;
                StartCoroutine(FeverTimeStart());
                GameManager.Instance.scoreUp = 3;
                isfeverTime = true;
            }
            if (isfeverTime)
            {
                GameManager.Instance.Black_Panel.SetActive(flashCheck);
                GameManager.Instance.Energy += 3;
                if (GameManager.Instance.Energy > GameManager.Instance.maxEnergy)
                    GameManager.Instance.Energy = GameManager.Instance.maxEnergy;
                armpitHealthLeft = 100;
                armpitHealthRight = 100;
                face_SR.sprite = faceImg[5];
            }
            else
            {
                if (!GameManager.Instance.GameOver)
                {
                    if (armpitHealthLeft > 75)
                    {
                        armL_SR.sprite = armImgLeft[0];
                    }
                    else if (armpitHealthLeft > 50)
                    {
                        armL_SR.sprite = armImgLeft[1];
                    }
                    else if (armpitHealthLeft > 25)
                    {
                        armL_SR.sprite = armImgLeft[2];
                    }
                    else if (armpitHealthLeft > 0)
                    {
                        armL_SR.sprite = armImgLeft[3];
                    }

                    if (armpitHealthRight > 75)
                    {
                        armR_SR.sprite = armImgRight[0];
                    }
                    else if (armpitHealthRight > 50)
                    {
                        armR_SR.sprite = armImgRight[1];
                    }
                    else if (armpitHealthRight > 25)
                    {
                        armR_SR.sprite = armImgRight[2];
                    }
                    else if (armpitHealthRight > 0)
                    {
                        armR_SR.sprite = armImgRight[3];
                    }


                    if (armpitHealthLeft < 25 || armpitHealthRight < 25)
                    {
                        face_SR.sprite = faceImg[3];
                    }
                    else if (armpitHealthLeft < 50 || armpitHealthRight < 50)
                    {
                        face_SR.sprite = faceImg[2];
                    }
                    else if (armpitHealthLeft < 75 || armpitHealthRight < 75)
                    {
                        face_SR.sprite = faceImg[1];
                    }
                    else
                    {
                        face_SR.sprite = faceImg[0];
                    }

                    if (armpitHealthLeft <= 0 || armpitHealthRight <= 0 || GameManager.Instance.Energy <= 1)
                    {
                        GameManager.Instance.GameOver = true;
                    }
                }
                armpitHealthLeft -= armpitDownPower;
                armpitHealthRight -= armpitDownPower;
            }
        }
       
    }

    IEnumerator FeverTimeStart()
    {
        while (check < 50)
        {
            GameManager.Instance.Black_Panel.GetComponent<Image>().color = Random.ColorHSV();
            GameManager.Instance.Black_Panel.GetComponent<Image>().color += new Color(0, 0, 0, -0.5f);
            yield return new WaitForSeconds(0.1f);
            flashCheck = !flashCheck;
            check++;
        }
        SoundManager1.Instance.StopSound("FeverSound");
        SoundManager1.Instance.PlaySound("BGSound");
        check = 0;
        GameManager.Instance.Black_Panel.SetActive(false);
        GameManager.Instance.Energy -= 100;
        GameManager.Instance.scoreUp = 1;
        isfeverTime = false;
        StopCoroutine(FeverTimeStart());
    }

    IEnumerator armpitDownPowerUp()
    {
        while (!GameManager.Instance.GameOver)
        {
            yield return new WaitForSeconds(5f);
            
            armpitDownPower += 1f;
            GameManager.Instance.speed+=1.5f;
        }
        StopCoroutine(armpitDownPowerUp());
    }
}
