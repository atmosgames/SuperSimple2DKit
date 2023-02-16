using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Postprocess : MonoBehaviour
{
    private VolumeProfile volumeProfile;
    [SerializeField] int bugsAmount = 5; 
    private FilmGrain filmGrain;
    private float minGrain = 0f;
    private ChromaticAberration chromaticAberration;
    private float minDistortion = 0f;
    private LensDistortion lensDistortion;
    private float minAbberation = 0f;
    private Bloom bloom;
    private MotionBlur motionBlur;

    private float timeChange;

    private static Postprocess instance;
    public static Postprocess Instance
    {
        get
        {
            if (instance == null) instance = GameObject.FindObjectOfType<Postprocess>();
            return instance;
        }
    }

    private void Start()
    {
        volumeProfile = GetComponent<Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(VolumeProfile));
        timeChange = Time.time;

    }

    public void MultiplyBugEffect()
    {
        if (!volumeProfile.TryGet(out chromaticAberration)) throw new System.NullReferenceException(nameof(chromaticAberration));
        if (minAbberation < 2.0f)
        {
            minAbberation += 1.0f / bugsAmount;
            chromaticAberration.intensity.Override(minAbberation);
        }
        
        if (!volumeProfile.TryGet(out lensDistortion)) throw new System.NullReferenceException(nameof(lensDistortion));
        if(minDistortion < 0.5f)
        {
            minDistortion += 0.5f / bugsAmount;
            lensDistortion.intensity.Override(minDistortion);
        }
        if (!volumeProfile.TryGet(out filmGrain)) throw new System.NullReferenceException(nameof(filmGrain));
        if (minGrain < 1.0f)
        {
            minGrain += 1.0f / bugsAmount;
            filmGrain.intensity.Override(minGrain);
        }
    }

    public void DrunkEffect()
    {
        if (!volumeProfile.TryGet(out bloom)) throw new System.NullReferenceException(nameof(bloom)); 
        bloom.intensity.Override(12f);

        if (!volumeProfile.TryGet(out chromaticAberration)) throw new System.NullReferenceException(nameof(chromaticAberration));
        if (chromaticAberration.intensity.value >= minAbberation)
        {
            chromaticAberration.intensity.Override(((Mathf.Sin(Time.time)/2)+0.5f)*2f);
        }

        if (!volumeProfile.TryGet(out motionBlur)) throw new System.NullReferenceException(nameof(motionBlur));
        motionBlur.intensity.Override(0.2f);

        NewPlayer.Instance.cameraEffects.Shake(5, 0.5f);

    }
 
    
}
