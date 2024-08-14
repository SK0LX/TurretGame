using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Gun : MonoBehaviour
{
    public GameObject bulletPrefab; // Префаб пули
    public float bulletSpeed; // Скорость пули
    public float bulletSpread; // Разброс пули (в градусах)
    public float fireRate; // Скорость стрельбы (в секундах)
    
    
    public AudioSource shotAudio;
    public AudioSource soundAfterShot;
    public AudioSource reloadGunAudio;
    

    public Player player;


    private int clickButton; //0 - nothing, 1 - click, 2 - longClick
    
    private float nextFireTime; // Время следующего выстрела

    private void Update()
    {
        if (!reloadGunAudio.isPlaying)
        {
            if (Input.GetMouseButton(0) && Time.time > nextFireTime)
            {
                clickButton = 1;
                if (player.ammo <= 0)
                {
                    StartCoroutine(ReloadCoroutine());
                }
                else
                {
                    Shoot();
                    player.ammo -= 1;
                    shotAudio.Play();
                    nextFireTime = Time.time + fireRate; // Задержка перед следующим выстрелом
                }
            }
            if (clickButton == 1 && !Input.GetMouseButton(0))
            {
                soundAfterShot.Play();
                clickButton = 0;
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

    private IEnumerator ReloadCoroutine()
    {
        reloadGunAudio.Play();
        while (reloadGunAudio.isPlaying)
        {
            yield return null;
        }
        player.ReloadGun();
    }
}
