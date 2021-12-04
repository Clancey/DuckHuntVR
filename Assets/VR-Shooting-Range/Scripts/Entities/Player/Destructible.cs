using UnityEngine;

namespace ExitGames.SportShooting
{
    public class Destructible : MonoBehaviour
    {
        [SerializeField]
        GameObject _explosionPrefab;

        [SerializeField]
        GameObject _scorePrefab;

        [SerializeField]
        private float _autoDestructTime;
        
        public TrapThrowingConfiguration throwConfiguration;

        [SerializeField]
        Color _color;

        [SerializeField]
        private Renderer _destructibleRenderer;

        [SerializeField]
        private bool _colorRenderer;
        
        [SerializeField]
        int _hitValue = 0;

        [SerializeField]
        bool _distructableByHit = true;

        private bool _isHit = false;
        private float _timeAlive;

        private void Start()
        {
            if (_colorRenderer)
                _destructibleRenderer.material.color = _color;
        }

        private void Update()
        {
           

            _timeAlive += Time.deltaTime;
            if (_timeAlive >= _autoDestructTime)
            {
                Destroy(gameObject);
                enabled = false;
            }
        }

        void OnCollisionEnter(Collision col)
        {
            Destroy(gameObject);
        }

        public void MarkToDestroy()
        {
            DestroyByHit(1);
            // Send message to the Master client that we hit the target
            // photonView.RPC("DestroyByHit", RpcTarget.AllViaServer, PhotonNetwork.LocalPlayer.ActorNumber);
        }

        void DestroyByHit(int hitPlayerId)
        {
            CalculateScore(hitPlayerId);
            ExplodeByHit(hitPlayerId);
            
            if (_distructableByHit)
            {
                Instantiate(Resources.Load("Helper/TargetHit"), transform.position, transform.rotation);

                Destroy(gameObject);
            }
            else
            {
                Instantiate(Resources.Load("Helper/BadTarget"), transform.position, transform.rotation);
            }
        }

        private void CalculateScore(int hitPlayerId)
        {
            if (!_isHit)
            {
                _isHit = _distructableByHit;
                GameModel.Instance.CountScoreToPlayer(hitPlayerId, _hitValue);
            }
        }

        private void ExplodeByHit(int hitPlayerId)
        {
            if (_distructableByHit && _explosionPrefab != null)
            {
                var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity) as GameObject;
                var rigidbody = explosion.GetComponent<Rigidbody>();
                if (rigidbody != null)
                {
                    rigidbody.velocity = GetComponent<Rigidbody>().velocity;
                }

                ExplosionController explosionController = explosion.GetComponent<ExplosionController>();
                if (explosionController != null)
                {
                    explosionController.SetColor(_color);
                }
            }

            if (_scorePrefab != null)
            {
                var score = Instantiate(_scorePrefab, transform.position, Quaternion.identity) as GameObject;
                var text = score.GetComponent<ScoreTextField>();
                if (text != null)
                {
                    text.SetValue(_hitValue);
                }
            }
        }
    }
}
