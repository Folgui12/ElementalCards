using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private List<Sprite> walkingImages;

    private float speedX, speedY;

    private Rigidbody2D rb;

    private SpriteRenderer currentSprite;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSprite = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        speedX = Input.GetAxisRaw("Horizontal") * speed;
        speedY = Input.GetAxisRaw("Vertical") * speed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ChangeSprite();
    }

    private void Move()
    {
        rb.velocity = new Vector2(speedX, speedY);
    }

    private void ChangeSprite()
    {
        if(speedY < 0)
            currentSprite.sprite = walkingImages[0];
        if(speedY > 0)
            currentSprite.sprite = walkingImages[1];
        if(speedX > 0)
            currentSprite.sprite = walkingImages[2];
        if(speedX < 0)
            currentSprite.sprite = walkingImages[3];
    }

}
