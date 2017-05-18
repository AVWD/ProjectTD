using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "avwd/Units/Basic")]
public class LocalizationSO : ScriptableObject
{
    // Figure out a good approach for this
    // Instantiate per language
    // Dictionary using custom editor?
    public List<string> GameStrings = new List<string>();
}
