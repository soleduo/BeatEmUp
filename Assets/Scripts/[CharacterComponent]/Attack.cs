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

        public AttackData Data;

        public event System.Action<bool> OnAttackConnects;
        public event System.Action<int> OnAttackEnds;

        public Attack(Character owner, AttackData attackData)
        {
            character = owner;

            data = attackData;
        }

        public void StartAttack(float dir)
        {
            FrameUtility.WaitForFrame(data.frameData.anticipation, () => DoAttack(dir));
        }

        public void DoAttack(float dir)
        {
            //create collision rect in direction
            Vector3 center = character.transform.position + Vector3.right * (data.hitbox.x * 0.5f * dir);
            Rect hitbox = new Rect(center.x, center.y, data.hitbox.x, data.hitbox.y);

            Character[] targets = character.Targetting.GetAllEnemyInRange(dir, data.hitbox.x).ToArray();

            //if (targets == null || targets.Length <= 0)
            //{
            //    OnAttackConnects.Invoke(false);
            //    AttackEnds();
            //    return;
            //}
            
            foreach(Character target in targets)
            {
                target.ApplyHit(data.damage, (target.transform.position - character.transform.position).normalized * data.knockback);
            }

            bool attackConnects = !((targets == null || targets.Length <= 0));

            OnAttackConnects?.Invoke(attackConnects);
            OnAttackEnds?.Invoke(data.frameData.recovery + (attackConnects ? 0 : 30));
        }
    }
}