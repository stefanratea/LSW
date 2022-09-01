using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//when something get into the alta, make the runes glow
namespace Cainos.PixelArtTopDown_Basic
{

    public class PropsAltar : MonoBehaviour
    {
        public List<SpriteRenderer> runes;
        public float lerpSpeed;

        private Color curColor;
        private Color targetColor;

        public GameObject Writing;

        private void OnTriggerEnter2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 1);
            if (Writing.active == true)
            {
                GameManager.instance.managerUI.dialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Well, hello there little girl ! Welcome to the magic shop where you can buy amazing items !";
                GameManager.instance.managerUI.dialogueBox.SetActive(true);
                Writing.SetActive(false);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            targetColor = new Color(1, 1, 1, 0);
        }

        private void Update()
        {
            curColor = Color.Lerp(curColor, targetColor, lerpSpeed * Time.deltaTime);

            foreach (var r in runes)
            {
                r.color = curColor;
            }
        }
    }
}
