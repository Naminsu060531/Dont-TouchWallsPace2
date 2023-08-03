using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public enum Type { Player, Enemy, Bullet}
    public Type type;

    public enum Target { Player, Enemy }
    public Target target;
    public int HP, Speed;
}
