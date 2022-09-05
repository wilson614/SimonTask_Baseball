using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckArea : MonoBehaviour
{
    [SerializeField] Transform checkAreaCamera;
    [SerializeField] Transform hand;
    [SerializeField] Transform ballHand;
    [SerializeField] LineRenderer[] lineMaterials;

    [SerializeField] Vector3 handPosition;
    [SerializeField] Vector3 maxArea;
    [SerializeField] Vector3 minArea;

    private void Start()
    {
        lineMaterials = GetComponentsInChildren<LineRenderer>();
    }

    private void Update()
    {
        handPosition = hand.position;
        ballHand.localPosition = new Vector3(-handPosition.x, handPosition.y, -handPosition.z) * 15f;
        IsOverArea();
    }

    void IsOverArea()
    {
        if (!hand.gameObject.active)
        {
            OverArea();
            ballHand.gameObject.SetActive(false);
            return;
        }
        else
            ballHand.gameObject.SetActive(true);
        if (handPosition.x > maxArea.x)
        {
            OverArea();
            return;
        }
        else
        {
            InsideArea();
        }
        if(handPosition.x < minArea.x)
        {
            OverArea();
            return;
        }
        else
        {
            InsideArea();
        }
        if (handPosition.y > maxArea.y)
        {
            OverArea();
            return;
        }
        else
        {
            InsideArea();
        }
        if (handPosition.y < minArea.y)
        {
            OverArea();
            return;
        }
        else
        {
            InsideArea();
        }
        if (handPosition.z > maxArea.z)
        {
            OverArea();
            return;
        }
        else
        {
            InsideArea();
        }
        if (handPosition.z < minArea.z)
        {
            OverArea();
            return;
        }
        else
        {
            InsideArea();
        }
    }


    void OverArea()
    {
        foreach (LineRenderer material in lineMaterials)
        {
            material.materials[0].color = Color.red;
        }
    }

    void InsideArea()
    {
        foreach (LineRenderer material in lineMaterials)
        {
            material.materials[0].color = Color.green;
        }
    }
}
