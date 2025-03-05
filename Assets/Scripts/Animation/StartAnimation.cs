using UnityEngine;
using System.Collections;

public class StartAnimation : MonoBehaviour
{
    private Animator Anim;

    [Tooltip("Начальное состояние анимации")]
    public StatesStart initialState = StatesStart.Start; // Начинаем с загрузки

    [Tooltip("Время до перехода в состояние Idle (в секундах)")]
    public float timeToIdle = 3f;

    private StatesStart State
    {
        get { return (StatesStart)Anim.GetInteger("state"); }
        set { Anim.SetInteger("state", (int)value); }
    }

    void Start()
    {
        Anim = GetComponent<Animator>();
        if (Anim == null)
        {
            Debug.LogError("Animator component not found on this GameObject!");
            enabled = false;
            return;
        }

        State = initialState; // Устанавливаем начальное состояние анимации

        // Запускаем корутину для перехода в Idle после заданного времени
        StartCoroutine(SwitchToIdleAfterDelay(timeToIdle));
    }

    private IEnumerator SwitchToIdleAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        State = StatesStart.Idle; // Переключаемся в состояние Idle
    }
}

public enum StatesStart
{
    Start,
    Idle
}