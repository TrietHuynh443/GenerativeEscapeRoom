using System.Collections;
using System.Collections.Generic;
using DI;
using UnityEngine;

public class SoundManager : UnitySingleton<SoundManager>
{
    public static AudioClip Ins1L1, Ins2L1, Ins3L1, 
    Ins4L1, Ins5L1, Ins6L1, Ins7L1, Ins8L1, Ins9L1, Ins1L2, Ins2L2, Ins3L2, Ins4L2, Ins5L2, Ins6L2,
    Ins1L3, Ins2L3, Ins3L3, Ins4L3;
    static AudioSource audioSource; 
    public GameObject screen;
    public GameObject scene2;

    public GameObject arrow;
    public Animator animArrow;
    // [Injector] private LogService _logService;
    protected override void SingletonStarted()
    {
        base.SingletonStarted();
        Ins1L1 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins1L1");
        // Ins2L1 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins2L1");
        Ins3L1 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins3L1");
        Ins4L1 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins4L1");
        Ins5L1 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins5L1");
        Ins6L1 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins6L1");
        Ins7L1 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins7L1");
        Ins8L1 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins8L1");
        Ins9L1 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins9L1");

        Ins1L2 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins1L2");
        Ins2L2 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins2L2");
        Ins3L2 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins3L2");
        Ins4L2 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins4L2");
        Ins5L2 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins5L2");
        Ins6L2 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins6L2");

        Ins1L3 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins1L3");
        Ins2L3 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins2L3");
        Ins3L3 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins3L3");
        Ins4L3 = Resources.Load<AudioClip>("Sonidos/instrucciones/Ins4L3");

        arrow.SetActive(false);
        audioSource = GetComponent<AudioSource>();
        PlaySound("Ins1L1");
        StartCoroutine(ChangeScreenInstruction("Ins1L1", "1Ins", "2Ins", 4, 15, 3));
        Debug.Log($"SoundManager Object {gameObject.name}");
        // _logService.Log($"SoundManager Object {gameObject.name}");
    }

    public IEnumerator ChangeScreenInstruction(string nombreAnim, string nameInstruction, string nameInstruction2, int secsImagen1, int secsAnim,int secsImagen2)
    {
        yield return new WaitForSeconds(secsAnim);
        arrow.SetActive(true);
        animArrow.Play(nombreAnim);
               
        yield return new WaitForSeconds(secsImagen1);
        screen.GetComponent<Renderer>().material.mainTexture = Resources.Load("RecursosTutoriales/"+ nameInstruction) as Texture;

        if(!nameInstruction2.Equals(""))
        {
            yield return new WaitForSeconds(secsImagen2);
            screen.GetComponent<Renderer>().material.mainTexture = Resources.Load("RecursosTutoriales/"+ nameInstruction2) as Texture;
        }   
    }

    public IEnumerator CambiarInstruccionPantalla2(string nombreAnim,  int secsAnim, string nombreInstruccion, int secsImagen)
    {
        yield return new WaitForSeconds(secsAnim);
        arrow.SetActive(true);
        animArrow.Play(nombreAnim);
               
        yield return new WaitForSeconds(secsImagen);
        scene2.GetComponent<Renderer>().material.mainTexture = Resources.Load("RecursosTutoriales/"+ nombreInstruccion) as Texture;
    }

    public void activateAnimation(string nombreAnim)
    {
        animArrow.Play(nombreAnim);
    }

    public void PlaySound(string clip){
        switch(clip){
        case "Ins1L1":
            audioSource.PlayOneShot(Ins1L1);
            break;
        case "Ins2L1":
            audioSource.PlayOneShot(Ins2L1);
            break;   
        case "Ins3L1":
            audioSource.PlayOneShot(Ins3L1);
            break;
        case "Ins4L1":
            audioSource.PlayOneShot(Ins4L1);
            break;
        case "Ins5L1":
            audioSource.PlayOneShot(Ins5L1);
            break;
        case "Ins6L1":
            audioSource.PlayOneShot(Ins6L1);
            break;
        case "Ins7L1":
            audioSource.PlayOneShot(Ins7L1);
            break;
        case "Ins8L1":
            audioSource.PlayOneShot(Ins8L1);
            break; 
        case "Ins9L1":
            audioSource.PlayOneShot(Ins9L1);
            break; 
        case "Ins1L2":
            audioSource.PlayOneShot(Ins1L2);
            break; 
        case "Ins2L2":
            audioSource.PlayOneShot(Ins2L2);
            break; 
        case "Ins3L2":
            audioSource.PlayOneShot(Ins3L2);
            break; 
        case "Ins4L2":
            audioSource.PlayOneShot(Ins4L2);
            break;
        case "Ins5L2":
            audioSource.PlayOneShot(Ins5L2);
            break;
        case "Ins6L2":
            audioSource.PlayOneShot(Ins6L2);
            break;
        case "Ins1L3":
            audioSource.PlayOneShot(Ins1L3);
            break;
        case "Ins2L3":
            audioSource.PlayOneShot(Ins2L3);
            break;
        case "Ins3L3":
            audioSource.PlayOneShot(Ins3L3);
            break;
        case "Ins4L3":
            audioSource.PlayOneShot(Ins4L3);
            break;
        }
    }
}
