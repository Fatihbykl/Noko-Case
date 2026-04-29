using FSM;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPlayerContext
{
    // Bağımlılıkları arayüz üzerinden sağlıyoruz
    public PlayerMovement Movement { get; private set; }
    public PlayerCombat Combat { get; private set; }
    public IInputProvider Input { get; private set; }
    public StateMachine StateMachine { get; private set; }
    public Animator Animator { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<PlayerMovement>();
        Combat = GetComponent<PlayerCombat>();
        Input = GetComponent<IInputProvider>();
        
        StateMachine = new StateMachine();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Başlangıç state'ini atıyoruz
        StateMachine.ChangeState(new IdleState(this));
    }

    private void Update()
    {
        // State Machine sürekli çalışarak o anki state'in Tick metodunu çağırır
        StateMachine.Tick();
    }

    // Dışarıdan sağlık sistemi tetiklediğinde çağrılır
    public void Die()
    {
        StateMachine.ChangeState(new DeadState(this));
    }
}
