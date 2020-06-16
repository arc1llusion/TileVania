using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmethystPickup : MonoBehaviour
{
    [SerializeField]
    private AudioClip coinPickupSfx = null;

    [SerializeField]
    private int score = 100;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickupSfx, Camera.main.transform.position);
        GameSession.Instance.AddAmethyst(score);
        Destroy(this.gameObject);
    }
}
