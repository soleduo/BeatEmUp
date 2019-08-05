using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace soleduo.CharacterComponent
{
    [System.Serializable]
    public class Attack
    {
        protected AttackData data;
        protected Character character;

        public Attack(Character owner, AttackData attackData)
        {
            character = owner;

            data = new AttackData
            {
                damage = attackData.damage,
                knockback = attackData.knockback,
                hitbox = attackData.hitbox,
            };
        }

        public bool TryAttack(float dir)
        {
            //create collision rect in direction
            Vector3 center = character.transform.position + Vector3.right * (data.hitbox.width * 0.5f * dir);
            data.hitbox.center = center;

            Character[] targets = character.Targetting.GetAllEnemyInRange(dir, data.hitbox.width).ToArray();

            if (targets == null || targets.Length <= 0)
                return false;

            foreach(Character target in targets)
            {
                target.ApplyHit(data.damage, (target.transform.position - character.transform.position).normalized * data.knockback);
            }

            return true;
        }
    }
}