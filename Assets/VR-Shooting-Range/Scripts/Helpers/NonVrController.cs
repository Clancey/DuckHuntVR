using System.Collections;
using UnityEngine;

namespace ExitGames.SportShooting
{
    public class NonVrController : MonoBehaviour
    {
        public Camera playerCamera;
        
        [Range(0.1f, 10.0f)]
        public float MouseSensitivity = 2.0f;

        [SerializeField]
        Transform fireSpot;

        [SerializeField]
        private LayerMask hitLayer;

        [SerializeField]
        private GameObject canvas;

        private AudioSource shotSound;

        private float rotationX, rotationY;

        [Header("Feedback")]
        [SerializeField]
        GameObject muzzleMesh;

        [SerializeField]
        ParticleSystem muzzleFx;

        [SerializeField]
        float decreaseSpeed = 2;

        [SerializeField]
        LineRenderer _bulletTrace;
        
        public void Awake()
        {
            shotSound = GetComponent<AudioSource>();
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Update()
        {
            playerCamera.gameObject.SetActive(true);

            MenuInteraction();
            LookAround();

            FireWeapon();
        }

        public void OnGUI()
        {
            if (!GameController.Instance.IsInGame)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("Press 'RETURN' to begin.");
                GUILayout.EndVertical();
            }

         

            if (GameController.Instance.IsInGame)
            {
                GUILayout.BeginVertical();
                GUILayout.Label("Press 'ESC' to leave the game.");
                GUILayout.EndVertical();
            }
        }

        private void LookAround()
        {
            rotationX += Input.GetAxis("Mouse X") * MouseSensitivity;
            rotationY += Input.GetAxis("Mouse Y") * MouseSensitivity;

            rotationX = (rotationX < -360.0f) ? (rotationX + 360.0f) : ((rotationX > 360.0f) ? (rotationX - 360.0f) : rotationX);
            rotationY = (rotationY < -360.0f) ? (rotationY + 360.0f) : ((rotationY > 360.0f) ? (rotationY - 360.0f) : rotationY);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.rotation = Quaternion.identity * xQuaternion * yQuaternion;
        }

        private void MenuInteraction()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!GameController.Instance.IsInGame)
                {
                    GameController.Instance.StartMultiplayerGame();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (GameController.Instance.IsInGame)
                {
                    GameController.Instance.InitMainMenu();

                }
            }
        }

        private void FireWeapon()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Vector3 finalPosition = new Vector3();

                if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, 50, hitLayer))
                {
                    finalPosition = hit.point;

                    var hitObject = hit.collider.GetComponent<Destructible>();

                    if (hitObject != null)
                    {
                        hitObject.MarkToDestroy();
                    }
                }
                else
                {
                    finalPosition = fireSpot.position + Camera.main.transform.forward * 50;
                    
                }
                StopAllCoroutines();
                StartCoroutine(DisableBulletTrace(Camera.main.transform.forward,fireSpot.position));

                PlayShotFeedback(fireSpot.position, finalPosition);
            }
        }

        public void PlayShotFeedback(Vector3 position, Vector3 finalPosition)
        {
            var direction = finalPosition - position;
            direction = direction.normalized;

            _bulletTrace.SetPosition(0, position);
            _bulletTrace.SetPosition(1, finalPosition);

            PlayShotSound();
            StopAllCoroutines();
            StartCoroutine(DisableBulletTrace(direction, position));
        }
        public void PlayShotSound()
        {
            shotSound.PlayOneShot(shotSound.clip);
        }

      

        IEnumerator DisableBulletTrace(Vector3 direction, Vector3 initialPosition)
        {
            muzzleFx.Play();
            muzzleMesh.SetActive(true);
            yield return null;
            muzzleMesh.SetActive(false);

            Material m = _bulletTrace.material;
            Color color = m.GetColor("_TintColor");
            color.a = 1;
            while (color.a >= 0.1f)
            {
                color.a -= Time.deltaTime * decreaseSpeed;
                m.SetColor("_TintColor",color);
                Debug.Log(color.a);
                yield return null;
                initialPosition += direction.normalized * decreaseSpeed/2;
                _bulletTrace.SetPosition(0, initialPosition);

            }
            color.a = 0;
            m.SetColor("_TintColor", color);
        }

    }
}