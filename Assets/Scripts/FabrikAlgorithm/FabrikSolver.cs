using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace FabrikAlgorithm
{
    public class FabrikSolver : MonoBehaviour
    {
       [SerializeField] private Transform[] bones;
       private float[] _bonesLengths;

       [SerializeField] private int solverIterations = 2; //Veces que aplicamos fabrik

       [SerializeField] private Transform targetPosition,newTargetPoint;
       [SerializeField] private LayerMask layerToIgnore;

       private UnityEvent _onDistanceChanged = new UnityEvent();
       
       private float     _distance;
       private Coroutine _moveCoroutine;
       public float Distance
       {
           get => _distance;
           set
           {
               if(Mathf.Approximately(_distance, value)) return;
               if(_distance >= 1) _onDistanceChanged?.Invoke();
               _distance = value;
           }
       }

       private void Start()
       {
           _bonesLengths = new float[bones.Length];
           
           //Calcular longuitud de cada hueso
           for (var i = 0; i < bones.Length; i++)
           {
               if (i < bones.Length - 1) _bonesLengths[i] = (bones[i + 1].position - bones[i].position).magnitude;
               else //Si es el último hueso
                    _bonesLengths[i] = 0;
           }
       }

       private void OnEnable()
       {
           _onDistanceChanged.AddListener(Step);
       }

       private void OnDisable()
       {
           _onDistanceChanged.RemoveListener(Step);
       }

       private void Update()
       {
           SolveIk();
       }

       private void FixedUpdate()
       {
           RaycastNewPosition();
           CalculateDistance();
       }

       private void SolveIk()
       {
           var finalBonesPosition = new Vector3[bones.Length];
           
           //GuardamosPosicionesActuales
           for (int i = 0; i < bones.Length; i++) finalBonesPosition[i] = bones[i].position;
           
           //Aplicamos fabrik tantas veces como se indique en "solveIterations"
           for (int i = 0; i < solverIterations; i++)
               finalBonesPosition = SolveForwardPositions(SolveInversePositions(finalBonesPosition));
           
           //Aplicamos el resultado a cada hueso
           for (int i = 0; i < bones.Length; i++)
           {
               bones[i].position = finalBonesPosition[i];
               
               //Aplicamos Rotaciones
               if(i != bones.Length -1)
                   bones[i].rotation = Quaternion.LookRotation(finalBonesPosition[i + 1] - bones[i].position);
           }
       }
       //Raycast desde el cuerpo hasta el nuevo target para desplazarlo a su nueva posición respecto al suelo
       public void RaycastNewPosition()
       {
           if (Physics.Raycast(newTargetPoint.position, newTargetPoint.TransformDirection(Vector3.down),
                   out var hit, Mathf.Infinity, layerToIgnore))
           {
               newTargetPoint.position =  new Vector3(hit.point.x,hit.point.y,hit.point.z);
           }
       }
       
       //Primera parte, desde hueso libre hasta anclado
       Vector3[] SolveInversePositions(Vector3[] forwardPositions)
       {
           var inversePositions = new Vector3[forwardPositions.Length];

           //Calculamo posiciones desde el último hueso como si estubiera en la target pos hasta el primero
           for (int i = forwardPositions.Length - 1; i >= 0; i--)
           {
               if (i == forwardPositions.Length - 1) //Si es último hueso 
                   inversePositions[i] = targetPosition.position;
               else
               {
                   var primePosition = inversePositions[i + 1];
                   var basePosition = forwardPositions[i];
                   
                   var direction = (basePosition - primePosition).normalized;
                   var distance = _bonesLengths[i];

                   inversePositions[i] = primePosition +(direction * distance);
               }
           }

           return inversePositions;
       }
       
       Vector3[] SolveForwardPositions(Vector3[] inversePositions)
       {
           var forwardPositions = new Vector3[inversePositions.Length];

           //Calculamo posiciones desde el primer hueso como si estuviera en la target pos hasta el último
           for (int i = 0; i < inversePositions.Length; i++)
           {
               if (i == 0) //Si es primer hueso 
                   forwardPositions[i] = bones[0].position;
               else
               {
                   var primePosition = inversePositions[i];
                   var secondLastPrimePosition = forwardPositions[i - 1];
                   
                   var direction = (primePosition - secondLastPrimePosition).normalized;
                   var distance = _bonesLengths[i - 1];

                   forwardPositions[i] = secondLastPrimePosition +(direction * distance);
               }
           }

           return forwardPositions;
       }
       private void Step()
       {
           _moveCoroutine ??= StartCoroutine(Move());
       }

       private IEnumerator Move()
       {
           float _progress = 0;
           float _stepScale = 5;
           
           do
           {
               _progress = Mathf.Min(_progress + Time.deltaTime * _stepScale, 1.0f);

               // Turn this 0-1 value into a parabola that goes from 0 to 1, then back to 0.
               float parabola = 1.0f - 4.0f * (_progress - 0.5f) * (_progress - 0.5f);

               // Travel in a straight line from our start position to the target.        
               Vector3 nextPos = Vector3.Slerp(targetPosition.position, newTargetPoint.position, _progress);

               // Then add a vertical arc in excess of this.
               nextPos.y += parabola * 0.5f;

               // Continue as before.
               targetPosition.LookAt(nextPos, transform.forward);
               targetPosition.position = nextPos;
               
               yield return null;
           }
           while (Distance >= 0.05f);
           
           _moveCoroutine = null;
       }
       
       private void CalculateDistance() => Distance = Vector3.Distance(targetPosition.position,newTargetPoint.position);

       private void OnDrawGizmos()
       {
           Gizmos.color = Color.red;
           Vector3 direction = newTargetPoint.TransformDirection(Vector3.down) * 5;
           Gizmos.DrawRay(newTargetPoint.position, direction);
       }
    }
}
