using UnityEngine;

public class FloatingItem : MonoBehaviour
{
    [SerializeField] private float floatHeight = 0.25f;
    [SerializeField] private float floatSpeed = 2f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }
    private void Update()
    {
        float newY=
            startPosition.y + Mathf.Sin(Time.time *floatSpeed)* floatHeight;

        transform.position = new Vector3(
            startPosition.x,
            newY,
            startPosition.z
            );
    }
}
