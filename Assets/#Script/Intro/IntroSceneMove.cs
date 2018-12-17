using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroSceneMove : MonoBehaviour {

	public void InsertGame()
    {
        SceneMove.Instance.Move("Main");
    }
}
