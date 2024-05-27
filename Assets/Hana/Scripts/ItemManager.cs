using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemManager : MonoBehaviour
{
    PlayerTest player;
    //[SerializeField] TextMeshProUGUI speedTXT;

    private void Start()
    {
        player = FindObjectOfType<PlayerTest>();
    }
    private void Update()
    {
        //speedTXT.text = "Speed: " + player.moveSpeed.ToString();
    }
}
