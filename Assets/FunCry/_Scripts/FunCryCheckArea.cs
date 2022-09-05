using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunCryCheckArea : MonoBehaviour
{
    [Header("手部狀態")]
    [SerializeField] Text stateText;
    [Header("範圍視圖")]
    [SerializeField] Image areaImage;
    [Header("範圍視圖背景外框")]
    [SerializeField] Outline outLine;

    [Header("正視圖範圍內")]
    [SerializeField] Sprite forwardViewAreaGreat;
    [Header("正視圖範圍外")]
    [SerializeField] Sprite forwardViewAreaBad;
    [Header("俯視圖範圍內")]
    [SerializeField] Sprite topViewAreaGreat;
    [Header("俯視圖範圍外")]
    [SerializeField] Sprite topViewAreaBad;

    [Header("消失視圖片")]
    [SerializeField] Sprite nullImage;

    [Space(10)][Header("箭頭物件")]
    [SerializeField] GameObject upObject;
    [SerializeField] GameObject downObject;
    [SerializeField] GameObject rightObject;
    [SerializeField] GameObject leftObject;
    [Header("手部物件")]
    [SerializeField] RectTransform handUi;

    [Space(10)]
    [Header("手部實體追蹤")]
    [SerializeField] Transform hand;
    [Header("手部實體位置")]
    [SerializeField] Vector3 handPos;
    [Header("最大範圍座標")]
    [SerializeField] Vector3 areaMax;
    [Header("最小範圍座標")]
    [SerializeField] Vector3 areaMin;

    [SerializeField]
    bool topView;
    [SerializeField]
    bool forwardView;

    private void Awake()
    {

    }

    void Update()
    {
        GetHandPosition();

        Active();

        IsOverAear();

    }

    private void GetHandPosition()
    {
        handPos = hand.position;

        if (topView)
        {
            handUi.anchoredPosition = new Vector2(handPos.x * 500f, (handPos.z + .05f) * 500f);
            areaImage.sprite = topViewAreaBad;
        }

        if (forwardView)
        {
            handUi.anchoredPosition = new Vector2(handPos.x * 500f, (handPos.y - 0.25f) * 500f);
            areaImage.sprite = forwardViewAreaBad;
        }

        if (hand.gameObject.active && !topView && !forwardView)
        {
            handUi.anchoredPosition = new Vector2(handPos.x * 500f, (handPos.z + .05f) * 500f);
            areaImage.sprite = topViewAreaGreat;
        }
    }

    private void IsOverAear()
    {
        if (!hand.gameObject.active)
            return;
        if (hand.localPosition.x > areaMax.x)
        {
            ResetUI();
            stateText.text = "請將手往" + "左";
            outLine.effectColor = Color.red;
            leftObject.SetActive(true);
            topView = true;
            return;
        }
        else
        {
            topView = false;
            leftObject.SetActive(false);
            outLine.effectColor = Color.green;
        }
        if (hand.localPosition.x < areaMin.x)
        {
            ResetUI();
            stateText.text = "請將手往" + "右";
            outLine.effectColor = Color.red;
            rightObject.SetActive(true);
            topView = true;
            return;
        }
        else
        {
            topView = false;
            rightObject.SetActive(false);
            outLine.effectColor = Color.green;
        }

        if (hand.localPosition.z > areaMax.z)
        {
            ResetUI();
            stateText.text = "請將手往" + "後";
            outLine.effectColor = Color.red;
            downObject.SetActive(true);
            topView = true;
            return;
        }
        else
        {
            topView = false;
            downObject.SetActive(false);
            outLine.effectColor = Color.green;
        }

        if (hand.localPosition.z < areaMin.z)
        {
            ResetUI();
            stateText.text = "請將手往" + "前";
            outLine.effectColor = Color.red;
            upObject.SetActive(true);
            topView = true;
            return;
        }
        else
        {
            topView = false;
            upObject.SetActive(false);
            outLine.effectColor = Color.green;
        }

        if (hand.localPosition.y > areaMax.y)
        {
            ResetUI();
            stateText.text = "請將手往" + "下";
            outLine.effectColor = Color.red;
            downObject.SetActive(true);
            forwardView = true;
            return;
        }
        else
        {
            forwardView = false;
            downObject.SetActive(false);
            outLine.effectColor = Color.green;
        }

        if (hand.localPosition.y < areaMin.y)
        {
            ResetUI();
            stateText.text = "請將手往" + "上";
            outLine.effectColor = Color.red;
            upObject.SetActive(true);
            forwardView = true;
            return;
        }
        else
        {
            forwardView = false;
            upObject.SetActive(false);
            outLine.effectColor = Color.green;
        }

        stateText.text = "";

    }

    private void Active()
    {
        if (hand.gameObject.active)
        {
            outLine.effectColor = Color.green;
            ResetUI();
            handUi.gameObject.SetActive(true);
            stateText.text = "";
        }
        else
        {
            forwardView = false;
            topView = false;
            outLine.effectColor = Color.red;
            stateText.text = "請將手往保持在畫面中。";
            areaImage.sprite = nullImage;
            handUi.gameObject.SetActive(false);
            ResetUI();
        }
    }

    private void ResetUI()
    {
        upObject.SetActive(false);
        downObject.SetActive(false);
        rightObject.SetActive(false);
        leftObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.35f);
    }
}
