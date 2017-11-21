using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TouchItem : MonoBehaviour
{
    public bool Picking = false;
    bool inPress = false;
    public UnityEvent OnHover;
    public UnityEvent OnClick;
    public UnityEvent OnLeave;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if (other.gameObject.GetComponent<SteamVR_TrackedController>())
        {
            Debug.Log(other.gameObject.GetComponent<SteamVR_TrackedController>() ? "true" : "false");
            SteamVR_TrackedController controller = other.gameObject.GetComponent<SteamVR_TrackedController>();
            if (controller.triggerPressed)
            {
                if (!inPress)
                {
                    inPress = !inPress;
                    OnClick.Invoke();
                    Debug.Log("OnClick");
                }

            }
            else//Hover
            {
                OnHover.Invoke();
                inPress = false;
                Debug.Log("Hover");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<SteamVR_TrackedController>())
        {
            OnLeave.Invoke();
            Debug.Log("OnLeave");
        }
    }
    public void LeaveAvtion()
    {
        GetComponent<Collider>().enabled = false;
        iTween.ScaleTo(gameObject, iTween.Hash("scale", new Vector3(0, 1, 0), "time", 0.5f, "easetype", iTween.EaseType.easeInBack));
    }

    public void ReloadLV()
    {
        SceneManager.LoadScene(0);
    }
    public void GameStart()
    {
        StartCoroutine(GameStartAction());
    }
    IEnumerator GameStartAction()
    {
        
        yield return new WaitForSeconds(1);
        SoundManager Sound = FindObjectOfType<SoundManager>();
        Sound.PlaySound("BGM_GameMain", SoundManager.SoundType.BGM_MAINGAME);
    }
}
