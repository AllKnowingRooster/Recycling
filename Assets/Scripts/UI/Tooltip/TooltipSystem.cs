using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instace;
    [SerializeField] private Tooltip tooltip;
    [HideInInspector] public Dictionary<int, string> actionDictionary;
    private void Awake()
    {
        if (instace!=null)
        {
            return;
        }
        instace = this;
        actionDictionary = new Dictionary<int, string>();
    }

    public void Show(string itemName)
    {
        tooltip.gameObject.SetActive(true);
        tooltip.SetText(itemName);
    }

    public void Hide()
    {
        tooltip.gameObject.SetActive(false);
    }

    public void UpdateAction()
    {
        tooltip.ShowAllAction();
    }
    

}
