using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class WeatherController : MonoBehaviour
{
    [Header("Light and Color Settings")]
    [SerializeField]
    private GameObject sunObject;
    [SerializeField]
    private GameObject moonObject;
    [SerializeField]
    private GameObject skyAndFogObject; // Consider renaming to skyAndFogVolume
    [SerializeField]
    private Color morningColor;
    [SerializeField]
    private Color nightColor;

    [Header("Weather Settings")]
    public ParticleSystem rainEffect;
    [SerializeField]
    private float morningTime = 0.10f;
    [SerializeField]
    private float nightTime = 0.35f;

    [Header("SkySettings")]
    [SerializeField]
    private float dayAerosolDensity = 0.0f;
    [SerializeField]
    private float nightAerosolDensity = 0.394f;
    private PhysicallyBasedSky.RenderingMode daySkyRenderingMode =
        PhysicallyBasedSky.RenderingMode.Default;
    private PhysicallyBasedSky.RenderingMode nightSkyRenderingMode =
        PhysicallyBasedSky.RenderingMode.Material;

    [Header("Fog Settings")]
    private Volume skyAndFogVolume;
    [SerializeField]
    private float fogAttenuation = 50.0f;
    [SerializeField]
    private float fogBaseHeight = 142.0f;
    [SerializeField]
    private float fogMaximumHeight = 350.0f;
    [SerializeField]
    private float fogMaxDistance = 1000.0f;

    private VisualEnvironment visualEnvironment;
    private PhysicallyBasedSky physicallyBasedSky;
    private Fog fog;

    private void Start()
    {
        InitializeSkyAndFog();
        SetMorning();
    }

    public void SetMorning()
    {
        SetTimeOfDay(morningTime);
        SetRain(false);
        SetFog(false);
        sunObject.SetActive(true);
        moonObject.SetActive(false);
        visualEnvironment.cloudType.Override(1);
        physicallyBasedSky.aerosolDensity.Override(dayAerosolDensity);
        physicallyBasedSky.renderingMode.Override(daySkyRenderingMode);
    }

    public void SetNight()
    {
        SetTimeOfDay(nightTime);
        SetRain(false);
        SetFog(false);
        sunObject.SetActive(false);
        moonObject.SetActive(true);
        visualEnvironment.cloudType.Override(1);
        physicallyBasedSky.aerosolDensity.Override(nightAerosolDensity);
        physicallyBasedSky.renderingMode.Override(nightSkyRenderingMode);
    }

    public void SetRainy() { SetRain(true); }

    public void SetFoggy() { SetFog(true); }

    private void SetTimeOfDay(float timeOfDay)
    {
        if (timeOfDay <= morningTime || timeOfDay >= nightTime)
        {
            sunObject.GetComponent<Light>().color = morningColor;
        }
        else
        {
            moonObject.GetComponent<Light>().color = nightColor;
        }
    }

    private void SetRain(bool isRaining)
    {
        if (rainEffect != null)
        {
            if (isRaining && !rainEffect.isPlaying)
            {
                rainEffect.Play();
            }
            else if (!isRaining && rainEffect.isPlaying)
            {
                rainEffect.Stop();
            }
        }
    }

    private void SetFog(bool enableFog)
    {
        fog.enabled.Override(enableFog);
        fog.meanFreePath.Override(fogAttenuation);
        fog.baseHeight.Override(fogBaseHeight);
        fog.maximumHeight.Override(fogMaximumHeight);
        fog.maxFogDistance.Override(fogMaxDistance);
    }

    private void InitializeSkyAndFog()
    {
        if (skyAndFogObject == null)
        {
            Debug.LogError("Sky and Fog Object is not assigned!");
            return;
        }

        skyAndFogVolume = skyAndFogObject.GetComponent<Volume>();
        skyAndFogVolume.profile.TryGet(out visualEnvironment);
        skyAndFogVolume.profile.TryGet(out physicallyBasedSky);
        skyAndFogVolume.profile.TryGet(out fog);
    }
}
