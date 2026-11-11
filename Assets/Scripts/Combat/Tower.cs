using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Ustawienia Wieży")]
    public float range = 10f;          // Zasięg wieży
    public float fireRate = 1f;        // Ile razy na sekundę strzela (1 = raz na sekundę)
    public float damage = 1f;          // Ile obrażeń zadaje

    private float fireCountdown = 0f;

    void Update()
    {
        // 1. Szukanie najbliższego wroga
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Transform closestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        // 2. Jeśli wróg jest w zasięgu, strzelaj
        if (closestEnemy != null && shortestDistance <= range)
        {
            // Obracanie wieży w stronę wroga (żeby lufa patrzyła w cel)
            Vector3 direction = closestEnemy.position - transform.position;
            direction.y = 0; // Blokujemy pochylanie w górę/dół
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
            }

            // Licznik czasu do strzału
            if (fireCountdown <= 0f)
            {
                Shoot(closestEnemy);
                fireCountdown = 1f / fireRate;
            }
        }

        fireCountdown -= Time.deltaTime;
    }

    void Shoot(Transform target)
    {
        // Rysujemy czerwoną linię w oknie Scene, żeby widzieć strzał
        Debug.DrawLine(transform.position, target.position, Color.red, 0.2f);

        // Zamiast tworzyć pocisk, po prostu niszczymy wroga (na razie)
        Destroy(target.gameObject);
        Debug.Log("Wieża zestrzeliła wroga!");
    }

    // Rysowanie zasięgu wieży na scenie (żeby było widać)
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}