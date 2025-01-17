using Assets.Scripts;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject[] enemyArray;

    public int numberOfEnemies;
    private int currentEnemies;

    public float spawnTime;

    public string nextSection;

    void Update()
    {
        if(currentEnemies >= numberOfEnemies)
        {
            int enemies = FindAnyObjectByType<EnemyMeleeController>(FindObjectsSortMode.None).Length;

            if (enemies <= 0)
            {
                LevelManager.ChangeSection(nextSection);

                this.gameObject.SetActive(false);
            }
        }
        
    }

    void SpawnEnemy()
    {
        // Posi��o de Spawn do inimigo
        Vector2 spawnPosition;

        // Limites Y
        // -0.36
        // -0.95
        spawnPosition.y = Random.Range(-0.95f, -0.36f);

        // Posi��o X m�ximo (direita) do confiner da camera + 1 de distancia
        // Pegar RightBound (limite direito) da Section (Confiner) como base
        float rightSectionBound = LevelManager.currentConfiner.BoundingShape2D.bounds.max.x;

        // Define o x do spawnPosition, igual ao ponto da DIREITA do confiner
        spawnPosition.x = rightSectionBound;

        // Instancia ("Spawna") os inimigos
        // Pega um inimigo aleat�rio da lista de inimigos
        // Spawna na posi��o spawnPosition
        // Quaternion � uma classe utilizada para trabalhar com rota��es
        Instantiate(enemyArray[Random.Range(0, enemyArray.Length)], spawnPosition, Quaternion.identity).SetActive(true);

        // Incrementa o contador de inimigos do Spawner
        currentEnemies++;

        // Se o numero de inimigos atualmente na cena for menor que o numero m�ximo de inimgos,
        // Invoca novamente a fun��o de spawn
        if (currentEnemies < numberOfEnemies)
        {
            Invoke("SpawnEnemy", spawnTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player)
        {
         
            this.GetComponent<BoxCollider2D>().enabled = false;

            SpawnEnemy();
        }
    }
}
