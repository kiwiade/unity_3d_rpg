using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoIntensity : MonoBehaviour {

    [SerializeField]
    private Gradient nightDayColor;

    [SerializeField]
    private readonly float maxIntensity = 3f;
    [SerializeField]
    private readonly float minIntensity = 0f;
    [SerializeField]
    private float minPoint = -0.2f;

    [SerializeField]
    private readonly float maxAmbient = 1f;
    [SerializeField]
    private readonly float minAmbient = 0f;
    [SerializeField]
    private float minAmbientPoint = -0.2f;

    [SerializeField]
    private Gradient nightDayFogColor;
    [SerializeField]
    private AnimationCurve fogDensityCurve;
    [SerializeField]
    private float fogScale = 1f;

    [SerializeField]
    private float dayAtmosphereThickness = 0.4f;
    [SerializeField]
    private float nightAtmosphereThickness = 0.87f;

    [SerializeField]
    private Vector3 dayRotateSpeed;
    [SerializeField]
    private Vector3 nightRotateSpeed;

    // 낮과 밤 속도변화는 skySpeed를 조정할 것
    private float skySpeed = 1;


    Light mainLight;
    Skybox sky;
    Material skyMat;

    void Start()
    {
        mainLight = GetComponent<Light>();
        skyMat = RenderSettings.skybox;
    }

    void Update()
    {
        float tRange = 1 - minPoint;
        float dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minPoint) / tRange);
        float i = ((maxIntensity - minIntensity) * dot) + minIntensity;

        mainLight.intensity = i;

        tRange = 1 - minAmbientPoint;
        dot = Mathf.Clamp01((Vector3.Dot(mainLight.transform.forward, Vector3.down) - minAmbientPoint) / tRange);
        i = ((maxAmbient - minAmbient) * dot) + minAmbient;
        RenderSettings.ambientIntensity = i;

        mainLight.color = nightDayColor.Evaluate(dot);
        RenderSettings.ambientLight = mainLight.color;

        RenderSettings.fogColor = nightDayFogColor.Evaluate(dot);
        RenderSettings.fogDensity = fogDensityCurve.Evaluate(dot) * fogScale;

        i = ((dayAtmosphereThickness - nightAtmosphereThickness) * dot) + nightAtmosphereThickness;
        skyMat.SetFloat("_AtmosphereThickness", i);

        if (dot > 0) 
            transform.Rotate(dayRotateSpeed * Time.deltaTime * skySpeed);
        else
            transform.Rotate(nightRotateSpeed * Time.deltaTime * skySpeed);
    }
}
