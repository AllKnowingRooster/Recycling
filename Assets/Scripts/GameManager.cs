using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int maxRound;
    private int currentRound;
    private float timeLimit;
    private Player player;
    private SpawnManager spawnManager;
    private string playerName;
    private string spawnManagerName;
    private string UICanvasName;
    private int totalTrash;
    [HideInInspector] public static GameManager instance;
    [HideInInspector] public MainGameUIHandler mainGameHandler;
    [HideInInspector] public bool isActive;
    public bool isGameOver;
    public float currentTime;
    public int currentTrash;

    private void Awake()
    {
        if (instance!=null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        maxRound = 3;
        currentRound = 1;
        timeLimit = 180;
        totalTrash = 10;
        playerName="FPSController";
        spawnManagerName = "SpawnManager";
        UICanvasName = "UICanvas";
        isActive = false;
        isGameOver = false;
        SceneManager.sceneLoaded += HandleMainGameSceneLoad;
        DontDestroyOnLoad(gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= HandleMainGameSceneLoad;
    }


    private void HandleMainGameSceneLoad(Scene scene,LoadSceneMode mode)
    {
        if (scene.buildIndex==1)
        {
            player = GameObject.Find(playerName).GetComponent<Player>();
            spawnManager = GameObject.Find(spawnManagerName).GetComponent<SpawnManager>();
            mainGameHandler=GameObject.Find(UICanvasName).GetComponent<MainGameUIHandler>();
            StartCoroutine(GameLoop());
        }
    }
    private IEnumerator GameLoop()
    {
        yield return StartRound();
        yield return PlayRound();
        yield return EndRound();
    }

    private IEnumerator StartRound()
    {
        player.gameObject.SetActive(false);
        player.gameObject.SetActive(true);
        spawnManager.SpawnTrash(totalTrash);
        currentTime = timeLimit;
        currentTrash = totalTrash;
        mainGameHandler.UpdateUI();
        yield return mainGameHandler.ShowStartRound();
        isActive = true;
    }

    private IEnumerator PlayRound()
    {
        while (!isGameOver && currentTrash>0 && currentTime>0.0f )
        {
            currentTime -= Time.deltaTime;
            mainGameHandler.UpdateUI();
            yield return null;
        }
        mainGameHandler.UpdateUI();
        if (currentTime<=0.0f)
        {
            isGameOver=true;
        }

    }

    private IEnumerator EndRound()
    {
        isActive=false;
        if (currentTime <= 0.0f || isGameOver)
        {
            yield return mainGameHandler.ShowText("Game Over");
            Destroy(instance.gameObject);
            SceneManager.LoadScene(0);
        }
        else
        {
            if (currentRound==maxRound)
            {
                yield return mainGameHandler.ShowText("You Win The Game");
                Destroy(instance.gameObject);
                SceneManager.LoadScene(0);
            }
            else
            {
                yield return mainGameHandler.ShowText("You Win Round" + currentRound);
                currentRound += 1;
                totalTrash += 10;
                timeLimit -= 30;
                StartCoroutine(GameLoop());
            }
        }
    }

    public void TakeDamage()
    {
        player.TakeDamage();
    }

    public void ReduceTrash()
    {
        currentTrash -= 1;
    }

}
