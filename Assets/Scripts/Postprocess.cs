using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class Postprocess : MonoBehaviour
{
    private VolumeProfile volumeProfile;
    private ChromaticAberration ca;
    private LensDistortion ld;
    private float minDistortion = 0f;
    private float minAbberation = 0f;

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
        

    }

    public void MultiplyBugEffect()
    {
        if (!volumeProfile.TryGet(out ca)) throw new System.NullReferenceException(nameof(ca));
        if (minAbberation < 1.0f)
        {
            minAbberation += 0.1f;
            ca.intensity.Override(minAbberation);
        }
        
        if (!volumeProfile.TryGet(out ld)) throw new System.NullReferenceException(nameof(ld));
        if(minDistortion < 0.5f)
        {
            minDistortion += 0.05f;
            ld.intensity.Override(minDistortion);
        }
        
    }
 
    
}
