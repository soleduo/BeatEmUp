using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace soleduo.CharacterComponent
{
    [System.Serializable]
    public class Movement
    {
        protected float baseSpeed;
        protected Transform character;

        public event System.Action MoveDone;

        public Movement(Transform owner, float speed)
        {
            character = owner;
            baseSpeed = speed;
        }

        public void Move(Vector3 target, int frameCount = -1)
        {
            Vector3 deltaPosition = (target - character.position);
            Vector2 direction = deltaPosition.normalized;
            float displacement = frameCount > 0 ? GetDisplacement(deltaPosition, frameCount) : baseSpeed * Time.fixedDeltaTime;

            UpdatePosition(direction, displacement, frameCount);
        }

        private float GetDisplacement(Vector3 deltaPosition, int frameCount)
        {
            return deltaPosition.magnitude / frameCount;
        }

        private void UpdatePosition(Vector2 direction, float displacement, int frameCount = -1)
        {
            character.transform.position += (Vector3)direction * displacement;
            frameCount--;

            if (frameCount > 0)
            {
                LeanTween.delayedCall(Time.deltaTime, () => { UpdatePosition(direction, displacement, frameCount); });
            }
            else
            {
                MoveDone?.Invoke();
                MoveDone = null;
            }
        }
    }
}

public enum EMovementMode
{
    NORMAL,
    FORCED_PLACEMENT
}
