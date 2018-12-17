using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneMove : MonoBehaviour {

    private static SceneMove instance;
    public static SceneMove Instance
    {
        get
        {
            return instance;
        }
    }
    public GameObject Loading;

    private void Awake()
    {
        instance = this;
    }
    //1초간 로딩을 보여주고 다음 씬으로 이동

    public void Move(string SceneName)
    {
        Time.timeScale = 1;
        StartCoroutine(LoadScene(SceneName));
    }

    IEnumerator LoadScene(string SceneName)
    {
        Loading.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(SceneName);
    }
	
}
