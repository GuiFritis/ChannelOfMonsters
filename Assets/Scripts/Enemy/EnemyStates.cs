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

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            _enemy.StartMoving();
        }

        public override void OnStateStay()
        {
            base.OnStateEnter();
            _enemy.Move();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            _enemy.StopMoving();
        }
    }

    public class EnemyStateStunned : EnemyStateBase
    {
        public EnemyStateStunned(EnemyBase enemy) : base(enemy){}

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            _enemy.Stunned((float)objs[0]);
        }
    }

    public class EnemyStateDead : EnemyStateBase
    {
        public EnemyStateDead(EnemyBase enemy) : base(enemy){}
    }
}
