using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public float bulletSpeed = 10f; // Скорость пули
    public float bulletSpread = 5f; // Разброс пули (в градусах)
    public float fireRate = 0.1f; // Скорость стрельбы (в секундах)
    public AudioSource audioSource;

    public Player player;
    

    private float nextFireTime; // Время следующего выстрела

    private void Update()
    {
        if (Input.GetMouseButton(0) && Time.time > nextFireTime) 
        {
            if (player.ammo <= 0)
            {
                player.ReloadGun();
            }
            else
            {
                Shoot();
                player.ammo -= 1;
                audioSource.Play();
                nextFireTime = Time.time + fireRate; // Задержка перед следующим выстрелом
            }
        }
    }

    private void Shoot()
    {
        // Создание экземпляра пули
        var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);

        // Получение направления на цель (в данном случае - мышь)
        var targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        targetPosition.z = 0; // Установка координаты Z на 0, чтобы пуля летела по плоскости

        // Добавление случайного разброса
        Vector3 randomDirection = Random.insideUnitCircle * (Mathf.Deg2Rad * bulletSpread); // Генерируем случайное направление в пределах разброса
        var finalDirection = (targetPosition - transform.position).normalized + randomDirection; // Добавляем разброс к направлению
        
        // Добавление компонента Rigidbody, если он еще не добавлен
        var rb = bullet.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = bullet.AddComponent<Rigidbody>();
        }
        
        rb.useGravity = false;
        
        var distance = Vector3.Distance(transform.position, targetPosition);
        
        var timeToTarget = distance / bulletSpeed;
        
        rb.velocity = finalDirection * bulletSpeed;
        
        Destroy(bullet, timeToTarget); 
    }
}
