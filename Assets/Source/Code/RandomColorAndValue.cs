using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomColorAndValue : MonoBehaviour
{
    [SerializeField] private List<Color> listColorRandom;
    private bool isUse = false;

    private void Start()
    {
        if (!isUse)
        {
            var xcolorShow = listColorRandom[Random.Range(0, listColorRandom.Count)];
            var xvalue = Random.Range(1, 10);
            ShowData(xcolorShow,xvalue);
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    public void RandomValue()
    {
        var xcolorShow = listColorRandom[Random.Range(0, listColorRandom.Count)];
        var xvalue = Random.Range(1, 10);
        ShowData(xcolorShow,xvalue);
    }

    

    public void ShowDone(Color co,int val)
    {
        ShowData(co,val);
        gameObject.tag = "NotUse";
    }

    public void ShowUse(Color xcolorShow,int xvalue)
    {
        isUse = true;
        ShowData(xcolorShow,xvalue);
    }
    public void ShowBoxCollier()
    {
        GetComponent<BoxCollider2D>().enabled = true;
        isUse = true;
        gameObject.tag = "b1";
    }
    void ShowData(Color color, int val)
    {
        var spriteRen = transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>();
        var text = spriteRen.transform.GetChild(0).GetComponent<TextMeshPro>();
        spriteRen.color = color;
        if (val == -1) text.text = "";
        else text.text = val.ToString();
    }
}
