using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissapear : MonoBehaviour
{
    [SerializeField] private float begining;
    [SerializeField] private float ending;
    [SerializeField] private float time;
    private float timer = 0;
    private bool start = false;

    private Material material;

    private void Update()
    {
        if (start)
        {
            timer += Time.deltaTime;
            material.SetFloat("_value", timer/time * (ending - begining) + begining);
            if(timer > time)
            {
                start = false;
            }
        }
    }
    public void DissapearPlayer()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        material = sprite.sharedMaterial;
        start = true;
        timer = 0;
    }
}
