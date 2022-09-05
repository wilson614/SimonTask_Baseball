using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunCryOptionManager : MonoBehaviour
{
    [Header("選項的父母")]
    public Transform optionsParent;
    [Header("選項們")]
    public RectTransform[] options;
    [Header("選項的讀條們")]
    public Slider[] sliders;
    [Header("選項的按鈕們")]
    public Button[] buttons;

    [Space(10)]
    [Header("目標標記是否動態搖擺")]
    public bool tagetMark_Swing;
    [Header("目標標記物件")]
    [SerializeField] RectTransform tagetMark;

    [Space(10)]
    [Header("選項讀條延遲")]
    public float sliderSpeed = 5f;
    [Header("標記平移速度")]
    public float markMoveSpeed;
    [Header("標記旋轉速度")]
    public float markRotateSpeed;

    [Space(10)]
    [Header("選項狀態")]
    public bool ok;
    public bool isCheck;

    int index;

    private void OnValidate()
    {
        buttons = optionsParent.GetComponentsInChildren<Button>();
        sliders= optionsParent.GetComponentsInChildren<Slider>();
        foreach (Slider sliders_All in sliders)
        {
            sliders_All.maxValue = sliderSpeed;
        }
        options = new RectTransform[optionsParent.childCount];
        for (int i = 0; i < optionsParent.childCount; i++)
        {
            options[i] = optionsParent.GetChild(i).gameObject.GetComponent<RectTransform>();
        }
    }

    private void Start()
    {
        tagetMark.anchoredPosition = new Vector3(708, buttons[index].GetComponent<RectTransform>().anchoredPosition.y - 119f, 0);
    }

    private void Update()
    {
        TagetMarkSwing();

        if (isCheck)
        {
            buttons[index].onClick.Invoke();
            ok = false;
            isCheck = false;

            transform.GetComponent<CanvasGroup>().alpha = 0;
            transform.GetComponent<CanvasGroup>().interactable = false;
            transform.GetComponent<CanvasGroup>().blocksRaycasts = false;
            return;
        }
        if (ok)
        {
            sliders[index].value += 2f * Time.deltaTime;
            if (sliders[index].value == sliders[index].maxValue)
            {
                isCheck = true;
            }
        }
        else
        {
            if (index != 0)
                sliders[index - 1].value = 0;
            else
                sliders[sliders.Length - 1].value = 0;
        }
    }

    private void TagetMarkSwing()
    {
        if (tagetMark_Swing)
        {
            tagetMark.transform.GetChild(0).transform.localPosition = new Vector3(Mathf.PingPong(Time.time * markMoveSpeed, 10) - 10, 0, 0);
            tagetMark.transform.GetChild(0).transform.localEulerAngles = new Vector3(Mathf.PingPong(Time.time * markRotateSpeed, 360) - 0, 0, 45);
        }
    }

    public void OKHoldEvent()
    {
        ok = true;
    }

    public void OKReleaseEvent()
    {
        ok = false;

        foreach (Slider item in sliders)
        {
            item.value = 0;
        }
    }

    public void RockHoldEvent()
    {
        if (index == buttons.Length - 1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        tagetMark.anchoredPosition = new Vector3(708, buttons[index].GetComponent<RectTransform>().anchoredPosition.y-119f, 0);
        ok = false;
    }
}
