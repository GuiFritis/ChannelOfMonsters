using UnityEngine;
using Pathfinding;
using Enemies;
using System.Collections;

[RequireComponent(typeof(Seeker))]
public class EnemySeeker : EnemyBase
{
    [Header("Seeking")]
    public Seeker seeker;
    public float distanceToDestination = .1f;
    private Path _path;
    private int _currentWaypoint = 0;
    private Coroutine _updatePathCoroutine;

    protected void OnValidate()
    {
        seeker = GetComponent<Seeker>();
    }

    protected override void Update()
    {
        base.Update();
    }

    #region MOVE
    public override void StartMoving()
    {
        _updatePathCoroutine = StartCoroutine(UpdatePathCoroutine());
    }

    private IEnumerator UpdatePathCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(.15f);
            CalculatePath();
        }
    }

    public override void Move()
    {
        if(_path != null)
        {
            Seek();
            transform.position += _speed * Time.deltaTime * (_path.vectorPath[_currentWaypoint] - transform.position).normalized;
            if(_path.vectorPath[_currentWaypoint].x < transform.position.x)
            {
                FlipSprites();
            }
        }
    }

    public override void StopMoving()
    {
        StopCoroutine(_updatePathCoroutine);
    }
    #endregion

    public void CalculatePath()
    {
        seeker.StartPath(transform.position, _player.transform.position, OnPathGenerated);
    }

    private void OnPathGenerated(Path path)
    {
        if(!path.error)
        {
            _path = path;
            _currentWaypoint = 0;
        }
    }

    private void Seek()
    {
        if(_currentWaypoint < _path.vectorPath.Count)
        {
            if(_currentWaypoint < _path.vectorPath.Count - 1)
            {
                if(Vector2.Distance(transform.position, _path.vectorPath[_currentWaypoint]) <= .1f)
                {
                    _currentWaypoint++;
                }
            } 
        }
    }

    private void OnDrawGizmosSelected() 
    {
        if(_path != null)
        {
            if(_currentWaypoint < _path.vectorPath.Count)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(_path.vectorPath[_currentWaypoint], 0.5f);
            }
        }
    }
}   