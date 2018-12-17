using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmpitTouchL : MonoBehaviour {

    public GameObject target1;
    private Vector2 oriPos;

    private void Start()
    {
        oriPos = target1.transform.position;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Time.timeScale == 1)
        {
            Touch[] myTouches = Input.touches;
            for (int i = 0; i < Input.touchCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(myTouches[i].position),
                    Vector2.zero);

                if (myTouches[i].phase == TouchPhase.Began)
                {

                    if (hit.collider.CompareTag("armpitLeft"))
                    {
                        if (ArmpitCtrl.Instance.armpitHealthLeft <= 100)
                        {
                            ArmpitCtrl.Instance.armpitHealthLeft++;
                            target1.GetComponent<Animator>().SetTrigger("LeftTouch");

                            SoundManager1.Instance.PlaySound("TouchSound");

                            GameManager.instance.Energy += 1;
                            if (GameManager.Instance.Energy > GameManager.Instance.maxEnergy)
                                GameManager.Instance.Energy = GameManager.Instance.maxEnergy;
                            GameManager.instance.score += GameManager.Instance.scoreUp;
                        }

                    }
                    if (hit.collider.CompareTag("armpitRight"))
                    {
                        if (ArmpitCtrl.Instance.armpitHealthRight <= 100)
                        {
                            ArmpitCtrl.Instance.armpitHealthRight++;
                            target1.GetComponent<Animator>().SetTrigger("LeftTouch");

                            SoundManager1.Instance.PlaySound("TouchSound");

                            GameManager.instance.Energy += 1;
                            if (GameManager.Instance.Energy > GameManager.Instance.maxEnergy)
                                GameManager.Instance.Energy = GameManager.Instance.maxEnergy;
                            GameManager.instance.score += GameManager.Instance.scoreUp;
                        }

                    }
                }
            }
        }
    }

    private void OnMouseDown()
    {
        if (ArmpitCtrl.Instance.armpitHealthLeft <= 100 && Time.timeScale == 1)
        {
            target1.transform.position = oriPos;

            int randomX = Random.Range(1, 5);
            int randomY = Random.Range(1, 5);
            target1.transform.position += new Vector3(randomX*0.1f, randomY*0.1f);

            ArmpitCtrl.Instance.armpitHealthLeft++;

            SoundManager1.Instance.PlaySound("TouchSound");

            target1.GetComponent<Animator>().SetTrigger("LeftTouch");

            GameManager.instance.Energy += 1;
            if (GameManager.Instance.Energy > GameManager.Instance.maxEnergy)
                GameManager.Instance.Energy = GameManager.Instance.maxEnergy;
            GameManager.instance.score += GameManager.Instance.scoreUp;
        }

        /*
        if (ArmpitCtrl.Instance.armpitHealthRight < 100)
        {
            ArmpitCtrl.Instance.armpitHealthRight++;
        }
        */
    }
}
