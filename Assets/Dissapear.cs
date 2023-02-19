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
    private bool appear = false;
    private Material material;

    private void Update()
    {
        if (start)
        {
            if (appear)
            {
                timer -= Time.deltaTime;
                material.SetFloat("_value", timer / time * (ending - begining) + begining);
                if (timer < 0)
                {
                    start = false;
                }
            }
            else
            {
                timer += Time.deltaTime;
                material.SetFloat("_value", timer / time * (ending - begining) + begining);
                if (timer > time)
                {
                    start = false;
                }
            }
        }
    }
    public void DissapearPlayer()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        material = sprite.sharedMaterial;
        start = true;
        timer = 0;
        appear = false;
    }
    
    public void AppearPlayer()
    {
        SpriteRenderer sprite = GetComponentInChildren<SpriteRenderer>();
        material = sprite.sharedMaterial;
        start = true;
        timer = time;
        appear = true;
    }
}
