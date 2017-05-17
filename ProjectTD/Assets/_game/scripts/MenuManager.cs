/*
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Units
{
    public string Name;
    public Sprite Icon;
    public string Effect;
    public double Cost;
    public int Bought;
}

public class MenuManager : MonoBehaviour
{
    #region members

    public GameObject unitButtonPrefab;
    public List<Units> itemlist;
    public Transform contentPanel;
    public Text moneyDisplay;

    public double Total = 0;

    #endregion

    // Use this for initialization
	void Start () {
        PopulateList();
        moneyDisplay.text = Total.ToString("C2");
	}

	// Update is called once per frame
	void Update () {
	
	}

    private void PopulateList()
    {
        kgLogger.instance.Post(kgLogger.PostChannel.SYSTEM, "Initializing GUI", kgLogger.PostLevel.verbose);

        foreach (var unit in itemlist)
        {
            GameObject newButton = Instantiate(unitButtonPrefab) as GameObject;
            UnitButtonInfo unitInfo = newButton.GetComponent<UnitButtonInfo>();
            unitInfo.headerLabel.text = unit.Name;
            unitInfo.effectLabel.text = unit.Effect;
            unitInfo.costLabel.text = unit.Cost.ToString("C2");
            unitInfo.boughtLabel.text = unit.Bought.ToString();
            unitInfo.icon.sprite = unit.Icon;
            
            // Create a local val so the lambda is set correctly. 
            // Otherwise it would take the value from the last unit iterated.
            double thisCost = unit.Cost;
            unitInfo.button.onClick.AddListener(() => { Purchase(thisCost); });

            unitInfo.transform.SetParent(contentPanel);
        }
    }

    private void Purchase(double cost)
    {
        Total += cost;
        moneyDisplay.text = Total.ToString("C2");

        kgLogger.instance.Post(kgLogger.PostChannel.GUI, string.Format("Purchased for {0}", cost), kgLogger.PostLevel.verbose);
    }


    public void BuyUnit(string unitCode)
    {
        Debug.Log("Clicked here:" + unitCode);

        kgLogger.instance.Post(kgLogger.PostChannel.GUI, "THIS SHOULD NOT BE CALLED", kgLogger.PostLevel.verbose);
    }
}
*/