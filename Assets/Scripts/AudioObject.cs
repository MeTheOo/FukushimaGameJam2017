using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour 
{
    public string mId = string.Empty;
    public bool mIsDestroyAfterplay = false; 
    private void Awake()
	{
        if (mId.Equals(string.Empty)) { mId = this.GetInstanceID().ToString(); }
		mAudioSource  = GetComponent<AudioSource>(); 
        SoundManager.Instance.AddAudioSouce(mId, mAudioSource);
    }
    public void Init(string iId,bool IsDestroy)
    {
		mIsDestroyAfterplay = IsDestroy;
        mId = iId;
		SoundManager.Instance.AddAudioSouce(mId, mAudioSource);
    }
    private void Update()
    {
        if(mIsDestroyAfterplay)
        {
            if(!mAudioSource.isPlaying)
            {
                Destroy(this.gameObject);
            }
        }
    }
    public void OnDestroy()
    {
        if(SoundManager.Instance !=null )
		{
			SoundManager.Instance.RemoveAudioSouce(mId);
        }
    }
    private AudioSource mAudioSource;
}
