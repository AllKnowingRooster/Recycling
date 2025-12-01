using UnityEngine;
using System.Collections.Generic;
public class Pool 
{
    private PoolableObject trashPrefab;
    private int poolSize;
    private List<PoolableObject> listTrash;
    public GameObject poolGameObject;
    private Pool(PoolableObject trashPrefab,int size)
    {
        this.trashPrefab = trashPrefab;
        this.poolSize = size;
        this.listTrash = new List<PoolableObject>(size);
    }

    public static Pool CreatePoolInstance(PoolableObject trashPrefab,int size)
    {
        Pool newPool= new Pool(trashPrefab,size);
        newPool.poolGameObject = new GameObject(trashPrefab + " Pool");
        newPool.GenerateObject();
        return newPool;
    }

    private void GenerateObject()
    {
        for (int i=0;i<poolSize;i++)
        {
            PoolableObject obj= GameObject.Instantiate(trashPrefab,Vector3.zero,Quaternion.identity,this.poolGameObject.transform);
            obj.name = trashPrefab.name;
            obj.objectPool = this;
            obj.gameObject.SetActive(false);
        }
    }

    public void ReturnToPool(PoolableObject trash)
    {
        listTrash.Add(trash);
    }

    public PoolableObject GetObject()
    {
        if (listTrash.Count==0)
        {
            return null;
        }

        PoolableObject trash = listTrash[0];
        trash.gameObject.SetActive(true);
        listTrash.RemoveAt(0);
        return trash;
    }

}
