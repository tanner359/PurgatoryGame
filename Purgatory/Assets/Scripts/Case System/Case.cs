using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Case", menuName = "Case")]
public class Case : ScriptableObject
{
    public string caseTitle;
    public List<Event> events;
}
