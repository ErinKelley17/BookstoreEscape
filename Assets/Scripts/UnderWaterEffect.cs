using UnityEngine;

public class UnderWaterEffect : MonoBehaviour
{
    public Color waterColor = new Color(0, 0.2f, 0.3f, 0);
    public float waterDensity = 0.1f;
    public float surfaceBrightness = 1.5f;
    public float cameraHeightAdjustment = 0.05f;

    private Camera playerCam = null;
    private float waterLevel;
    private bool underWaterTrigger = false;
    private bool effectApplied = false;

    private bool origFog;
    private Color origFogColor;
    private float origFogDensity;
    private float origFogEndDistance;
    private Material origSkybox;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        origFog = RenderSettings.fog;
        origFogColor = RenderSettings.fogColor;
        origFogDensity = RenderSettings.fogDensity;
        origFogEndDistance = RenderSettings.fogEndDistance;
        origSkybox = RenderSettings.skybox;

        waterLevel = transform.position.y; //water level is height of water surface
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            if (playerObject.name.Contains("Controller")
                && playerObject.GetComponentInChildren<Camera>() != null)
            {
                playerCam = playerObject.GetComponentInChildren<Camera>();
            }
        }

        if (playerCam != null)
        {
            playerCam.backgroundColor = new Color(surfaceBrightness * waterColor.r,
                                                    surfaceBrightness * waterColor.g,
                                                    surfaceBrightness * waterColor.b,
                                                    1);
        }
        else
        {
            Debug.Log("Couldn't find player camera");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (underWaterTrigger && !effectApplied
            && playerCam.transform.position.y < waterLevel + cameraHeightAdjustment)
        {
            effectApplied = true;
            RenderSettings.fog = true;
            RenderSettings.fogColor = waterColor;
            RenderSettings.fogDensity = waterDensity;
            RenderSettings.fogEndDistance = 600;
            RenderSettings.skybox = null;

        }
        else if (effectApplied
            && playerCam.transform.position.y >= waterLevel + cameraHeightAdjustment)
        {
            effectApplied = false;
            RenderSettings.fog = origFog;
            RenderSettings.fogColor = origFogColor;
            RenderSettings.fogDensity = origFogDensity;
            RenderSettings.fogEndDistance = origFogEndDistance;
            RenderSettings.skybox = origSkybox;
        }
    }

    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            underWaterTrigger = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            underWaterTrigger = false;
            effectApplied = false;

            RenderSettings.fog = origFog;
            RenderSettings.fogColor = origFogColor;
            RenderSettings.fogDensity = origFogDensity;
            RenderSettings.fogEndDistance = origFogEndDistance;
            RenderSettings.skybox = origSkybox;
        }
    }
}
