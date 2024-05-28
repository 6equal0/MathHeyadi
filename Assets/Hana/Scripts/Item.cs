using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    SpeedUp
}

public class Item : MonoBehaviour
{
    public ItemType itemType;
    private PlayerMove player;
    [SerializeField] private float speedUp = 2f;
    [SerializeField] AudioSource speedUpSound;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>();
        speedUpSound = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (itemType)
            {
                case ItemType.SpeedUp:
                    speedUpSound.Play();
                    player.SpeedUp(speedUp);
                    Destroy(gameObject, .3f);
                    break;
            }
        }
    }
}
