using UnityEngine;
using TMPro;

public class PickupItem : MonoBehaviour
{
    public int scoreValue = 1;
    public TMP_Text scoreText;
    public AudioClip pickupSound;
    public Animator animator;

    private AudioSource audioSource;
    private bool pickedUp = false;
    private static int totalScore = 0; // общий счёт

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (animator == null)
            animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        
            pickedUp = true;

            // 1. Обновляем счёт
            totalScore = totalScore+1;
            if (scoreText != null)
            {
                scoreText.text = "Score: " + totalScore;
            }

            // 2. Звук
            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // 3. Анимация
            if (animator != null)
            {
                animator.SetTrigger("PickedUp");
            }

            // 4. Выключаем коллайдер и спрайт
            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

           

            // 5. Удаляем объект после задержки
            Destroy(gameObject, 0.6f);

          
    }
}
