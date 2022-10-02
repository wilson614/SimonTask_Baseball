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
    public Image correctS;
    public Image wrongS;
    public GameObject fingers;
    public List<Image> answerS;
    public Text statisticsText;

    public float cd;
    public Text timer;
    public Image overPitch;

    public Image hrBoard;
    public GameObject KBoard;
    int totalBall = 10;
    int ballCount = 0;
    int correctCount = 0;
    int wrongCount = 0;
    int level = 1;
    float score = 0;

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
            score += cd;
        }
        else
        {
            wrong.enabled = true;
        }
        pitch = true;        
        cd = -1;
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
        timer.enabled = false;

        finger1.enabled = false;
        finger3.enabled = false;        
        //翻轉重置
        fingers.transform.localScale = new Vector3(1, 1, 1);
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
        Instantiate(lostAudio, Vector2.zero, Quaternion.identity);
        yield return new WaitForSeconds(1.5f);
        hrBoard.enabled = false;
        homerun.SetActive(false);
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
        if (level > 3)
        {
            statisticsText.text = "總分：" + Mathf.Ceil(score / 3 * 10) + " 分" + "\n準確率：" + Mathf.Round(((float)correctCount / (totalBall * 3)) * 100) + "%";
        }
        RuleManager.Instance.ruleIndex = level;
        RuleManager.Instance.RuleTurnOn();        
    }

    
    public void createIns()
    {
        answerReset();
        overPitch.enabled = false;
        bubble.SetActive(true);
        int isFlipped = 0;
        if (level == 3)
        {
            //(0: 不翻轉, 1: 翻轉)
            isFlipped = Random.Range(0, 2);            
            if (isFlipped == 1)
            {
                fingers.transform.localScale = new Vector3(1, -1, 1);
            }
        }
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
            if (isFlipped == 1)
            {
                finger3.enabled = true;
            } else
            {
                finger1.enabled = true;
            }            
        }
        if (instruction == 1)
        {
            if (isFlipped == 1)
            {
                finger1.enabled = true;
            }
            else
            {
                finger3.enabled = true;
            }
        }        
        StartCoroutine(playBall());
        StartCoroutine(Countdown());
        canPitch = true;
        isOpen = false;
    }

    IEnumerator Countdown()
    {
        timer.enabled = true;

        while (cd > 0)       //如果時間尚未結束
        {
            yield return new WaitForSeconds(0.1f); //等候一秒再次執行

            cd -= 0.1f;            //將秒數減 1

            if (cd <= -1)      //如果秒數小於 -1 代表有作答 
            {
                cd = -1;     //設定秒數等於 -1
                break;       //跳出迴圈停止計時
            }
            timer.text = cd.ToString("0");
        }

        if (cd < 0 && cd != -1)
        {
            overPitch.enabled = true;           //時間結束時，畫面出現驚嘆號
            choose = 99;
            pitchReset();
            compareAnswer();            
        }
        cd = 10;
        timer.text = cd.ToString("0");
    }
}
