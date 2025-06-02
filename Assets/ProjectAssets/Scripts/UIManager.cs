using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;
    
    public void UpdatePointsText(int newScore)
    {
        pointsText.text = newScore.ToString();
    }
}