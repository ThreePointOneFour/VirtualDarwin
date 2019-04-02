using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KitchenSink;
using DNAUtils;

public class Gene
{
    private readonly PhysicsU.Directions Direction;
    private readonly CellType Type;

    public Gene(PhysicsU.Directions direction, CellType type) {
        Direction = direction;
        Type = type;

    }

    public Gene() {
        Direction = GetRandomDirection();
        Type = GetRandomType();
    }

    public PhysicsU.Directions GetDirection() {
        return Direction;
    }

    public CellType GetCellType() {
        return Type;
    }

    public Gene Clone(float mutationProb) {
        PhysicsU.Directions newDirection = GetDirection();
        CellType newType = GetCellType();

        if (Random.value < mutationProb) {
            if (Random.value < 0.5)
            {
                newDirection = GetRandomDirection();
            }
            else
            {
                newType = GetRandomType();
            }
        }
        return new Gene(newDirection, newType);
    }

    private PhysicsU.Directions GetRandomDirection() {
        int dirLength = BaseU.GetEnumLength(typeof(PhysicsU.Directions));
        return (PhysicsU.Directions)Random.Range(0, dirLength);
    }

    private CellType GetRandomType() {
        int typeLength = BaseU.GetEnumLength(typeof(CellType));
        return (CellType) Random.Range(0, typeLength);
    }

    public int ToNumber()
    {
        int dir = (int)GetDirection();
        int type = (int)GetDirection();

        string nbm = "" + dir + "" + type;
        return int.Parse(nbm);
    }

    public bool Equals(Gene compareTo) {
        return (compareTo.GetCellType() == GetCellType() &&
                compareTo.GetDirection() == GetDirection());
    }
}
