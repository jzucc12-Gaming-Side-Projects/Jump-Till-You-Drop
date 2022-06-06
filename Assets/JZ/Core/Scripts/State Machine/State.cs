namespace JZ.CORE.STATEMACHINE
{
    public abstract class State
    {
        public virtual void StartState() { }
        public virtual void EndState(State nextState) { }
        public virtual void StateUpdate() { }
    }
}