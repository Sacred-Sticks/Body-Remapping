using UnityEngine;

public class Avatar : MonoBehaviour
{
    [SerializeField] private string loadAvatarKey;

    private void Start()
    {
        EventManager.Trigger(loadAvatarKey, new AvatarLoadEvent(this, gameObject)); 
    }
}
