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

    public class EnemyStateStunned : EnemyStateBase
    {
        public EnemyStateStunned(EnemyBase enemy) : base(enemy){}

        public override void OnStateEnter(params object[] objs)
        {
            base.OnStateEnter(objs);
            _enemy.Stunned((int)objs[0]);
        }
    }

    public class EnemyStateDead : EnemyStateBase
    {
        public EnemyStateDead(EnemyBase enemy) : base(enemy){}
    }
}
