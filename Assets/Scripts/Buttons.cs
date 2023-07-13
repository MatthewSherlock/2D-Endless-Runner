using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public Animator animator;

    public void OpenOptions()
    {
        animator.SetBool("Options", true);
        animator.SetTrigger("MainMenuClose");
        GetComponent<AudioSource>().Play();
    }

    public void CloseOptions()
    {
        animator.SetBool("Options", false);
        GetComponent<AudioSource>().Play();
    }

    public void OpenCosmetics()
    {
        animator.SetBool("Cosmetics", true);
        animator.SetTrigger("MainMenuClose");
        GetComponent<AudioSource>().Play();
    }

    public void CloseCosmetics()
    {
        animator.SetBool("Cosmetics", false);
        GetComponent<AudioSource>().Play();
    }

    public void OpenShop()
    {
        animator.SetBool("Shop", true);
        animator.SetTrigger("MainMenuClose");
        GetComponent<AudioSource>().Play();
    }

    public void CloseShop()
    {
        animator.SetBool("Shop", false);
        GetComponent<AudioSource>().Play();
    }
}
