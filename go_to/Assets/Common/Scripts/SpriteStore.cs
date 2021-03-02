using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteStore : MonoBehaviour
{
    [SerializeField]
    Sprite successSprite;
    public Sprite SuccessSprite => successSprite;

    [SerializeField]
    Sprite selectedSuccessSprite;
    public Sprite SelectedSuccessSprite => selectedSuccessSprite;

    [SerializeField]
    Sprite failureSprite;
    public Sprite FailureSprite => failureSprite;

    [SerializeField]
    Sprite selectedFailureSprite;
    public Sprite SelectedFailureSprite => selectedFailureSprite;
}
