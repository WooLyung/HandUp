using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour {

    public AudioSource TouchSoundEf;
    public Animator Character;
    public GameObject[] BG;
    #region enum
    enum STATE
    {
        SCORE, FEVER, SPEED, TISSUE
    }

    enum OPTION
    {
        BGM, SEF
    }
    #endregion

    #region classes
    public class state
    {
        public Text stateText { get; set; }
        public Text upgradeText { get; set; }
        public Image gage { get; set; }

        public state(string name)
        {
            Transform panel = GameObject.Find("Canvas").transform.Find("Panel_States");

            stateText = panel.Find(name).Find("Text").GetComponent<Text>();
            upgradeText = panel.Find(name).Find("Upgrade").Find("Text").GetComponent<Text>();
            gage = panel.Find(name).Find("GageBackground").Find("Gage").GetComponent<Image>();
        }
    }
    #endregion

    #region variable
    GameObject panel_States;
    GameObject panel_Options;

    state[] states = new state[4];
    Slider[] slider = new Slider[2];

    public int[] upgradePrice_score = new int[8];
    public int[] upgradePrice_fever = new int[8];
    public int[] upgradePrice_speed = new int[8];
    public int[] upgradePrice_tissue = new int[8];

    Text moneyText;
    #endregion

    #region awake & start
    private void Awake()
    {
        // 상태와 옵션창, 포인트 표시창의 텍스트 컴포넌트를 받아옴
        panel_States = GameObject.Find("Canvas").transform.Find("Panel_States").gameObject;
        panel_Options = GameObject.Find("Canvas").transform.Find("Panel_Options").gameObject;
        moneyText = GameObject.Find("moneyText").GetComponent<Text>();

        // 상태창과 옵션창의 게이지들을 받아옴
        states[(int)STATE.SCORE] = new state("State_Score");
        states[(int)STATE.FEVER] = new state("State_Fever");
        states[(int)STATE.SPEED] = new state("State_Speed");
        states[(int)STATE.TISSUE] = new state("State_Tissue");
        slider[(int)OPTION.BGM] = panel_Options.transform.Find("Background_Sound").Find("Slider").GetComponent<Slider>();
        slider[(int)OPTION.SEF] = panel_Options.transform.Find("Sound_Effect").Find("Slider").GetComponent<Slider>();
    }

    private void Start()
    {
        StateUpdate();

        slider[(int)OPTION.BGM].value = DataManager.Instance.BGMSound;
        slider[(int)OPTION.SEF].value = DataManager.Instance.SoundEfSound;
    }
    #endregion

    private void Update()
    {
        Close();

        if (Input.GetKeyDown(KeyCode.A))
        {
            DataManager.Instance.Point = 10000;
            DataManager.Instance.ScoreUp = 0;
            DataManager.Instance.SpeedUp = 0;
            DataManager.Instance.FeverUp = 0;
            DataManager.Instance.Tissue = 0;

            StateUpdate();
        }
    }

    #region States
    public void Button_States()
    {
        if (CanTouch())
            panel_States.SetActive(true);
    }

    public void ExitState()
    {
        panel_States.SetActive(false);
    }
        #region Upgrade
    public void Upgrade_score()
    {
        TouchSoundEf.Play();
        if (DataManager.Instance.ScoreUp >= 8)
            return;

        if (DataManager.Instance.Point >= upgradePrice_score[DataManager.Instance.ScoreUp])
        {
            DataManager.Instance.Point -= upgradePrice_score[DataManager.Instance.ScoreUp];
            DataManager.Instance.ScoreUp++;
            StateUpdate();
        }
    }

    public void Upgrade_fever()
    {
        TouchSoundEf.Play();
        if (DataManager.Instance.FeverUp >= 8)
            return;

        if (DataManager.Instance.Point >= upgradePrice_fever[DataManager.Instance.FeverUp])
        {
            DataManager.Instance.Point -= upgradePrice_fever[DataManager.Instance.FeverUp];
            DataManager.Instance.FeverUp++;
            StateUpdate();
        }
    }

    public void Upgrade_speed()
    {
        TouchSoundEf.Play();
        if (DataManager.Instance.SpeedUp >= 8)
            return;

        if (DataManager.Instance.Point >= upgradePrice_speed[DataManager.Instance.SpeedUp])
        {
            DataManager.Instance.Point -= upgradePrice_speed[DataManager.Instance.SpeedUp];
            DataManager.Instance.SpeedUp++;
            StateUpdate();
        }
    }

    public void Upgrade_tissue()
    {
        TouchSoundEf.Play();
        if (DataManager.Instance.Tissue >= 8)
            return;

        if (DataManager.Instance.Point >= upgradePrice_tissue[DataManager.Instance.Tissue])
        {
            DataManager.Instance.Point -= upgradePrice_tissue[DataManager.Instance.Tissue];
            DataManager.Instance.Tissue++;
            StateUpdate();
        }
    }
    #endregion
    #endregion

    #region Options
    public void Button_Options()
    {
        TouchSoundEf.Play();
        if (CanTouch())
            panel_Options.SetActive(true);
    }

    public void ChangeValue_BGM()
    {
        DataManager.Instance.BGMSound = slider[(int)OPTION.BGM].value;
    }

    public void ChangeValue_SEF()
    {
        DataManager.Instance.SoundEfSound = slider[(int)OPTION.SEF].value;
    }

    public void Options_Off()
    {
        TouchSoundEf.Play();
        panel_Options.SetActive(false);
    }
    #endregion

    #region Start
    public void Button_Start()
    {
        TouchSoundEf.Play();
        if (CanTouch())
        {
            StartCoroutine(Coroutine_Start());
        }
            
    }
    #endregion

    #region Methods
    private void Close()
    {
        // 뒤로가기를 눌렀을 때 옵션이나 상태창이 켜져 있다면 그 창을 끔
        // 옵션, 상태창 둘 다 켜져있지 않다면 게임을 종료

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panel_Options.activeSelf)
                panel_Options.SetActive(false);
            else if (panel_States.activeSelf)
                panel_States.SetActive(false);
            else
                Application.Quit();
        }
    }

    private bool CanTouch()
    {
        // 옵션, 상태창 둘 다 켜져있지 않다면 true, 그렇지 않다면 false를 반환

        return !panel_Options.activeSelf && !panel_States.activeSelf;
    }

    private void StateUpdate()
    {
        moneyText.text = DataManager.Instance.Point.ToString();

        states[(int)STATE.FEVER].gage.fillAmount = DataManager.Instance.FeverUp / 8f;
        states[(int)STATE.SPEED].gage.fillAmount = DataManager.Instance.SpeedUp / 8f;
        states[(int)STATE.SCORE].gage.fillAmount = DataManager.Instance.ScoreUp / 8f;
        states[(int)STATE.TISSUE].gage.fillAmount = DataManager.Instance.Tissue / 8f;

        if (DataManager.Instance.FeverUp >= 8)
            states[(int)STATE.FEVER].upgradeText.text = "최대치";
        else
            states[(int)STATE.FEVER].upgradeText.text = upgradePrice_fever[DataManager.Instance.FeverUp].ToString();

        if (DataManager.Instance.SpeedUp >= 8)
            states[(int)STATE.SPEED].upgradeText.text = "최대치";
        else
            states[(int)STATE.SPEED].upgradeText.text = upgradePrice_speed[DataManager.Instance.SpeedUp].ToString();

        if (DataManager.Instance.ScoreUp >= 8)
            states[(int)STATE.SCORE].upgradeText.text = "최대치";
        else
            states[(int)STATE.SCORE].upgradeText.text = upgradePrice_score[DataManager.Instance.ScoreUp].ToString();

        if (DataManager.Instance.Tissue >= 8)
            states[(int)STATE.TISSUE].upgradeText.text = "최대치";
        else
            states[(int)STATE.TISSUE].upgradeText.text = upgradePrice_tissue[DataManager.Instance.Tissue].ToString();
    }
    #endregion

    #region Coroutines
    IEnumerator Coroutine_Start()
    {
        Character.SetBool("IsStop", true);

        BGMove bg1 = BG[0].GetComponent<BGMove>();
        BGMove bg2 = BG[1].GetComponent<BGMove>();

        for (int i = 0; i < 10;i++)
        {
            yield return new WaitForSeconds(0.1f);
            bg1.currentSpeed -= bg1.speed * 0.1f;
            bg2.currentSpeed -= bg2.speed * 0.1f;
        }
        yield return new WaitForSeconds(1.3f);
        SceneMove.Instance.Move("InGame");
    }
    #endregion
}
