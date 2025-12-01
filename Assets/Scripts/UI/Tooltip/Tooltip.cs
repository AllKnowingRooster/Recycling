using TMPro;
using UnityEngine;

using UnityEngine;
public class Tooltip:MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textGameObject;
    [SerializeField] private TextMeshProUGUI actionGameObject;

    public void SetText(string head)
    {
        textGameObject.text = head;
        ShowAllAction();
    }
    public void ShowAllAction()
    {
        string action = "";
        foreach (var element in TooltipSystem.instace.actionDictionary)
        {
            action += "<sprite=" + element.Key.ToString() + "> " + element.Value+" \n";
        }
        actionGameObject.text = action;
    }


}
