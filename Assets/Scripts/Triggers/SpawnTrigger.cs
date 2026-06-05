using UnityEngine;
using UnityEngine.Events;

// Trigger que faz o inimigo/chefão aparecer quando o Player entra na área.
// Coloque este componente em um GameObject com um Collider marcado como "Is Trigger".
[RequireComponent(typeof(Collider))]
public class SpawnTrigger : MonoBehaviour
{
    [Tooltip("Tag do objeto que ativa o trigger (normalmente o Player).")]
    public string targetTag = "Player";

    [Tooltip("Se verdadeiro, o trigger dispara apenas uma vez.")]
    public bool oneShot = true;

    [Tooltip("Inimigos / chefão que serão ativados ao entrar no trigger.")]
    public GameObject[] objectsToActivate;

    [Tooltip("Eventos extras disparados ao entrar no trigger.")]
    public UnityEvent OnActivate;

    private bool _triggered;

    // Garante que o Collider já comece como trigger ao adicionar o componente.
    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (oneShot && _triggered) return;
        if (!other.CompareTag(targetTag)) return;

        _triggered = true;
        Activate();
    }

    private void Activate()
    {
        foreach (var go in objectsToActivate)
        {
            if (go == null) continue;

            go.SetActive(true);

            // O chefão precisa ser iniciado explicitamente (anda e ataca).
            var boss = go.GetComponent<BossBase>();
            if (boss != null) boss.StartBoss();
        }

        OnActivate?.Invoke();
    }
}
