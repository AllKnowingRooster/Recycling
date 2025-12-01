using System.Collections;
using TMPro;
using UnityEngine;

public class MainGameUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeUI;
    [SerializeField] private TextMeshProUGUI objectUI;
    [SerializeField] private TextMeshProUGUI livesUI;
    [SerializeField] private TextMeshProUGUI roundText;
    [SerializeField] private Player playerLife;
    private float roundDelay=5.0f;
    private WaitForSeconds afterRoundWait=new WaitForSeconds(2.0f);

    public void UpdateUI()
    {
        timeUI.text = Mathf.Ceil(GameManager.instance.currentTime).ToString();
        objectUI.text = GameManager.instance.currentTrash.ToString();
        livesUI.text=playerLife.currentLife.ToString();
    }

    public IEnumerator ShowStartRound()
    {
        roundText.gameObject.SetActive(true);
        float time = roundDelay;
        while (time>0.0f)
        {
            time -= Time.deltaTime;
            roundText.text="Round Start in "+Mathf.Ceil(time).ToString();
            yield return null;
        }
        roundText.gameObject.SetActive(false);
    }

    public IEnumerator ShowText(string text)
    {
        roundText.text = text;
        roundText.gameObject.SetActive(true);
        yield return afterRoundWait;
        roundText.gameObject.SetActive(false);
    }


}
