using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateSprite : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private CollisionHandler coll;

    [SerializeField] private Material flashMat;
    [SerializeField] private Material defMat;
    //[SerializeField] private anima
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //anim.SetBool("onGround", coll.onGround);
    }

    /// <summary>
    /// Flips the sprite 
    /// </summary>
    /// <param name="direction">False flips left, True flips right</param>
    public void FlipSprite (bool direction) 
    {
        sr.flipX=direction;
    }

    public void FlashWhite()
    {
        sr.material = flashMat;
    }

    public void DefaultMaterial()
    {
        sr.material = defMat;
    }
}
