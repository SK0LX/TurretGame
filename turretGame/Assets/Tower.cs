using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float rotationSpeed = 10f; // Скорость поворота (в градусах в секунду)
    public AudioSource rotationAudio;

    private Vector3 mousePosition;
    private bool isRotating = false; // Флаг, указывающий, вращается ли башня

    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var direction = mousePosition - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x);
        angle = Mathf.Rad2Deg * angle;

        // Вращаем башню с заданной скоростью
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), rotationSpeed * Time.deltaTime);

        // Проверяем, вращается ли башня
        if (!isRotating && Mathf.Abs(transform.rotation.eulerAngles.z - angle) > 0.1f)
        {
            isRotating = true;
            rotationAudio.Play(); // Проигрываем звук середины вращения
        }

        // Если башня перестала вращаться, останавливаем звук середины
        if (isRotating && Mathf.Abs(transform.rotation.eulerAngles.z - angle) < 0.1f)
        {
            isRotating = false;
            rotationAudio.Stop(); // Останавливаем звук середины вращения
        } else if (isRotating) {
            rotationAudio.Play(); // Повторяем звук середины вращения, если он не играет
        }
    }
}