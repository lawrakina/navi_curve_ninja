using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CloudHandler : MonoBehaviour
{
    public ParticleSystem mainParticle;

    public List<EmoContainer> emotions = new List<EmoContainer>();

    public void Initialize(EmoType emotion)
    {
        mainParticle.Play();
        var emo = emotions.First(e=>e.emoType == emotion);
        emo.emoPart.gameObject.SetActive(true);
        emo.emoPart.Play();
        Invoke("DisAction", 5f);
    }

    void DisAction()
    {
        Destroy(gameObject);
    }
}

[Serializable]
public class EmoContainer
{
    public ParticleSystem emoPart;
    public EmoType emoType;
}

public enum EmoType
{
    Cry,
    Happy,
    Falling,
    Scared,
}