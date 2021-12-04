using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ExitGames.SportShooting
{
    public class TrapFactory : MonoBehaviour
    {
        [SerializeField]
        private List<Destructible> _trapObjects;

        [SerializeField]
        float _trapSpawnInterval;
        public float TrapSpawnInterval
        {
            get
            {
                return _trapSpawnInterval;
            }
        }

        [SerializeField]
        Transform _trapSpawnPointsRoot;
        
        List<Transform> _trapSpawnPoints;
        public Transform RandomTrapSpawnPoint
        {
            get
            {
                int trapIndex = Random.Range(0, _trapSpawnPoints.Count);                
                return _trapSpawnPoints[trapIndex];
            }
        }

        void Start()
        {
            _trapSpawnPoints = new List<Transform>();
            foreach (Transform childTransform in _trapSpawnPointsRoot)
            {
                _trapSpawnPoints.Add(childTransform);
            }
        }

        public void Build()
        {
            Transform spawnPoint = RandomTrapSpawnPoint;

            System.Random random = new System.Random();
            int trapIndex = random.Next(_trapObjects.Count);
            
            var prefab = _trapObjects[trapIndex];
            var throwConfig = prefab.throwConfiguration;
                        
            Quaternion randAng = Quaternion.Euler(throwConfig.RandomTrapXAngle, throwConfig.RandomTrapYAngle, 0 );
            var go = Instantiate(prefab, spawnPoint.position, randAng);

            var dir = throwConfig.throwForward ? go.transform.forward : throwConfig.throwDirection;

            go.GetComponent<Rigidbody>().AddForce(dir * throwConfig.RandomTrapForce, ForceMode.Impulse);
        }
    }
}
 