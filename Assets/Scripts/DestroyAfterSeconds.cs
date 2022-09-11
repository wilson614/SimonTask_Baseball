using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour
{
    //過幾秒後移除Game Object
    public float second;

    void Start()
    {
        //second秒後呼叫DestroyGameObject函數
        Invoke("DestroyGameObject", second);
    }

    void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
