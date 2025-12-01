using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI buttonText;
    private Image buttonBackground;
    private Color textOriginalColor;
    private Color buttonOriginalColor;
    private void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        buttonBackground = GetComponent<Image>();
        textOriginalColor=buttonText.color;
        buttonOriginalColor=buttonBackground.color;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonText.color = Color.white;
        buttonBackground.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonText.color = textOriginalColor;
        buttonBackground.color = buttonOriginalColor;
    }
}
