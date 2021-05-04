using UnityEngine;

namespace SpaceFighter.Combat
{
    public class EnemyHomingProjectile : Projectile
    {
        private GameObject _player;

        protected override void Awake()
        {
            base.Awake();
            
            this._player = GameObject.Find("Player");
        }

        protected override void Update()
        {
            var screenPos = Camera.main.WorldToScreenPoint(this._player.transform.position);
            this._mover.LookAt(screenPos);

            base.Update();
        }
    }
}