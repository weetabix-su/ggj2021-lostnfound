using UnityEngine;
using UnityEngine.Events;

enum when { Never, OnEntry, OnExit }

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]

public class TriggerEvent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] string checkTag = "Player";
    [SerializeField] AudioClip triggerClip;
    [SerializeField] when DestroyWhen = when.OnEntry;
    [SerializeField] when PlayClipWhen = when.OnEntry;

    [Header("Events")]
    [SerializeField] UnityEvent OnEntry;
    [SerializeField] UnityEvent OnExit;

    AudioSource sfx;

    private void OnEnable()
    {
        sfx = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (checkTag != "" ? other.CompareTag(checkTag) : true)
        {
            OnEntry.Invoke();
            if (PlayClipWhen == when.OnEntry && sfx != null && triggerClip != null) sfx.PlayOneShot(triggerClip);
            if (DestroyWhen == when.OnEntry) Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (checkTag != "" ? other.CompareTag(checkTag) : true)
        {
            OnExit.Invoke();
            if (PlayClipWhen == when.OnExit && sfx != null && triggerClip != null) sfx.PlayOneShot(triggerClip);
            if (DestroyWhen == when.OnExit) Destroy(this.gameObject);
        }
    }
}
