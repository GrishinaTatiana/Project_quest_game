using BloodTribute;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class PlayerPuppeteer : IPuppeteer
{
    Hero hero;

    HeroState defaultState;
    HeroState currentState;

    delegate bool StateConditionChecker(Hero hero);
    delegate void StateProcesser(Hero hero);
    List<(HeroState, StateConditionChecker)> StateDecider;
    Dictionary<HeroState, StateProcesser> StateProcessers;

    List<IInteractable> CurrentInteractables;

    bool isBlockedInputs = false;
    bool allowDefaulting = true;
    IInteractable lastlyInteracted;

    public PlayerPuppeteer(Hero hero)
    {
        this.hero = hero;
        hero.FinishedInteracting += ReturnControl;
        StateDecider = [];
        StateProcessers = [];

        defaultState = new HeroIdleState(hero);
        StateProcessers[defaultState] = (hero) => { hero.Velocity = new Vector2(0, hero.Gravity); };

        HeroState tmp = new HeroRunState(hero);
        StateDecider.Add((tmp, (hero) => Input.GetAxis("MoveLeft", "MoveRight") != 0));
        StateProcessers[tmp] = (hero) => { 
            hero.Velocity = new Vector2(Input.GetAxis("MoveLeft", "MoveRight") * hero.Speed, hero.Gravity); 
        };

        StateConditionChecker interactStateChecker = (hero) =>
        {
            if(!allowDefaulting) return false;
            if (Input.IsActionJustPressed("InteractMouse"))
            {
                CurrentInteractables = hero.GetIInteractableAreasUnderCursor();
                return CurrentInteractables.Count > 0;
            }
            else if (Input.IsActionJustPressed("InteractKey"))
            {
                CurrentInteractables = hero.GetIInteractableOverlappingAreas();
                return CurrentInteractables.Count > 0;
            }
            return false;
        };


        tmp = new HeroInteractState(hero);
        StateProcesser interactStateProcess = (hero) =>
        {
            hero.Velocity = new Vector2(0, hero.Gravity);
            if (!allowDefaulting && lastlyInteracted == CurrentInteractables[0])
                return;
            isBlockedInputs = true;
            lastlyInteracted = CurrentInteractables[0];
            allowDefaulting = false;


            hero.Interact(CurrentInteractables[0]);
        };
        StateDecider.Add((tmp, interactStateChecker));
        StateProcessers[tmp] = interactStateProcess;

        currentState = defaultState;
    }

    void ChangeState(HeroState state)
    {
        if(currentState == state || state == null) return;
        if (!allowDefaulting)
        {
            hero.Sprite.AnimationLooped -= AllowDefaulting;
            allowDefaulting = true;
        }
        currentState.Exit();
        currentState = state;
        currentState.Enter();
    }

    public void PhysicsProcess(double delta)
    {
        if (isBlockedInputs)
            return;
        var state = currentState;
        state = DecideState();
        ChangeState(state);
        //GD.Print(state.GetType());
        StateProcessers[currentState].Invoke(hero);
        currentState.PhysicsUpdate(delta);
    }

    public void Process(double delta)
    {
    }

    private HeroState DecideState()
    {
        HeroState tmp = null;
        if(allowDefaulting)
            tmp = defaultState;
        foreach (var e in StateDecider)
        {
            if (e.Item2.Invoke(hero))
                tmp = e.Item1;
        }
        return tmp;
    }

    void ReturnControl()
    {
        isBlockedInputs = false;
        hero.Sprite.AnimationLooped += AllowDefaulting;
    }

    void AllowDefaulting()
    {
        allowDefaulting = true;
        hero.Sprite.AnimationLooped -= AllowDefaulting;
    }

}