using UnityEngine;

public class ProjecttileAttack : MonoBehaviour
{
    public int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if(player)
        {
            player.TakeDamage(damage);
        }
        
    }
}
