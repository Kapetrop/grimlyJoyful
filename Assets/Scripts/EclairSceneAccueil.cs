using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EclairSceneAccueil : MonoBehaviour
{
    [SerializeField] private Sprite sprite1; // Reference to the first sprite
    [SerializeField] private Sprite sprite2; // Reference to the second sprite
    [SerializeField] private Image spriteImage; // Reference to the SpriteImage component

    void Start()
    {
        // Start the coroutine to change sprites
        StartCoroutine(ChangeSpriteAfterRandomTime());
    }

    IEnumerator ChangeSpriteAfterRandomTime()
    {
        while (true)
        {
            // Wait for a random time between 0 and 2 seconds
            float randomTime = Random.Range(0f, 5f);
            yield return new WaitForSeconds(randomTime);

            // Change the sprite to the second sprite
            spriteImage.sprite = sprite2;

            // Wait for a moment to display the second sprite
            yield return new WaitForSeconds(0.1f);

            // Change the sprite back to the first sprite
            spriteImage.sprite = sprite1;
        }
    }
}
