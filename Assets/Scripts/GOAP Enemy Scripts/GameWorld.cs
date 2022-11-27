using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class GameWorld
{
    private static readonly GameWorld gameWorld = new GameWorld();
    private static WorldStates world;

    static GameWorld()
    {
        world = new WorldStates();
    }

    private GameWorld()
    {

    }

    public static GameWorld Instance
    {
        get { return gameWorld; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}
