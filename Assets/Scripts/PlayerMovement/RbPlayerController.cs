using PlayerMovement.GameInputState;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerMovement
{
    /// <summary>
    /// Controlador de personaje mediante fuerzas del RB de Unity. Esta clase hace uso del new input system de Unity,
    /// para poder controlar el movimiento del personaje de forma "cartoon" con balanceos y fuerzas a modo de springs.
    /// </summary>
    public class RbPlayerController : MonoBehaviour
    {

        #region Player Components

        [Header("Player Components")]
        [SerializeField] private Animator  characterAnimator;
        [SerializeField] private Rigidbody rb;

        #endregion
        
        #region Physics Calculation

        private Rigidbody _hitRigidbody;
        
        [Header("Ground Calculation Ray")]
        [Range(0,5),SerializeField] private float groundRayDistance;
        [Range(0,1),SerializeField] private float groundRayOriginOffset;
        private Ray _groundRay;
        
        [Header("Spring Force")]
        [Range(0,1),SerializeField] private float rideHeight;
        [Range(0,5000),SerializeField]private float rideSpringDamper;
        [Range(0,5000),SerializeField]private float rideSpringStrength;

        [Header("Rotation Forces")] 
        [SerializeField]private float upRightRotationDamper;
        [SerializeField]private float upRightRotationStrength;

        [Header("Locomotion")] 
        [SerializeField] private float maxSpeed;
        [SerializeField] private float acceleration;
        [SerializeField] private float breakAcceleration;
        
        [SerializeField] private AnimationCurve accelerationCurve;
        
        private Vector3 _maxVelocity;
        private Vector3 _dir;
        private InputController _inputController;

        #endregion

        #region Properties
        private Vector3 Velocity => rb.linearVelocity;
        private Vector3 RayDir   => transform.TransformDirection(_groundRay.direction);

        #endregion

        #region LifeCycle Methods

        private void Awake() => FillReferences();

        //Seteo e inicializacion de inputs
        private void Start()
        {
            var inputConfiguration = new GameObject("Input Configuration");
            inputConfiguration.transform.SetParent(transform);
            
            var config = inputConfiguration.AddComponent<InputConfigurationInstaller>();
            
            _inputController.SetInput(config.GetInput());
            _inputController.Initialize();
        }

        private void FixedUpdate()
        {
            SetGroundRay();
            ApplyUprightForce();
            CalculateMovement();
            
            if (GroundRayHit(out var hit)) ApplySpringForce(hit);
        }
        
        //Suscripción y desuscripción a eventos del input system 
        private void OnEnable()
        {
            PlayerInputState.MovePlayer += ChangeDirection;
        }

        private void OnDisable()
        {
            PlayerInputState.MovePlayer -= ChangeDirection;
        }

        #endregion

        private void FillReferences()
        {
            TryGetComponent(out rb);
            TryGetComponent(out characterAnimator);
            TryGetComponent(out _inputController);
        }

        private void SetGroundRay() => _groundRay = new Ray(transform.position + groundRayOriginOffset * Vector3.up, Vector3.down * groundRayDistance);

        /// <summary>
        /// Aplicacion de fuerza de spring contra el "suelo" para mantener el personaje siempre en el aire haciendolo
        /// capaz de moverse a traves de superficies con forma de rampas y escaleras
        /// </summary>
        /// <param name="hit"></param>
        private void ApplySpringForce(RaycastHit hit)
        {
            var otherVel = Vector3.zero;
            
            if (_hitRigidbody != null) otherVel = _hitRigidbody.linearVelocity;

            var rayDirVel = Vector3.Dot(RayDir, Velocity);
            var otherDirVel = Vector3.Dot(RayDir, otherVel);

            var relVel = rayDirVel - otherDirVel;

            var x = hit.distance - rideHeight;

            var springForce = (x * rideSpringStrength) - (relVel * rideSpringDamper);
            
            rb.AddForce(RayDir * springForce);

            if(_hitRigidbody != null) _hitRigidbody.AddForceAtPosition(RayDir * -springForce, hit.point);
        }

        /// <summary>
        /// Método para aplicar fuerza de torque para rotar personaje y moverlo en dirección deseada.
        /// </summary>
        private void ApplyUprightForce()
        {
            var dampTorque = upRightRotationDamper * -rb.angularVelocity;
            
            if (_dir == Vector3.zero)
            {
                var springTorque = upRightRotationStrength * Vector3.Cross(rb.transform.up, Vector3.up);
                rb.AddTorque(springTorque + dampTorque, ForceMode.Acceleration);
                return;
            }
            
            var lookRotation = transform.position + _dir * upRightRotationStrength;
            
            Quaternion targetOrientation = Quaternion.LookRotation(lookRotation);       
            Quaternion rotationChange = targetOrientation * Quaternion.Inverse(rb.rotation);

            rotationChange.ToAngleAxis(out float angle, out Vector3 axis);
            if (angle > 180f)
                angle -= 360f;

            if (Mathf.Approximately(angle, 0)) {
                rb.angularVelocity = Vector3.zero;
                return;
            }

            angle *= Mathf.Deg2Rad;

            var targetAngularVelocity = axis * angle / Time.deltaTime;

            float catchUp = 1.0f;
            targetAngularVelocity *= catchUp;

            rb.AddTorque(targetAngularVelocity - rb.angularVelocity + dampTorque, ForceMode.VelocityChange);
        }
        
        private bool GroundRayHit(out RaycastHit hit) => Physics.Raycast(_groundRay,out hit ,groundRayDistance);
        
        private void ChangeDirection(object sender, InputAction.CallbackContext context)
        {
            var dir = context.ReadValue<Vector2>();
            _dir = new Vector3(dir.x,0,dir.y);
        }

        /// <summary>
        /// Cálculo de movimiento para aplicar animación de "caminar" o "correr" y aplicación de velocidad en el RB.
        /// </summary>
        private void CalculateMovement()
        {
            characterAnimator.SetFloat("Speed",rb.linearVelocity.magnitude);
            
            if (_dir == Vector3.zero || _dir.normalized != Velocity.normalized) rb.AddForce(-Velocity * breakAcceleration);

            if (Velocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = Vector3.ClampMagnitude(Velocity, maxSpeed);
                return;
            }

            _dir.Normalize();

            _maxVelocity = maxSpeed * _dir;
            
            var velDot = Mathf.Lerp(Velocity.magnitude, maxSpeed,Time.deltaTime);

            var acc = acceleration * accelerationCurve.Evaluate(velDot/maxSpeed);

            var finalVelocity = Velocity + _dir * acc;
           
            rb.AddForce(finalVelocity);
        }

        /// <summary>
        /// Gizmo que ayude a visualizar que está pasando en las diferentes fuerzas aplicadas al RB.
        /// </summary>
        private void OnDrawGizmos()
        {
            Handles.color = Color.yellow;
            Handles.DrawLine(transform.position,transform.position+_dir,8);
            
            if (!GroundRayHit(out var hit)) return;
            
            Handles.color = Color.green;
            Handles.DrawWireCube(hit.point,Vector3.one * 0.15f);
            
            Handles.color = Color.red;
            Handles.DrawLine(_groundRay.origin,_groundRay.GetPoint(groundRayDistance),5);
            Handles.Label(hit.point,"Raycast",new GUIStyle(){ fontSize = 20});
            
            Handles.color = Color.magenta;
            Handles.DrawLine(_groundRay.origin + transform.forward, _groundRay.GetPoint(rideHeight) + transform.forward,5);
            Handles.Label(_groundRay.origin + transform.forward,"RideHeight",new GUIStyle(){ fontSize = 20});

            Handles.color = Color.cyan;
            Handles.DrawLine(_groundRay.GetPoint(rideHeight) + transform.forward, hit.point + transform.forward,5);
            Handles.Label(_groundRay.GetPoint(rideHeight) + transform.forward,"Force Vector",new GUIStyle(){ fontSize = 20});
        }
    }
}
