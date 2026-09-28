using UnityEngine;

public class CoinPickable : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
PlayerController player = other.GetComponent<PlayerController>();
            if (player != null )
            {
                player.AddCoins(coinValue);
                Destroy(gameObject);
            }
        }
        
    }
}
