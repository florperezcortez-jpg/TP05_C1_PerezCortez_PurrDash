using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [System.Serializable]
    public struct ParallaxLayer
    {
        public SpriteRenderer layerRenderer;

        [Range(0f, 1f)]
        public float parallaxFactor;
    }

    [Header("Configuración de Capas")]
    [SerializeField] private ParallaxLayer[] parallaxLayers;

    [Header("Referencias")]
    [SerializeField] private Transform mainCamera;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main.transform;
        }

        if (mainCamera != null)
        {
            lastCameraPosition = mainCamera.position;
        }
    }

    private void LateUpdate()
    {
        if (mainCamera == null)
            return;

        Vector3 cameraDelta =
            mainCamera.position - lastCameraPosition;

        for (int i = 0; i < parallaxLayers.Length; i++)
        {
            if (parallaxLayers[i].layerRenderer != null)
            {
                Transform layer =
                    parallaxLayers[i].layerRenderer.transform;

                layer.position += new Vector3(
                    cameraDelta.x * parallaxLayers[i].parallaxFactor,
                    0f,
                    0f
                );
            }
        }

        lastCameraPosition = mainCamera.position;
    }
}