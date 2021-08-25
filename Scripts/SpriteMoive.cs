using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMoive : MonoBehaviour
{
    SpriteRenderer _sprite;
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _sprite.material.SetTextureOffset(1, new Vector2(1*Time.deltaTime, 0.1f*Time.deltaTime));
    }
}
