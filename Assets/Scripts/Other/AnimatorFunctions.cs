using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This script can be used on pretty much any gameObject. It provides several functions that can be called with 
animation events in the animation window.*/

public class AnimatorFunctions : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem particleSystem;
    [SerializeField] private Animator setBoolInAnimator;

    // If we don't specify what audio source to play sounds through, just use the one on player.
    void Start()
    {
        if (!audioSource) audioSource = NewPlayer.Instance.audioSource;
    }

    //Hide and unhide the player
    public void HidePlayer(bool hide)
    {
        NewPlayer.Instance.Hide(hide);
    }
  
    //Sometimes we want an animated object to force the player to jump, like a jump pad.
    public void JumpPlayer(float power = 1f)
    {
        NewPlayer.Instance.Jump(power);
    }

    //Freeze and unfreeze the player movement
    void FreezePlayer(bool freeze)
    {
        NewPlayer.Instance.Freeze(freeze);
    }

    //Play a sound through the specified audioSource
    void PlaySound(AudioClip whichSound)
    {
        audioSource.PlayOneShot(whichSound);
    }

    public void EmitParticles(int amount)
    {
        particleSystem.Emit(amount);
    }

    public void ScreenShake(float power)
    {
        NewPlayer.Instance.cameraEffects.Shake(power, 1f);
    }

    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }

    public void SetAnimBoolToFalse(string boolName)
    {
        setBoolInAnimator.SetBool(boolName, false);
    }

    public void SetAnimBoolToTrue(string boolName)
    {
        setBoolInAnimator.SetBool(boolName, true);
    }

    public void FadeOutMusic()
    {
       GameManager.Instance.gameMusic.GetComponent<AudioTrigger>().maxVolume = 0f;
    }

    public void LoadScene(string whichLevel)
    {
        SceneManager.LoadScene(whichLevel);
    }

    //Slow down or speed up the game's time scale!
    public void SetTimeScaleTo(float timeScale)
    {
        Time.timeScale = timeScale;
    }
}
    