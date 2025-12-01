using UnityEngine;

public class Trashcan : MonoBehaviour, IGlow, IAction
{
    private bool isGlowing;
    private MeshRenderer trashCanMeshRenderer;
    private string emissionKeyword;
    private void Awake()
    {
        isGlowing = false;
        trashCanMeshRenderer = GetComponent<MeshRenderer>();
        emissionKeyword="_EMISSION";
    }

    public void ToggleGlow()
    {
        if (isGlowing)
        {
            trashCanMeshRenderer.material.DisableKeyword(emissionKeyword);
        }
        else
        {
            trashCanMeshRenderer.material.EnableKeyword(emissionKeyword);
        }
        isGlowing = !isGlowing;
    }

    public void AddAction(int index, string action)
    {
        if (!TooltipSystem.instace.actionDictionary.ContainsKey(index))
        {
            TooltipSystem.instace.actionDictionary.Add(index, action);
        }
    }

    public void RemoveAction(int index)
    {
        if (TooltipSystem.instace.actionDictionary.ContainsKey(index))
        {
            TooltipSystem.instace.actionDictionary.Remove(index);
        }
    }

    public void Recycle(Trash trash)
    {
        trash.transform.parent = trash.objectPool.poolGameObject.transform;
        GameManager.instance.ReduceTrash();
        if ((trash.CompareTag("organic") && gameObject.CompareTag("nonOrganicTrashCan")) || (trash.CompareTag("nonOrganic") && gameObject.CompareTag("organicTrashCan")))
        {
            GameManager.instance.TakeDamage();
        }
    }
}
