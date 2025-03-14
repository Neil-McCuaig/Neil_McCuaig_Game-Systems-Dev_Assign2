using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinAnimationEvents : MonoBehaviour
{
    public ParticleSystem powerUpEffect;
    public AudioSource audioSource;
    public AudioClip powerUpSoundEffect;
    public Animator animator;



    public void PlayPowerUpAnimation()
    {
        animator.Play("Power Up");
    }

    public void Powerfire()
    {
        powerUpEffect.Play();
    }
    
    public void PlayPowerUpSound()
    {
        audioSource.PlayOneShot(powerUpSoundEffect);
    }



    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.collider == )
        //{
            //take
        //}
    }

    //Copy this onto the actual head
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            //Take double damage
        }        
    }

}
