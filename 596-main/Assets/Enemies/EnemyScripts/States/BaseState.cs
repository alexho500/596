public abstract class BaseState
{
    public Enemy enemy;
    public StateMachine stateMachine;

    // Add the StateMachine property
    public StateMachine StateMachine
    {
        get { return stateMachine; }
        set { stateMachine = value; }
    }

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
    
}