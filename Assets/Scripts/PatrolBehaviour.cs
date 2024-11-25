using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PatrolBehaviour : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _patrolPoints; // Список точек для патрулирования
   [SerializeField]
    private float _moveSpeed = 2f; // Скорость перемещения (м/с)
    [SerializeField]
    private float _waitTime = 2f; // Время ожидания на точке (секунды)

    private float _deltatime;
    private int _currentPointIndex = 0; // Индекс текущей точки
    
    private void Update()
    {
        if (_patrolPoints == null || _patrolPoints.Count < 2)
            return;
        
        MoveTowardsPoint();
    }

    private void MoveTowardsPoint()
    {
        
        var targetPoint = _patrolPoints[_currentPointIndex];
        var targetPosition = targetPoint.position;

        // Перемещение объекта к целевой точке
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _moveSpeed * Time.deltaTime);

        // Проверка достижения целевой точки
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            
            _deltatime += Time.deltaTime;
            Debug.Log(_deltatime);
            if (_deltatime >= _waitTime)
            {
                _currentPointIndex = (_currentPointIndex + 1) % _patrolPoints.Count;
                _deltatime = 0;
            }
            
        }
    }

    
}
