using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private List<RandomColorAndValue> listPoint = new List<RandomColorAndValue>();
    [SerializeField] private List<RandomColorAndValue> listPointUse = new List<RandomColorAndValue>();
    [SerializeField] private ObjectMoveByDrag prefabs;
    [SerializeField] private Transform postInit;

    [SerializeField] private List<Color> listColorRandom;

    private int curentLevel = 1;
    private int curStep = 0;
    private List<int> ListValue = new List<int>();
    private List<Color> listColor = new List<Color>();
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        curentLevel = 1;
    }

    private void OnEnable()
    {
        listPoint = FindObjectsOfType<RandomColorAndValue>().ToList();
        Invoke(nameof(RandomDataByLevel),0.25f);
    }


    public void CheckStep()
    {
        listPointUse[curStep].ShowDone(listColor[curStep],ListValue[curStep]);
        curStep++;
        int valueCheck = curentLevel == 1 ? 4 : curentLevel == 2 ? 5 : 6;
        if (curStep >= valueCheck)
        {
           Invoke(nameof(NextLevel),1.5f);
        }
        else
        {
            InitItemToPlay();
        }
    }

    void NextLevel()
    {
        curentLevel++;
        if (curentLevel >= 4)
            curentLevel = 1;
        listPointUse.Clear();
        listColor.Clear();
        ListValue.Clear();
        foreach (var poValue in listPoint)
        {
            poValue.RandomValue();
        }
        curStep = 0;
        Invoke(nameof(RandomDataByLevel),0.5f);
    }
    void RandomDataByLevel()
    {
        for (int i = 0; i < curentLevel+3; i++)
        {
            var va = Random.Range(0, 9);
            var co = listColorRandom[Random.Range(0, listColorRandom.Count)];
            
            ListValue.Add(va);
            listColor.Add(co);
            var po = listPoint[Random.Range(0, listPoint.Count)];
            while (listPointUse.Exists( l => l == po))
            {
                po = listPoint[Random.Range(0, listPoint.Count)];
            }
            listPointUse.Add(po);
        }
        
        int stt = 0;
        foreach (var b in listPointUse)
        {
            b.ShowUse(listColor[stt],-1);
            stt++;
        }
        InitItemToPlay();
        
    }
    
    

    void InitItemToPlay()
    {
        var x = Instantiate(prefabs, postInit.position, Quaternion.identity);
        x.ShowData(listColor[curStep],ListValue[curStep]);
        listPointUse[curStep].ShowBoxCollier();
    }
    
    
    // drag obj
    public ObjectMoveByDrag selectedObject;
    Vector3 offset;
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);
            if (targetObject && targetObject.GetComponent<ObjectMoveByDrag>() != null)
            {
                selectedObject = targetObject.GetComponent<ObjectMoveByDrag>();
                offset = selectedObject.transform.position - mousePosition;
            }
        }
        if (selectedObject)
        {
            selectedObject.transform.position = mousePosition + offset;
        }
        if (Input.GetMouseButtonUp(0) && selectedObject)
        {
            if (selectedObject)
            {
                selectedObject.CheckOnMouseUp();
            }
            selectedObject = null;
        }
    }
}
