using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*The core functionality of both the EnemyFlyer and the EnemyWalker*/

[RequireComponent(typeof(RecoveryCounter))]

public class EnemyBase : MonoBehaviour
{
    [Header ("Reference")]
    [System.NonSerialized] public AudioSource audioSource;
    public Animator animator;
    private AnimatorFunctions animatorFunctions;
    [SerializeField] Instantiator instantiator;
    [System.NonSerialized] public RecoveryCounter recoveryCounter;

    [Header ("Properties")]
    [SerializeField] private GameObject deathParticles;
    [SerializeField] private int health = 3;
    public AudioClip hitSound;
    public bool isBomb;
    [SerializeField] bool requirePoundAttack; //Requires the player to use the down attack to hurt

    void Start()
    {
        recoveryCounter = GetComponent<RecoveryCounter>();
        audioSource = GetComponent<AudioSource>();
        animatorFunctions = GetComponent<AnimatorFunctions>();
    }

    void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void GetHurt(int launchDirection, int hitPower)
    {
        //Hit the enemy, causing a damage effect, and decreasing health. Allows for requiring a downward pound attack
        if ((GetComponent<Walker>() != null || GetComponent<Flyer>() != null) && !recoveryCounter.recovering)
        {
            if (!requirePoundAttack || (requirePoundAttack && NewPlayer.Instance.pounding))
            {
                NewPlayer.Instance.cameraEffects.Shake(100, 1);
                health -= hitPower;
                animator.SetTrigger("hurt");

                audioSource.pitch = (1);
                audioSource.PlayOneShot(hitSound);

                //Ensure the enemy and also the player cannot engage in hitting each other for the max recoveryTime for each
                recoveryCounter.counter = 0;
                NewPlayer.Instance.recoveryCounter.counter = 0;

                if (NewPlayer.Instance.pounding)
                {
                    NewPlayer.Instance.PoundEffect();
                }


                //The Walker being launched after getting hit is a little different than the Flyer getting launched by a hit.
                if (GetComponent<Walker>() != null)
                {
                    Walker walker = GetComponent<Walker>();
                    walker.launch = launchDirection * walker.hurtLaunchPower / 5;
                    walker.velocity.y = walker.hurtLaunchPower;
                    walker.directionSmooth = launchDirection;
                    walker.direction = walker.directionSmooth;
                }

                if (GetComponent<Flyer>() != null)
                {
                    Flyer flyer = GetComponent<Flyer>();
                    flyer.speedEased.x = launchDirection * 5;
                    flyer.speedEased.y = 4;
                    flyer.speed.x = flyer.speedEased.x;
                    flyer.speed.y = flyer.speedEased.y;
                }

                NewPlayer.Instance.FreezeFrameEffect();
            }
        }
    }

    public void Die()
    {
        if (NewPlayer.Instance.pounding)
        {
            NewPlayer.Instance.PoundEffect();
        }

        NewPlayer.Instance.cameraEffects.Shake(200, 1);
        health = 0;
        deathParticles.SetActive(true);
        deathParticles.transform.parent = transform.parent;
        instantiator.InstantiateObjects();
        Time.timeScale = 1f;
        Destroy(gameObject);
    }
}