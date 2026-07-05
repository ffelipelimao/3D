using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class ChestBase : MonoBehaviour
{
    public Animator animator;
    public string openTrigger = "Open";
    public GameObject notificationImage;
    private float _startScale;
    private float tweenDuration = 0.3f;
    public KeyCode keyCode = KeyCode.O;
    private bool _chestOpened = false;
    public ChestItemBase chestItem;
    public SFXType openSFX = SFXType.CHEST_OPEN;

    void Start()
    {
        _startScale = notificationImage.transform.localScale.x;
        HideNotification();
    }

    void Update()
    {
        if (Input.GetKeyDown(keyCode) && notificationImage.activeSelf)
        {
            OpenChest();
        }
    }

    [NaughtyAttributes.Button]
    public void OpenChest()
    {
        if (_chestOpened) return;
        animator.SetTrigger(openTrigger);
        if (SFXPool.Instance != null)
        {
            SFXPool.Instance.Play(openSFX);
        }
        _chestOpened = true;
        Invoke(nameof(DelayedShowItem), 1f);
        Invoke(nameof(CollectedShowItem), 2f);
    }

    private void DelayedShowItem()
    {
        chestItem.ShowItem();
    }
    private void CollectedShowItem()
    {
        chestItem.Collect();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if (_chestOpened) return;
            ShowNotification();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            HideNotification();
        }
    }

    [NaughtyAttributes.Button]
    private void ShowNotification()
    {
        notificationImage.SetActive(true);
        notificationImage.transform.localScale = Vector3.zero;
        notificationImage.transform.DOScale(_startScale, tweenDuration).SetEase(Ease.OutBack);
    }

    [NaughtyAttributes.Button]
    private void HideNotification()
    {
        notificationImage.SetActive(false);
    }
}
