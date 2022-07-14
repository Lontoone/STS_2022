using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public static PlayerHUD instance;
    public Text Health;

    private void Awake() { instance = this; }

    public static void UpdateLifes() { instance.Health.text = "Lifes: " + Player.Lifes.ToString(); }
}
