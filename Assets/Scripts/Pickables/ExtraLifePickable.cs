using UnityEngine;

public class ExtraLifePickable : MonoBehaviour
{
    [SerializeField] private int livesToAdd = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        //verifica que el objeto que lo toca tiene eltag player
        if (other.CompareTag("Player"))
        {
            //busca el vcomponente playercontroller en el jugador
            PlayerController player = other.GetComponent<PlayerController>();

            if (player !=null)
            {
                player.AddLife(livesToAdd); //para q sume la vida al player
                Destroy(gameObject); //destruye el pickable de la escena
            }
        }
    }
}
