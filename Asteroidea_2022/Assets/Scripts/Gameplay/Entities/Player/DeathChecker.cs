using UnityEngine;
using System;
using Toolbox;

namespace Entities.Player
{
    public class DeathChecker : MonoBehaviour
    {
        #region VARIABLES
        #region SERIALIZED VARIABLES
        [Header("Death feedback")]
        [SerializeField] private GameObject deathParticles = null;
        [SerializeField] private float deathParticlesDuration = 0;
        [SerializeField] private float xMaxPosition = 0;
        [SerializeField] private float yMaxPosition = 0;
        [SerializeField] private float xMinPosition = 0;
        [SerializeField] private float yMinPosition = 0;
        #endregion

        #region STATIC VARIABLES
        public static Action OnReachLimit;
        #endregion

        #region PROTECTED VARIABLES

        #endregion

        #region PRIVATE VARIABLES
        private PlayerStats playerStats = null;

        private Timer endGameTimer = new Timer();
        #endregion
        #endregion

        #region METHODS
        #region PUBLIC METHODS
        public void DeadPlayer()
        {

            Instantiate(deathParticles, transform.position, Quaternion.identity);

            playerStats.ChangePlayerState(PlayerStats.STATE.DAMAGE);
            endGameTimer.ActiveTimer();
            Time.timeScale = 0;
        }
        #endregion

        #region STATIC METHODS

        #endregion

        #region PROTECTED METHODS

        #endregion

        #region PRIVATE METHODS
        private void Awake()
        {
            playerStats = GetComponent<PlayerStats>();
            endGameTimer.SetTimer(deathParticlesDuration, Timer.TIMER_MODE.DECREASE);
        }

        private void Update()
        {
            if (endGameTimer.Active) endGameTimer.UpdateTimer();
            if (endGameTimer.ReachedTimer()) OnReachLimit?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("Limit")/* || collision.tag.Equals("Enemy") || collision.tag.Equals("Shot")*/) DeadPlayer();
        }
        #endregion
        #endregion
    }
}
