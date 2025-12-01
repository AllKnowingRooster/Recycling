using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject playerSpawnPosition;
    private int startingLife;
    public int currentLife;

    private void Awake()
    {
        startingLife = 3;
    }
    private void OnEnable()
    {
        currentLife = startingLife;
        transform.position=playerSpawnPosition.transform.position;
    }

    public void TakeDamage()
    {
        currentLife -= 1;
        if (currentLife<=0)
        {
            GameManager.instance.isGameOver = true;
        }
    }
}
