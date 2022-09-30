using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    public int ruleIndex;
    [SerializeField] Leap.Unity.PinchDetector pinchDetector;
    [SerializeField] FunCryOptionManager FunCryOptionManager;

    public CanvasGroup canvasGroup;
    public CanvasGroup statistics;

    private void Start()
    {        
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
        if(ruleIndex > 3)
        {
            statistics.alpha = 1;
            statistics.interactable = true;
            statistics.blocksRaycasts = true;
            CheckArea.Instance.transform.GetComponentInParent<Canvas>().sortingOrder = -1;
            return;
        }
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
