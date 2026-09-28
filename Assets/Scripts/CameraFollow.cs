using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; //el purrplayer
    [SerializeField] private float offsetX = 3f; //que tan adelantado se ve el gato
    private void LateUpdate()
    {
        Vector3 newPosition = transform.position;
        newPosition.x = target.position.x + offsetX;
        transform.position = newPosition;
    }
}
