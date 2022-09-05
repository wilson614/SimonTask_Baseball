using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] GameObject hand;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hand.active)
        {
            RuleManager.Instance.RuleTurnOn();
        } else
        {
            RuleManager.Instance.RuleTurnOff();
        }
    }
}
