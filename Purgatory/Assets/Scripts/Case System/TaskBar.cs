using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class TaskBar : MonoBehaviour
{
    public Case _targetCase;
    public Case targetCase{
        get{
            return _targetCase;
        }
        set{
            _targetCase = value;
            UpdateDisplay();
        }
    }
   
    [Header("Components:")]
    public TMP_Text caseTitle;
    public Transform taskContainer;

    public TaskBarData data;

    private void Start()
    {
        data = TaskBarData.GetData(); 
    }

    public void UpdateDisplay()
    {
        foreach(Transform child in taskContainer.transform)
        {
            Destroy(child.gameObject);
        }

        if (targetCase) { caseTitle.text = targetCase.caseTitle; }
        else { caseTitle.text = "No Case Selected"; return; }

        foreach (Event e in targetCase.events)
        {
            if (!e.completed)
            {
                foreach (Task t in e.tasks)
                {
                    AddTask(t.description, t.progress);
                }
                break;
            }
        }

    }

    public void AddTask(string description, string progress)
    {
        GameObject a = Instantiate(data.default_task, taskContainer);
        a.GetComponent<Task_UI>().SetProperties(description, progress);
    }
    private void OnValidate()
    {
        targetCase = _targetCase;
    }

}
