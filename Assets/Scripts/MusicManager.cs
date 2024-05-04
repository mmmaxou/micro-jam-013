using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Control the different music of the game. Does not control the ambiant and effects
/// </summary>
public class MusicManager : MonoBehaviourSingleton<MusicManager>
{

    public AudioMixer masterMixer;

    public AudioSource mainMusic;
    public AudioSource menuMusic;
    public AudioSource gameOverMusic;
    public AudioSource gameEndingMusic;

    /// <summary> Time in seconds the crossfade takes to transition </summary>
    public float crossfadeTime = 5f;

    private Coroutine crossfade;
    private Coroutine currentSourceCrossfade;
    private Coroutine newSourceCrossfade;

    private AudioSource finishingMusic;
    private AudioSource currentMusic;


    /// ========================================
    /// UNITY METHODS
    /// ========================================

    new protected void Awake()
    {
        base.Awake();
        this.mainMusic.volume = 0;
        this.menuMusic.volume = 0;
        this.gameOverMusic.volume = 0;
        this.currentMusic = gameOverMusic;
    }


    /// ========================================
    /// PUBLIC METHODS
    /// ========================================

    public void TransitionToMain()
    {
        this.finishingMusic = this.currentMusic;
        this.currentMusic = this.mainMusic;
        this.CrossFade(this.finishingMusic, this.currentMusic, 1f);
    }

    public void TransitionToMenu()
    {
        this.finishingMusic = this.currentMusic;
        this.currentMusic = this.menuMusic;
        this.CrossFade(this.finishingMusic, this.currentMusic, 1f);
    }

    public void TransitionToGameOver()
    {
        this.finishingMusic = this.currentMusic;
        this.currentMusic = this.gameOverMusic;
        this.CrossFade(this.finishingMusic, this.currentMusic, 1f, 1f);
    }

    public void TransitionToGameEnding()
    {
        this.finishingMusic = this.currentMusic;
        this.currentMusic = this.gameEndingMusic;
        this.CrossFade(this.finishingMusic, this.currentMusic, 1f);
    }

    /*

    https://forum.unity.com/threads/audiosource-cross-fade-component.443257/?_ga=2.203803664.1044920491.1581954103-874291043.1579535016
    
    Credit Igor Aherne. Feel free to use as you wish, but mention me in credits :)
    www.facebook.com/igor.aherne

    Audio source which holds a reference to Two audio sources, allowing to transition etween incoming sound and the previously played one.

    The idea is
    1) attach this component onto gameObject.
    2) use GetComponent() to get reference to this DoubleAudioSource,
    3) tell it which AudioClip (.mp3, .wav etc) to play.

    No need to attach any clips to the _source0 or _source1, Just call CrossFade and it will smoothly transition to the clip from the currently played one.
    _source0 and _source1 are for the component's use - you don't have to worry about them.

    */



    /// <summary>
    /// Gradually shifts the sound comming from our audio sources to the this clip:
    /// maxVolume should be in 0-to-1 range
    /// </summary>
    /// <param name="maxVolume">Should be in 0-to-1 range</param>
    public void CrossFade(AudioSource from, AudioSource to, float maxVolume)
    {
        this.CrossFade(from, to, maxVolume, this.crossfadeTime);
    }
    public void CrossFade(AudioSource from, AudioSource to, float maxVolume, float crossfadeTime)
    {
        this.EndCrossFade();
        to.Play();
        this.crossfade = StartCoroutine(Fade(from, to, maxVolume, crossfadeTime));
    }

    /// <summary>
    /// End the cross fade for the played music
    /// Reset the player
    /// </summary>
    /// <param name="maxVolume"></param>
    public void EndCrossFade(float maxVolume = 1f)
    {
        if (this.crossfade != null)
        {
            StopCoroutine(this.crossfade);
            this.crossfade = null;
            if (this.finishingMusic)
            {
                this.currentMusic.volume = maxVolume;
                this.finishingMusic.volume = 0;
                this.finishingMusic.Stop();
                this.finishingMusic = null;
            }
        }
    }


    /// ========================================
    /// COROUTINE METHODS
    /// ========================================

    /// <summary>
    /// Main Iterator loop to fade in or out the player
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="maxVolume"></param>
    /// <param name="fadingTime"></param>
    /// <returns></returns>
    IEnumerator Fade(AudioSource from, AudioSource to, float maxVolume, float fadingTime)
    {

        if (this.currentSourceCrossfade != null)
        {
            StopCoroutine(this.currentSourceCrossfade);
            this.currentSourceCrossfade = null;
        }

        if (this.newSourceCrossfade != null)
        {
            StopCoroutine(this.newSourceCrossfade);
            this.newSourceCrossfade = null;
        }

        this.currentSourceCrossfade = StartCoroutine(FadeSource(from, from.volume, 0, fadingTime));
        this.newSourceCrossfade = StartCoroutine(FadeSource(to, to.volume, maxVolume, fadingTime));

        yield break;
    }

    /// <summary>
    /// Atomic Iterator to fade in or out one source
    /// </summary>
    /// <param name="sourceToFade"></param>
    /// <param name="startVolume"></param>
    /// <param name="endVolume"></param>
    /// <param name="duration"></param>
    /// <returns></returns>
    IEnumerator FadeSource(AudioSource sourceToFade, float startVolume, float endVolume, float duration)
    {
        float startTime = Time.time;

        while (true)
        {
            float elapsed = Time.time - startTime;
            sourceToFade.volume = Mathf.Clamp01(Mathf.Lerp(startVolume, endVolume, elapsed / duration));
            if (sourceToFade.volume == endVolume)
            {
                this.EndCrossFade();
                break;
            }

            yield return null;
        }
    }
}