public class PlayerStateFactory
{
    PlayerStateMachine context;

    public PlayerStateFactory(PlayerStateMachine currentContext)
    {
        context = currentContext;
    }

    public PlayerBaseState Grounded()
    {
        return new PlayerGroundedState(context, this);
    }

    public PlayerBaseState Walk()
    {
        return new PlayerWalkState(context, this);
    }
    public PlayerBaseState Run() 
    {
        return new PlayerRunState(context, this);
    }
    public PlayerBaseState Crouch() 
    { 
        return new PlayerCrouchState(context, this);
    }
    public PlayerBaseState Air() 
    {
        return new PlayerAirState(context, this);
    }
    public PlayerBaseState Slide() 
    {
        return new PlayerSlideState(context, this);
    }

}
