using System.Collections.Generic;
using pj99.Code.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Shooting.Trajectory
{
    public class TrajectoryDrawer : MonoBehaviour
    {
        [SerializeField] private TrajectoryInterruption _trajectoryInterruption;
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private int _maxPhysicsIterations;
        [SerializeField] private float _length;

        private Scene _simulationScene;
        private PhysicsScene _physicsScene;

        private static bool _isInited;
        private static Scene _simulationSceneInstance;
        private static PhysicsScene _physicsSceneInstance;

        private int _currentIterationsCount;
        private int _currentPointIndex;
        private float _currentLength;
        private bool _isStopped;

        private static readonly Dictionary<Transform, Transform> _spawnedGhosts = new Dictionary<Transform, Transform>();
        private Bullet _bulletGhost;
        private Transform _shootPoint;
        private Vector3 _stoppedPosition;

        public void Init()
        {
            if (_isInited)
            {
                InitScenes();
                return;
            }

            _isInited = true;

            CreatePhysicsScene();
            // CreateObstacles();

            InitScenes();
        }

        private void InitScenes()
        {
            _simulationScene = _simulationSceneInstance;
            _physicsScene = _physicsSceneInstance;
        }

        private void Update()
        {
            foreach (var spawnedGhost in _spawnedGhosts)
            {
                spawnedGhost.Value.position = spawnedGhost.Key.position;
                spawnedGhost.Value.rotation = spawnedGhost.Key.rotation;
            }
        }

        private void OnDestroy()
        {
            if (!_isInited)
            {
                return;
            }

            _isInited = false;
            SceneManager.UnloadSceneAsync(_simulationSceneInstance);
            _spawnedGhosts.Clear();
        }

        public void StartSimulate(Bullet bullet, Transform shootPoint, Vector3 shootPosition,
            int ricochetsCount)
        {
            _shootPoint = shootPoint;
            _bulletGhost = Instantiate(bullet, shootPosition, _shootPoint.rotation);
            SceneManager.MoveGameObjectToScene(_bulletGhost.gameObject, _simulationScene);

            _bulletGhost.Init(ricochetsCount, true);
            _bulletGhost.OnCollided += Bullet_Collided;

            _trajectoryInterruption.OnStop += Interruption_Stop;
        }

        private void Interruption_Stop()
        {
            _isStopped = true;
        }

        public void StopSimulate()
        {
            _trajectoryInterruption.OnStop -= Interruption_Stop;
            _trajectoryInterruption.StopObserve(false);
            _trajectoryInterruption.Reset();
            _lineRenderer.positionCount = 0;
            _bulletGhost.OnCollided -= Bullet_Collided;
            
            foreach (var go in _simulationScene.GetRootGameObjects()){
                if (go.TryGetComponent<Bullet>(out var bullet)){
                    Destroy(bullet.gameObject);
                }
            }

            Destroy(_bulletGhost);
        }

        public void Simulate(Vector3 velocity, Vector3 shootPosition)
        {
            _currentIterationsCount = _maxPhysicsIterations + 1;
            var startPosition = shootPosition;
            var startRotation = _shootPoint.rotation;
            
            // DebugExtension.DebugArrow(shootPosition, velocity,Color.cyan,5f);
            
            _currentLength = 0f;
            _isStopped = false;
            _trajectoryInterruption.Reset();
            _bulletGhost.Shoot(velocity);

            _lineRenderer.positionCount = _currentIterationsCount;
            _lineRenderer.SetPosition(0, shootPosition);

            var simulatingTime = Time.fixedDeltaTime;
            var needStopByLength = false;

            for (_currentPointIndex = 1; _currentPointIndex < _currentIterationsCount; _currentPointIndex++)
            {
                if (_currentPointIndex > 1)
                {
                    var length = (_lineRenderer.GetPosition(_currentPointIndex - 1) -
                                  _lineRenderer.GetPosition(_currentPointIndex - 2)).magnitude;
                    _currentLength += length;
                    var potentialLength = _currentLength + length;

                    if (potentialLength > _length)
                    {
                        needStopByLength = true;
                        
                        var difference = potentialLength - _length;
                        var ratio = 1 - Mathf.InverseLerp(0, length, difference);
                        simulatingTime = Mathf.Lerp(0f, simulatingTime, ratio);
                    }
                }
                
                _physicsScene.Simulate(simulatingTime);

                if (_isStopped)
                {
                    _lineRenderer.positionCount = _currentPointIndex;
                    break;
                }

                _lineRenderer.SetPosition(_currentPointIndex, _bulletGhost.RigidBodyPosition);

                if (needStopByLength)
                {
                    _lineRenderer.positionCount = _currentPointIndex + 1;
                    break;
                }
            }

            _trajectoryInterruption.Cut();
            _trajectoryInterruption.StopObserve(_isStopped);
            _bulletGhost.Stop();
            _bulletGhost.Reset(startPosition, startRotation);
        }

        private void Bullet_Collided(Vector3 position, GameObject collidedObject)
        {
            Debug.DrawLine(position, position + Vector3.forward, Color.red);
            _lineRenderer.SetPosition(_currentPointIndex, position);
            _currentIterationsCount++;
            _currentPointIndex++;
            _lineRenderer.positionCount = _currentIterationsCount;
            var length = (_lineRenderer.GetPosition(_currentPointIndex - 1) -
                _lineRenderer.GetPosition(_currentPointIndex - 2)).magnitude;
            _currentLength += length;

            _trajectoryInterruption.Check(position, collidedObject, _currentPointIndex - 1);
        }

        private static void CreatePhysicsScene()
        {
            _simulationSceneInstance =
                SceneManager.CreateScene("Simulation", new CreateSceneParameters(LocalPhysicsMode.Physics3D));

            _physicsSceneInstance = _simulationSceneInstance.GetPhysicsScene();
        }

        private static void CreateObstacles(){
            var objects = GameObject.FindGameObjectsWithTag("Walls");
            // var simulatedObjects = FindObjectsOfType<SimulatedObstacle>();
            for (var i = 0; i < objects.Length; i++)
            {
                var simulatedObject = objects[i];
                var ghostObject = Instantiate(simulatedObject.gameObject, simulatedObject.transform.position,
                    simulatedObject.transform.rotation);
                CleanComponents(ghostObject);
        
                SceneManager.MoveGameObjectToScene(ghostObject, _simulationSceneInstance);
        
                if (!ghostObject.isStatic)
                {
                    _spawnedGhosts.Add(simulatedObject.transform, ghostObject.transform);
                }
            }
        }

        private static void CleanComponents(GameObject ghostObject)
        {
            List<Component> listComponents = new List<Component>();

            ghostObject.transform.GetComponentsInChildrenRecursively<Component>(listComponents);

            foreach (var component in listComponents){
                
            }
            
            var components = ghostObject.GetComponents<Component>();
            for (var j = 0; j < components.Length; j++)
            {
                var component = components[j];
                if (component is Transform || component is Collider || component is Rigidbody
                    // || component is SimulatedObstacle)
                ){
                    if (component is Collider collider)
                    {
                        collider.isTrigger = false;
                    }

                    continue;
                }

                DestroyImmediate(component);
            }
        }
    }
}