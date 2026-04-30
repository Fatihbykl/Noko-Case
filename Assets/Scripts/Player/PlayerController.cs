using FSM;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPlayerContext
{
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
        StateMachine.ChangeState(new IdleState(this));
    }

    private void Update()
    {
        StateMachine.Tick();
    }
    

    public void Die()
    {
        StateMachine.ChangeState(new DeadState(this));
    }
}
