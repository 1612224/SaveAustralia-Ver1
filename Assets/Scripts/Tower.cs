﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Tower : GameTileContent
{
    [SerializeField, Range(0f, 3f)]
    protected float targetingRange = 2f;


    const int enemyLayerMask = 1 << 10;
    static Collider[] targetsBuffer = new Collider[100];

    public TowerType towerType;

    private TowerFactory towerFactory;
    public new TowerFactory OriginFactory
    {
        get => towerFactory;
        set
        {
            Debug.Assert(towerFactory == null, "Redefined origin factory!");
            towerFactory = value;
        }
    }



    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }


    public override void GameUpdate()
    {
       
    }

    protected virtual void Shoot()
    {

    }

    protected bool AcquireTarget(out TargetPoint target)
    {
        int hits = Physics.OverlapSphereNonAlloc(
            transform.position, targetingRange, targetsBuffer, enemyLayerMask
        );
        if (hits > 0)
        {
            target = targetsBuffer[0].GetComponent<TargetPoint>();
            Debug.Assert(target != null, "Targeted non-enemy!", targetsBuffer[0]);
            return true;
        }
        target = null;
        return false;
    }

    protected bool TrackTarget(ref TargetPoint target)
    {
        if (target == null)
        {
            return false;
        }
        Vector3 a = transform.position;
        Vector3 b = target.Position;
        if (Vector3.Distance(a, b) > targetingRange)
        {
            target = null;
            return false;
        }
        return true;
    }

    public virtual void AddDamage(int damage)
    {
        
    }
}
