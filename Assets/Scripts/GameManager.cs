using System.Collections;
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
    public GameObject runPage;
    public Image correctS;
    public Image wrongS;
    public GameObject fingers;
    public List<Image> answerS;

    public Image hrBoard;
    public GameObject KBoard;
    int totalBall = 3;
    int ballCount = 0;
    int correctCount = 0;
    int wrongCount = 0;
    int level = 1;

    public GameObject catchAudio;
    public GameObject strikeAudio;
    public GameObject hitAudio;
    public GameObject lostAudio;
    public GameObject playBallAudio;

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
                ballCount++;
                createBall();
                if (choose == instruction)
                {
                    StartCoroutine(strikeIns());
                }
                else
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
        Instantiate(playBallAudio, Vector2.zero, Quaternion.identity);
        point.enabled = true;
        yield return new WaitForSeconds(1);
        point.enabled = false;
    }

    IEnumerator strikeIns()
    {
        Instantiate(catchAudio, Vector2.zero, Quaternion.identity);    
        strikeObj.SetActive(true);
        Instantiate(strikeAudio, Vector2.zero, Quaternion.identity);
        correctCount++;
        Image I = Instantiate(correctS, new Vector3(-288 + 64 * (ballCount - 1), -38, 0), Quaternion.identity) as Image;
        I.transform.SetParent(GameObject.FindGameObjectWithTag("countBoard").transform, false);
        answerS.Add(I);
        yield return new WaitForSeconds(2);
        strikeObj.SetActive(false);
        if (ballCount < totalBall)
        {
            createIns();
        } else
        {
            answerReset();
        }
        if (ballCount == totalBall)
        {
            nextLevel();
        }        
    }

    IEnumerator homerunPage()
    {
        Instantiate(hitAudio, Vector2.zero, Quaternion.identity);
        Image I = Instantiate(wrongS, new Vector3(-288 + 65 * (ballCount - 1), -38, 0), Quaternion.identity) as Image;
        I.transform.SetParent(GameObject.FindGameObjectWithTag("countBoard").transform, false);
        answerS.Add(I);
        wrongCount++;
        homerun.SetActive(true);
        yield return new WaitForSeconds(1);
        hrBoard.enabled = true;
        yield return new WaitForSeconds(1.5f);
        hrBoard.enabled = false;
        runPage.SetActive(true);
        Instantiate(lostAudio, Vector2.zero, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        homerun.SetActive(false);
        runPage.SetActive(false);
        if (ballCount < totalBall)
        {
            createIns();
        }
        else
        {
            answerReset();
        }
        if (ballCount == totalBall)
        {
            nextLevel();
        }
    }

    private void nextLevel()
    {
        ballCount = 0;
        level++;        
        foreach (Image item in answerS)
        {
            Destroy(item);
        }
        RuleManager.Instance.ruleIndex = level;
        RuleManager.Instance.RuleTurnOn();        
    }

    
    public void createIns()
    {
        answerReset();
        bubble.SetActive(true);
        if (level >= 2)
        {
            //(0: 左45度, 1: 右45度)
            int angle = Random.Range(0, 2);
            if (angle == 0)
            {
                fingers.transform.localEulerAngles = new Vector3(0, 0, -45);
            }
            if (angle == 1)
            {
                fingers.transform.localEulerAngles = new Vector3(0, 0, 45);
            }
        }
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
