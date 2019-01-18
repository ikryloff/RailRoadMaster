using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : Singleton<CompositionManager>
{
    public List<string> compositions = new List<string>();
    public Dictionary<int, RollingStock[]> compositionsList = new Dictionary<int, RollingStock[]>();
    private GameObject[] carsObj;
    private RollingStock[] cars;
    public string composition;
    private string compositionNumber;


   


    public List<string> Compositions
    {
        get
        {
            return compositions;
        }

        set
        {
            compositions = value;
        }
    }

    public string CompositionNumber
    {
        get
        {
            return compositionNumber;
        }

        set
        {
            compositionNumber = value;
        }
    }

    public Dictionary<int, RollingStock[]> CompositionsList
    {
        get
        {
            return compositionsList;
        }

        set
        {
            compositionsList = value;
        }
    }

   
    private void Awake()
    {
        carsObj = GameObject.FindGameObjectsWithTag("RollingStock");
        int i = 0;
        cars = new RollingStock[carsObj.Length];
        foreach (GameObject item in carsObj)
        {
            cars[i] = item.GetComponent<RollingStock>();
            i++;
        }        
    }
    private void Start()
    {
        Invoke("UpdateCompositionsInformation", 0.5f);
    }


    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
            UpdateCompositionsInformation();
    }

    public void UpdateCompositionsInformation()
    {
        UpdateCompositionList();
        MakeCompositionDictionary();
        
        for (int i = 0; i < Compositions.Count; i++)
        {
            foreach (RollingStock item in CompositionsList[i])
            {
                print("In comp #" + i + " car " + item.Number);
            }
        }
        
        SetCompositionNumbersToRS();
    }

    private void SetCompositionNumbersToRS()
    {
        for (int i = 0; i < Compositions.Count; i++)
        {
            foreach (RollingStock item in CompositionsList[i])
            {
                item.CompositionNumberofRS = i;
                item.CompositionNumberString = Compositions[i];                
            }
        }
    }

    public int GetNumberOfRSFromCompositionNumber(int num)
    {
        return CompositionsList[num].Length;
    }

    public void MakeCompositionDictionary()
    {
        if(Compositions != null)
        {
            int ind = 0;
            foreach (string item in Compositions)
            {
                compositionsList.Add(ind, CompositionFromStringToRSArray(item));
                ind++;
            }
        }       
        
    }

    private RollingStock[] CompositionFromStringToRSArray(string compNum)
    {
        if (compNum != null)
        {
            string tempStr = "";
            RollingStock[] tempArr = new RollingStock[compNum.Length/4];
            
            int index = 0;
            for (int i = 0; i < compNum.Length; i++)
            {
                tempStr += compNum[i];
                if((i + 1) % 4 == 0)
                {
                    //print(tempStr);
                    tempArr[index] = GetRollingStockByNumber(tempStr);
                    tempStr = "";
                    index++;
                }

            }
            return tempArr;
        }
        return null;
    }

    private RollingStock GetRollingStockByNumber(string num)
    {
        foreach (RollingStock item in cars)
        {
            if (item.Number == num)
                return item;
        }
        return null;
    }

    public void UpdateCompositionList()
    {
        CompositionsList.Clear();
        Compositions.Clear();
        foreach (RollingStock item in cars)
        {
            MakeCompositionNumberList(item);            
        }
    }



    public void MakeCompositionNumberList(RollingStock rs)
    {
        composition = "";
        if (!rs.ConnectedToPassive)
        {
            CompositionNumber = CompositionNumberFromCars(rs);
            //print( "Comp # = " + CompositionNumber);
            Compositions.Add(CompositionNumber);
        }

    }

    private string CompositionNumberFromCars(RollingStock rs)
    {
        if (!rs.ActiveCoupler.ConnectedToActive)
        {
            return rs.Number;
        }            
        composition += rs.ActiveCoupler.ConnectedToActive.transform.parent.GetComponent<RollingStock>().Number;
        CompositionNumberFromCars(rs.ActiveCoupler.ConnectedToActive.transform.parent.GetComponent<RollingStock>());
        return rs.Number + composition;
    }

}
