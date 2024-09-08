using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.StateMachine;

namespace Enemies
{    
    public class EnemyStateBase : StateBase
    {
        protected EnemyBase _enemy;

        public EnemyStateBase(EnemyBase enemy) : base()
        {
            _enemy = enemy;
        }
    }

    public class EnemyStateMoving : EnemyStateBase
    {
        public EnemyStateMoving(EnemyBase enemy) : base(enemy){}

        public override void OnStateStay()
        {
            base.OnStateEnter();
            _enemy.Move();
        }
    }

    /* public class EnemyStateFrozen : EnemyStateBase
    {
        public EnemyStateFrozen(EnemyBase enemy) : base(enemy){}

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            _enemy.Freeze((float)objs[0]);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _enemy.Unfreeze();
        }
    } */

    public class EnemyStateStunned : EnemyStateBase
    {
        public EnemyStateStunned(EnemyBase enemy) : base(enemy){}
    }

    public class EnemyStateDead : EnemyStateBase
    {
        public EnemyStateDead(EnemyBase enemy) : base(enemy){}
    }
}
