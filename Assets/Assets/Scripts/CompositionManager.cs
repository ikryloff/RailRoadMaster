using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompositionManager : Singleton<CompositionManager>
{
    private List<string> compositions = new List<string>();
    private Dictionary<int, RollingStock[]> compositionsList = new Dictionary<int, RollingStock[]>();
    private GameObject[] carsObj;
    private RollingStock[] cars;
    string composition;
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

    void Awake () {
        carsObj = GameObject.FindGameObjectsWithTag("RollingStock");
        int i = 0;
        cars = new RollingStock[carsObj.Length];
        foreach (GameObject item in carsObj)
        {
            cars[i] = item.GetComponent<RollingStock>();
            i++;
        }

      
    }
	
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpdateCompositionList();
            MakeCompositionDictionary();
            for (int i = 0; i < Compositions.Count; i++)
            {
                foreach (RollingStock item in compositionsList[i])
                {
                    Debug.Log("In comp #" + i + " car " + item.Number);
                }
            }       
        }
        

    }

    private void MakeCompositionDictionary()
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
                    Debug.Log(tempStr);
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

    private void UpdateCompositionList()
    {
        CompositionsList.Clear();
        Compositions.Clear();
        foreach (RollingStock item in cars)
        {
            MakeCompositionNumberList(item);
        }
    }


    private void MakeCompositionNumberList(RollingStock rs)
    {
        composition = "";
        if (!rs.ConnectedToPassive)
        {
            CompositionNumber = CompositionNumberFromCars(rs);
            Debug.Log( "Comp # = " + CompositionNumber);
            Compositions.Add(CompositionNumber);
        }

    }

    private string CompositionNumberFromCars(RollingStock rs)
    {
        if (!rs.ActiveCoupler.ConnectedToActive)
            return rs.Number;
        composition += rs.ActiveCoupler.ConnectedToActive.transform.parent.GetComponent<RollingStock>().Number;
        CompositionNumberFromCars(rs.ActiveCoupler.ConnectedToActive.transform.parent.GetComponent<RollingStock>());
        return rs.Number + composition;
    }

}
