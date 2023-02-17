using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteExplosion : MonoBehaviour
{
    [SerializeField] AudioSource dynamiteFx;
    [SerializeField] AudioClip explosionFx;
    [SerializeField] GameObject smokeEffect;
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject Graphics;

    private Vector2 startPosition;
    private bool explosionStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        startPosition = NewPlayer.Instance.transform.position;
        gameObject.transform.position = startPosition;
        Debug.Log("dynamite exists");
    }


    void OnCollisionEnter2D() 
    {
        Debug.Log("function works");
        if(!explosionStarted) StartCoroutine(Countdown());
        explosionStarted = true;
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(4);
        var hitColliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (Collider2D hitCollider in hitColliders)
        {
            Debug.Log("hit something");
            if (hitCollider.GetComponent<AttackHit>())
            {
                AttackHit target = hitCollider.GetComponent<AttackHit>();
                Debug.Log("found target");
                if (target.attacksWhat.HasFlag(AttackHit.AttacksWhat.DestructibleWall))
                {
                    target.ExplodeObject();
                    Debug.Log("target destroyed");
                }
            }
        }

        if(Vector2.Distance(transform.position, NewPlayer.Instance.transform.position) <= 5f)
        {
            GameManager.Instance.EndGame("PlayerExplosion");
        }
        else
        {
            explosionEffect.SetActive(true);
            smokeEffect.SetActive(true);
            Graphics.SetActive(false);
            dynamiteFx.PlayOneShot(explosionFx);
            NewPlayer.Instance.cameraEffects.Shake(100, 1f);

            StartCoroutine(Destruction());
        }
        
        
        
    }

    IEnumerator Destruction()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
