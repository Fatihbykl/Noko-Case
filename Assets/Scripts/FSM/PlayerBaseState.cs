using UnityEngine;

namespace FSM
{
    public class PlayerBaseState : IState
    {
        protected readonly IPlayerContext Ctx;

        protected PlayerBaseState(IPlayerContext context)
        {
            Ctx = context;
        }

        public virtual void Enter() { }
        public virtual void Tick() { }
        public virtual void Exit() { }
    }
}
