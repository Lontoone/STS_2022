using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    public static PlayerHUD instance;
    public Text Health;
    public Text Codes;

    private void Awake() { instance = this; }

    public static void UpdateLifes() { instance.Health.text = $"Lifes: {Player.Lifes}/3"; }
    public static void UpdateCodes() { instance.Codes.text = "Codes: "; }
}
