using System.Collections.Generic;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Trash> listTrash;
    [SerializeField] private BoxCollider spawnArea;
    private Dictionary<int,Pool> listTrashPool=new Dictionary<int, Pool>();
    private int maxItem = 2;
    [HideInInspector] private float yOffset = 10.0f;
    private void Awake()
    {
        for (int i=0;i<listTrash.Count;i++)
        {
            listTrashPool.Add(i, Pool.CreatePoolInstance(listTrash[i], maxItem));
        }
    }

    public void SpawnTrash(int totalTrash)
    {
        for (int i=0;i< totalTrash; i++)
        {
            int index = SpawnRoundRobin(i);
            PoolableObject trashObject= listTrashPool[index].GetObject();
            if (trashObject!=null)
            {
                trashObject.transform.position = RandomizeSpawnPosition(spawnArea);
            }
        }
    }


    private int SpawnRoundRobin(int index)
    {
        return index%listTrash.Count;

    }

    private Vector3 RandomizeSpawnPosition(BoxCollider box)
    {
        Vector3 minBound=box.bounds.min;
        Vector3 maxBound=box.bounds.max;

        return new Vector3(Random.Range(minBound.x, maxBound.x), minBound.y-yOffset, Random.Range(minBound.z, maxBound.z));
    }


}
