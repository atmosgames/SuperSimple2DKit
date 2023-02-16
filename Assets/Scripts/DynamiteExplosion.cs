using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamiteExplosion : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = NewPlayer.Instance.transform.position;
    }

    void OnCollisionEnter2D() 
    {
        Debug.Log("function works");
        StartCoroutine(Wait());
        
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
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
        Destroy(gameObject);
    }
}
