using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class FitToScreen : MonoBehaviour
{
    private void Start()
    {
        Fit();
    }

    private void Update()
    {
        // Se recalcula automáticamente en el editor si mueves la cámara o la imagen
        if (!Application.isPlaying)
        {
            Fit();
        }
    }

    public void Fit()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null || sr.sprite == null) return;

        Camera mainCam = Camera.main;
        if (mainCam == null) return;

        // Centra la posición con respecto a la cámara
        transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, transform.position.z);

        // Resetea escala para medir el tamaño original
        transform.localScale = Vector3.one;

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = mainCam.orthographicSize * 2f;
        float worldScreenWidth = worldScreenHeight * mainCam.aspect;

        Vector3 scale = transform.localScale;
        scale.x = worldScreenWidth / width;
        scale.y = worldScreenHeight / height;

        transform.localScale = scale;
    }
}