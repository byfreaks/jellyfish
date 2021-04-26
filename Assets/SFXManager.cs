using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioClip upgrade1;
    [SerializeField] AudioClip death2;
    [SerializeField] AudioClip wielder3;
    [SerializeField] AudioClip bigFishDeath4;
    [SerializeField] AudioClip pressButton5;
    [SerializeField] AudioClip shootHarpoon6;
    [SerializeField] AudioClip jellyFishDeath7;
    [SerializeField] AudioClip receiveDamage8;
    [SerializeField] AudioClip breakAlgae9;
    [SerializeField] AudioClip breakRock10;
    [SerializeField] AudioClip getItem11;
    [SerializeField] AudioClip lacking12;
    
    AudioSource aa;

    private void Start() {
        aa = this.gameObject.GetComponent<AudioSource>();
    }

    public void Play(int clip){
        switch (clip)
        {
            case 1:  aa.PlayOneShot(upgrade1); break;
            case 2:  aa.PlayOneShot(death2); break;
            case 3:  aa.PlayOneShot(wielder3); break;
            case 4:  aa.PlayOneShot(bigFishDeath4); break;
            case 5:  aa.PlayOneShot(pressButton5); break;
            case 6:  aa.PlayOneShot(shootHarpoon6); break;
            case 7:  aa.PlayOneShot(jellyFishDeath7); break;
            case 8:  aa.PlayOneShot(receiveDamage8); break;
            case 9:  aa.PlayOneShot(breakAlgae9); break;
            case 10: aa.PlayOneShot(breakRock10); break;
            case 11: aa.PlayOneShot(getItem11); break;
            case 12: aa.PlayOneShot(lacking12); break;
        }
    }

}
