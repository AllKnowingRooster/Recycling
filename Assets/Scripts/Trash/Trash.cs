using UnityEngine;

public class Trash : PoolableObject,IAction,IGlow
{
    private bool isGlowing;
    private string meshGameObjectName;
    private Transform childMesh;
    private MeshRenderer childMeshRenderer;
    private string emissionKeyword = "_EMISSION";
    void OnEnable()
    {
        isGlowing = false;
        meshGameObjectName = "mesh";
        childMesh = transform.Find(meshGameObjectName);
        childMeshRenderer= childMesh.GetComponent<MeshRenderer>();
    }

    public void AddAction(int index,string action)
    {
        if (!TooltipSystem.instace.actionDictionary.ContainsKey(index)) 
        {
            TooltipSystem.instace.actionDictionary.Add(index,action);
        }
    }

    public void RemoveAction(int index)
    {
        if (TooltipSystem.instace.actionDictionary.ContainsKey(index))
        {
            TooltipSystem.instace.actionDictionary.Remove(index);
        }
    }

    public override  void OnDisable()
    {
        base.OnDisable();
    }

    public void ToggleGlow()
    {
        if (!isGlowing)
        {
            childMeshRenderer.material.EnableKeyword(emissionKeyword);
        }
        else
        {
            childMeshRenderer.material.DisableKeyword(emissionKeyword);
        }
        isGlowing = !isGlowing;
    }
}
