using UnityEngine;

public class PoolableObject : MonoBehaviour
{
    public Pool objectPool;

    public virtual void OnDisable()
    {
        objectPool.ReturnToPool(this);
    }

}
