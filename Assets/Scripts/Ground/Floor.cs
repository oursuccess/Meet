using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : Ground
{
    [SerializeField]
    [Tooltip("地砖对应的图像")]
    private Sprite[] sprites;
    void Start()
    {
        Type = GroundType.Floor;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
    }
    public override bool ThingCanMoveToMe(Element element)
    {
        return true;
    }
}
