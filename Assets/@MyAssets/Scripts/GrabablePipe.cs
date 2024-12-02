using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Connections {
    UP, DOWN, LEFT, RIGHT
}

public class GrabablePipe : Pipe
{
    [SerializeField] private List<Connections> originalConnections;
    [SerializeField] private List<Connections> rotatedConnections;

    public List<Connections> RotatedConnections { get => rotatedConnections;}

    public void RotateConnections(int steps, bool flipy, bool flipz)
    {
        //Rotation on X
        rotatedConnections = new List<Connections>(originalConnections);
        for (int i = 0; i < steps; i++)
        {
            for (int j = 0; j < rotatedConnections.Count; j++)
            {
                switch (rotatedConnections[j])
                {
                    case Connections.UP:
                        rotatedConnections[j] = Connections.RIGHT;
                        break;
                    case Connections.DOWN:
                        rotatedConnections[j] = Connections.LEFT;
                        break;
                    case Connections.LEFT:
                        rotatedConnections[j] = Connections.UP;
                        break;
                    case Connections.RIGHT:
                        rotatedConnections[j] = Connections.DOWN;
                        break;
                }
            }
            
        }
        for (int j = 0; j < rotatedConnections.Count; j++)
        {
            switch (rotatedConnections[j])
            {
                case Connections.UP:
                    if (flipz)
                    {
                        rotatedConnections[j] = Connections.DOWN;
                    }
                    break;
                case Connections.DOWN:
                    if (flipz)
                    {
                        rotatedConnections[j] = Connections.UP;
                    }
                    break;
                case Connections.LEFT:
                    if (flipy)
                    {
                        rotatedConnections[j] = Connections.RIGHT;
                    }
                    break;
                case Connections.RIGHT:
                    if (flipy)
                    {
                        rotatedConnections[j] = Connections.LEFT;
                    }
                    break;
            }
        }
    }
}
