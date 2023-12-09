using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TriggerWin : MonoBehaviour
{
    // Tiempo de cooldown en segundos
    public float cooldownTime = 3f;

    // Duración del efecto de difuminado en segundos
    public float fadeDuration = 1.5f;

    // Referencia al componente Image del objeto que tiene el efecto de difuminado
    public Image fadeImage;

    // Referencia al objeto de texto que mostrará el mensaje de misión completada
    public Text missionCompletedText;

    // Bandera para asegurarnos de que no carguemos la escena nuevamente durante el cooldown
    private bool canLoadScene = true;

    public FirstPersonMovement movement;

    // Este método se llama cuando un objeto entra en el trigger
    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si el objeto que entró es el jugador (puedes ajustar esto según tu setup)
        if (other.CompareTag("Player") && canLoadScene)
        {
            movement.enabled = false;

            // Puedes agregar cualquier lógica adicional aquí antes de cargar la siguiente escena
            Debug.Log("¡Partida completada!");

            // Desactivamos la posibilidad de cargar la escena durante el cooldown
            canLoadScene = false;

            // Mostramos el texto de misión completada
            missionCompletedText.gameObject.SetActive(true);

            // Lanzamos el método para iniciar el efecto de difuminado
            StartCoroutine(FadeAndLoad());
        }
    }

    // Método para iniciar el efecto de difuminado y cargar la siguiente escena
    private IEnumerator FadeAndLoad()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            // Calculamos el valor alpha para el efecto de difuminado
            float alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);

            // Aplicamos el valor alpha al componente Image
            fadeImage.color = new Color(0f, 0f, 0f, alpha);

            // Incrementamos el temporizador
            timer += Time.deltaTime;

            yield return null;
        }

        // Llamamos al método para cargar la siguiente escena después de que se complete el efecto de difuminado
        LoadNextScene();
    }

    // Método para cargar la siguiente escena
    private void LoadNextScene()
    {
        // Cargar la siguiente escena (asegúrate de haber configurado tus escenas en el Editor de Unity)
        SceneManager.LoadScene("Mapa");
    }
}
