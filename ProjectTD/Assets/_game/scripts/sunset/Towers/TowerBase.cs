using UnityEngine;
using System.Collections;

public abstract class TowerBase : MonoBehaviour
{
    public SearchType searchType = SearchType.Closest;
    public enum SearchType
    {
        Closest, HighestHP, LowestHP, Strongest, Weakest
    }

    protected virtual void SearchTarget()
    {
        switch (searchType)
        {
            case SearchType.Closest:
                break;
            case SearchType.HighestHP:
                break;
            case SearchType.LowestHP:
                break;
            case SearchType.Strongest:
                break;
            case SearchType.Weakest:
                break;
            default:
                break;
        }
    }

    // Use this for initialization
    void Start()
    {
        // Check new targets every third of a second
        InvokeRepeating("SearchTarget", 1f, 1.0f/3.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
