using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RuleManager : MonoBehaviour
{
    #region Singleton
    private static RuleManager _instance;

    public static RuleManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<RuleManager>();
                if (_instance == null)
                {
                    Debug.LogError("RuleManager is null");
                }
            }

            return _instance;
        }
    }
    #endregion

    [Header("顯示介面")]
    [SerializeField] Image ruleImage;
    [Header("規則圖")]
    [SerializeField] Sprite[] rules;

    [SerializeField] int ruleIndex;
    [SerializeField] Leap.Unity.PinchDetector pinchDetector;
    [SerializeField] FunCryOptionManager FunCryOptionManager;

    CanvasGroup canvasGroup;

    private void Start()
    {
        DontDestroyOnLoad(this);
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void startTurnOn()
    {
        RuleTurnOn();
        pinchDetector.OnActivate.RemoveAllListeners();
        pinchDetector.OnDeactivate.RemoveAllListeners();

        pinchDetector.OnActivate.AddListener(FunCryOptionManager.OKHoldEvent);
        FunCryOptionManager.buttons[0].onClick.AddListener(closeButton);
        pinchDetector.OnDeactivate.AddListener(FunCryOptionManager.OKReleaseEvent);
        
    }

    void closeButton()
    {
        FunCryOptionManager.enabled = false;
        foreach (Button button in FunCryOptionManager.buttons)
        {
            button.gameObject.SetActive(false);
        }
        this.enabled = false;
    } 

    public void RuleTurnOn()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        ruleImage.sprite = rules[ruleIndex - 1];
        
    }

    public void RuleTurnOff()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
