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
        public Button button; // ��ư
        public AudioClip hoverSound; // ���콺 ���� �Ҹ�
        public AudioClip clickSound; // Ŭ�� �Ҹ�
    }

    public List<ButtonSound> buttonSounds = new List<ButtonSound>(); // ��ư�� ���� ���� ����Ʈ
    public AudioSource audioSource; // ����� ����� AudioSource

    void Start()
    {
        // AudioSource�� �������� �ʾҴٸ� �ڵ����� �߰�
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }

        // �� ��ư�� �̺�Ʈ �߰�
        foreach (ButtonSound bs in buttonSounds)
        {
            if (bs.button != null)
            {
                bs.button.onClick.AddListener(() => PlaySound(bs.clickSound)); // Ŭ�� ��
                EventTrigger trigger = bs.button.gameObject.AddComponent<EventTrigger>();

                // ���콺 ���� �̺�Ʈ �߰�
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