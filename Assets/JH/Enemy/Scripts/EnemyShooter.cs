using JZ.AUDIO;
using UnityEngine;

namespace JH.ENEMY
{
    public class EnemyShooter : MonoBehaviour
    {
        #region //Cached components
        [SerializeField] private GameObject projectilePrefab = null;
        [SerializeField] private Transform projectileSpawnPoint = null;
        private Animator animator = null;
        private SoundPlayer sfxPlayer = null;
        #endregion

        #region //Firing variables
        [SerializeField] private DirectionEnum fireDirection;
        [SerializeField] private float fireRateMultiplier = 1f;
        #endregion


        #region //Monobehaviour
        private void Awake()
        {
            animator = GetComponent<Animator>();
            sfxPlayer = GetComponent<SoundPlayer>();
        }

        private void Start()
        {
            animator.speed = fireRateMultiplier;
        }
        #endregion

        #region //Firing
        public void FireProjectile()
        {
            sfxPlayer.Play("Shoot");
            var projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
            projectile.transform.parent = transform;
            projectile.GetComponent<Projectile>().SetDirection(GetDirection());
        }

        private Vector2 GetDirection()
        {
            switch(fireDirection)
            {
                default:
                case DirectionEnum.left:
                    return Vector2.left;
                case DirectionEnum.right:
                    return Vector2.right;
                case DirectionEnum.top:
                    return Vector2.up;
                case DirectionEnum.bottom:
                    return Vector2.down;
            }
        }
        #endregion
    }
}