using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonSoundManager : MonoBehaviour
{
    [System.Serializable]
    public class ButtonSound
    {
        public Button button; // 버튼
        public AudioClip hoverSound; // 마우스 오버 소리
        public AudioClip clickSound; // 클릭 소리
    }

    public List<ButtonSound> buttonSounds = new List<ButtonSound>(); // 버튼별 사운드 설정 리스트
    public AudioSource audioSource; // 오디오 재생용 AudioSource

    void Start()
    {
        // AudioSource가 설정되지 않았다면 자동으로 추가
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        // 각 버튼에 이벤트 추가
        foreach (ButtonSound bs in buttonSounds)
        {
            if (bs.button != null)
            {
                bs.button.onClick.AddListener(() => PlaySound(bs.clickSound)); // 클릭 시
                EventTrigger trigger = bs.button.gameObject.AddComponent<EventTrigger>();

                // 마우스 오버 이벤트 추가
                EventTrigger.Entry entry = new EventTrigger.Entry();
                entry.eventID = EventTriggerType.PointerEnter;
                entry.callback.AddListener((eventData) => { PlaySound(bs.hoverSound); });
                trigger.triggers.Add(entry);
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}