using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireStepEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem fireParticles;
    [SerializeField] float maxEmission = 25f;

    ParticleSystem.EmissionModule emission;
    float currentEmission;
    bool onFire;

    void Awake()
    {
        if (!fireParticles)
            fireParticles = GetComponentInChildren<ParticleSystem>();

        emission = fireParticles.emission;

        currentEmission = 0f;
        emission.rateOverTime = new ParticleSystem.MinMaxCurve(0f);

        fireParticles.Play();
    }

    void Update()
    {
        float target = onFire ? maxEmission : 0f;

        currentEmission = Mathf.Lerp(
            currentEmission,
            target,
            Time.deltaTime * 10f
        );

        emission.rateOverTime = new ParticleSystem.MinMaxCurve(currentEmission);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DamageZone"))
            onFire = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("DamageZone"))
            onFire = false;
    }
}
