using UnityEngine;

public class HeartsUI : MonoBehaviour
{
    [SerializeField] private GameObject[] hearts; // Heart1 a Heart5, en orden

    public void UpdateHearts(int currentLives)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].SetActive(i < currentLives);
        }
    }
}