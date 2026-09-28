using UnityEngine;

public class InvencibilityPowerUp : MonoBehaviour
{
    [SerializeField] private float duration = 5f;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
          
            PlayerController player = other.GetComponent<PlayerController>();
            
            if (player != null)
            {
                // reproduce el sonido de invencibilidad desde el SFXManager central
                SFXManager sfx = FindFirstObjectByType<SFXManager>();
                if (sfx != null)
                {
                    sfx.PlayInvincibility();
                }
                player.ActivateInvincibility(duration);
                Destroy(gameObject);
            }
        }
    }
}
