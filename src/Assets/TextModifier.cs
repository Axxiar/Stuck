using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextModifier : MonoBehaviour
{
    [SerializeField]
    private TextMesh Username;

    [SerializeField]
    private TextMesh Info1;

    [SerializeField]
    private TextMesh Info2;

    public void Setup(Player player)
    {
        Username.text = player.username;
        Info1.text = "";
        Info2.text = "";
    }


}
