﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool isOpen;
    public bool canPitch = false;
    public Image ball;
    float speed = 1000f;
    bool pitch = false;
    int instruction;
    int choose;
    public Image correct;
    public Image wrong;
    public Image finger1;
    public Image finger3;
    public GameObject bubble;
    public Image point;
    public GameObject strikeObj;
    public GameObject homerun;
    public GameObject run;
    int strike = 0;
    int outs = 0;
    public Image strike1;
    public Image strike2;
    public Image strike3;
    public Image out1;
    public Image out2;
    public Image out3;

    private void Start()
    {
        createIns();
    }
    private void Update()
    {
        if (pitch == true)
        {            
            if (ball.transform.localPosition.x != 483)
            {
                ball.transform.localPosition = Vector3.MoveTowards(ball.transform.localPosition, new Vector3(483, 184, 0), speed * Time.deltaTime);

            }
            else
            {
                pitch = false;
                isOpen = false;
                Destroy(ball.gameObject);
                createBall();
                if (choose == instruction)
                {
                    StartCoroutine(strikeIns());
                } else
                {
                    StartCoroutine(homerunPage());                    
                }                
            }

        }
    }
    public void openHands()
    {
        Debug.Log("Open hand");
        isOpen = true;
    }

    public void closeHands()
    {
        //Debug.Log("Close hand");
        //isOpen = false;
    }

    private void compareAnswer()
    {
        if (choose == instruction)
        {
            correct.enabled = true;
        }
        else
        {
            wrong.enabled = true;
        }
        pitch = true;
    }
    public void pitchLeft()
    {
        if (canPitch && isOpen)
        {
            choose = 0;
            pitchReset();
            compareAnswer();
            Debug.Log("pitchLeft");
                        
        }
        
    }

    public void pitchRight()
    {
        if (canPitch && isOpen)
        {
            choose = 1;
            pitchReset();
            compareAnswer();
            Debug.Log("pitchRight");
        }
    }

    private void answerReset()
    {
        correct.enabled = false;
        wrong.enabled = false;
    }

    private void pitchReset()
    {
        finger1.enabled = false;
        finger3.enabled = false;
        bubble.SetActive(false);
        canPitch = false;
    }

    public void createBall()
    {
        ball = Instantiate(ball, Vector3.zero, Quaternion.identity);
        ball.transform.SetParent(GameObject.FindGameObjectWithTag("baseball").transform, false);
        ball.enabled = true;
    }

    IEnumerator playBall()
    {
        point.enabled = true;
        yield return new WaitForSeconds(1);
        point.enabled = false;
    }

    IEnumerator strikeIns()
    {
        strikeObj.SetActive(true);
        strike++;
        if (strike == 1)
        {
            strike1.color = new Color32(253, 236, 4, 255);
            yield return new WaitForSeconds(2);
        }
        if (strike == 2)
        {
            strike2.color = new Color32(253, 236, 4, 255);
            yield return new WaitForSeconds(2);
        }
        if (strike == 3)
        {
            strike3.color = new Color32(253, 236, 4, 255);
            yield return new WaitForSeconds(2);
            strike1.color = new Color32(255, 255, 255, 255);
            strike2.color = new Color32(255, 255, 255, 255);
            strike3.color = new Color32(255, 255, 255, 255);
            strike = 0;
            outs++;
        }

        strikeObj.SetActive(false);

        if (outs == 1)
        {
            out1.color = new Color32(244, 10, 1, 255);
        }
        if (outs == 2)
        {
            out2.color = new Color32(244, 10, 1, 255);
        }
        if (outs == 3)
        {
            out3.color = new Color32(244, 10, 1, 255);
            yield return new WaitForSeconds(1);
            out1.color = new Color32(255, 255, 255, 255);
            out2.color = new Color32(255, 255, 255, 255);
            out3.color = new Color32(255, 255, 255, 255);
            outs = 0;
        }
        
        createIns();

    }

    IEnumerator homerunPage()
    {
        homerun.SetActive(true);
        yield return new WaitForSeconds(1);
        run.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        homerun.SetActive(false);
        run.SetActive(false);
        createIns();
    }

    public void createIns()
    {
        answerReset();
        bubble.SetActive(true);
        //(0: 投左, 1: 投右)
        instruction = Random.Range(0, 2);
        if (instruction == 0)
        {
            finger1.enabled = true;
        }
        if (instruction == 1)
        {
            finger3.enabled = true;
        }        
        StartCoroutine(playBall());
        canPitch = true;
    }


    
}
