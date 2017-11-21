using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public enum SoundType
    {
        NONE = 0,
        SE_ENEMY_SPONE,
        SE_ENEMY_DEATH,
        SE_ENEMY_DAMAGE,
        SE_ENEMY_ATTACK,
        SE_ENEMY_MOVE,
        SE_PLAYER_MOVE,
        SE_PLAYER_CATCH,
        SE_PLAYER_DAMAGE,

        SE_BALL_SPONE,
        SE_GAME_START,
        BGM_MAINGAME,
    }

    [Serializable]
    public class SoundData
    {
        public SoundType Type;
        public bool IsLoop;
        public AudioClip AudioClip;
        public float volume = 1;
    }

    public GameObject mAudioObject;

    [SerializeField]
    private List<SoundData> mClipList = new List<SoundData>();
    private Dictionary<string, AudioSource> mAudioSourceList = new Dictionary<string, AudioSource>();

    public void AddAudioSouce(string iID, AudioSource iAudioSource)
    {
        if (!mAudioSourceList.ContainsKey(iID))
        {
            mAudioSourceList.Add(iID, iAudioSource);
        }
    }
    public void RemoveAudioSouce(string iID)
    {
        if (mAudioSourceList == null) { return; }
        if (!mAudioSourceList.ContainsKey(iID))
        {
            mAudioSourceList.Remove(iID);
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySound(SoundType iType)
    {
        GameObject aObject = Instantiate(mAudioObject);
        AudioObject aAudioObject = aObject.GetComponent<AudioObject>();
        aAudioObject.Init(aObject.GetInstanceID().ToString(), true);
        PlaySound(aObject.GetInstanceID().ToString(), iType);
    }
    public void PlaySound_Ez(int iType)
    {
        GameObject aObject = Instantiate(mAudioObject);
        AudioObject aAudioObject = aObject.GetComponent<AudioObject>();
        aAudioObject.Init(aObject.GetInstanceID().ToString(), true);
        PlaySound(aObject.GetInstanceID().ToString(), (SoundType)iType);
    }

    public void PlaySound(string iID, SoundType iType)
    {
        if (mAudioSourceList.ContainsKey(iID))
        {
            PlaySound(iType, mAudioSourceList[iID]);
        }

    }
    public void PlaySound(SoundType iType, AudioSource iAudioSource)
    {
        AudioClip aAudioClip = GetAudioClip(iType);
        if (aAudioClip != null)
        {
            iAudioSource.clip = aAudioClip;
            iAudioSource.loop = GetAudioLoop(iType);
            iAudioSource.Play();
        }
    }

    public void StopSound(string iID, SoundType iType)
    {
        if (mAudioSourceList.ContainsKey(iID))
        {
            StopSound(mAudioSourceList[iID]);
        }

    }
    public void StopSound(AudioSource iAudioSource)
    {
        iAudioSource.Stop();
    }

    private bool GetAudioLoop(SoundType iType)
    {
        foreach (SoundData aData in mClipList)
        {
            if (aData.Type == iType)
            {
                return aData.IsLoop;
            }
        }
        return false;
    }
    private AudioClip GetAudioClip(SoundType iType)
    {
        foreach (SoundData aData in mClipList)
        {
            if (aData.Type == iType)
            {
                return aData.AudioClip;
            }
        }
        return null;
    }

}
