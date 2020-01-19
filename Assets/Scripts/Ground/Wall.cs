using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Ground
{
    [SerializeField]
    [Tooltip("墙壁的贴图")]
    private Sprite[] sprites;
    protected override void Start()
    {
        Type = GroundType.Floor;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[UnityEngine.Random.Range(0, sprites.Length)];
        base.Start();
    }
}
