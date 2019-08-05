using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace soleduo.CharacterComponent
{
    public class TargetManager
    {
        public Character owner;

        public TargetManager(Character owner)
        {
            this.owner = owner;
        }

        public List<Character> GetAllEnemyInRange(float direction, float attackDistance)
        {
            List<Character> targets = new List<Character>();
            List<Character> targetList = SessionManager.instance.EnemyList.Keys.ToList<Character>();

            foreach (Character character in targetList)
            {
                CollisionInfo1D collision = CollisionCheck.CheckCollision1D(owner.transform.position.x, character.transform.position.x,
                    attackDistance, character.Data.radius, direction);

                //Debug.Log(string.Format("Collision Check {0}", character.name));
                if (!collision.isCollide)
                    continue;

                targets.Add(character);
            }

            return targets;
        }
    }
}