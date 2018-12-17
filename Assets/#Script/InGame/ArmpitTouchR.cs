using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmpitTouchR : MonoBehaviour{

    // Update is called once per frame

    public GameObject target2;
    private Vector2 oriPos;

    private void Start()
    {
        oriPos = target2.transform.position;

    }

    void Update()
    {
        if (Input.touchCount > 0 && Time.timeScale == 1)
        {

            Touch[] myTouches = Input.touches;
            for (int i = 0; i < Input.touchCount; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(myTouches[i].position),
                    Vector2.zero);

                if (hit.collider.CompareTag("armpitLeft"))
                {
                    if (ArmpitCtrl.Instance.armpitHealthLeft <= 100)
                    {
                        ArmpitCtrl.Instance.armpitHealthLeft++;
                        target2.GetComponent<Animator>().SetTrigger("RightTouch");

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
                        SoundManager1.Instance.PlaySound("TouchSound");

                        target2.GetComponent<Animator>().SetTrigger("RightTouch");

                        ArmpitCtrl.Instance.armpitHealthRight++;
                        GameManager.instance.Energy += 1;
                        if (GameManager.Instance.Energy > GameManager.Instance.maxEnergy)
                            GameManager.Instance.Energy = GameManager.Instance.maxEnergy;
                        GameManager.instance.score += GameManager.Instance.scoreUp;
                    }

                }
            }
        }
    }

    private void OnMouseDown()
    {
        /*
        if (ArmpitCtrl.Instance.armpitHealthLeft < 100)
        {
            ArmpitCtrl.Instance.armpitHealthLeft++;
        }

        */
        
        if (ArmpitCtrl.Instance.armpitHealthRight <= 100 && Time.timeScale == 1)
        {
            ArmpitCtrl.Instance.armpitHealthRight++;

            target2.transform.position = oriPos;

            int randomX = Random.Range(1, 5);
            int randomY = Random.Range(1, 5);
            target2.transform.position += new Vector3(randomX * 0.1f, randomY * 0.1f);


            target2.GetComponent<Animator>().SetTrigger("RightTouch");
            SoundManager1.Instance.PlaySound("TouchSound");
                GameManager.instance.Energy += 1;
            if (GameManager.Instance.Energy > GameManager.Instance.maxEnergy)
                GameManager.Instance.Energy = GameManager.Instance.maxEnergy;
                GameManager.instance.score += GameManager.Instance.scoreUp;
        }
        
    }
}
