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
    private static int totalScore = 0; // ����� ����

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

            // 1. ��������� ����
            totalScore = totalScore+1;
            if (scoreText != null)
            {
                scoreText.text = "Score: " + totalScore;
            }

            // 2. ����
            if (audioSource != null && pickupSound != null)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            // 3. ��������
            if (animator != null)
            {
                animator.SetTrigger("PickedUp");
            }

            // 4. ��������� ��������� � ������
            Collider2D col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;

           

            // 5. ������� ������ ����� ��������
            Destroy(gameObject, 0.6f);

          
    }
}
