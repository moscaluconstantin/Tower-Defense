using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
     public Text livesText;

     private void Update()
     {
          livesText.text = PlayerStats.Lives + " LIVES";
     }
}
