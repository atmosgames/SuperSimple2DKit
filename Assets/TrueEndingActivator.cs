using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrueEndingActivator : MonoBehaviour
{
    [SerializeField] private Postprocess pp;

    private void OnEnable()
    {
        pp.TrueEndingSequence();
    }
}
