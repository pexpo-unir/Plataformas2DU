using UnityEngine;

namespace Plataformas2DU.Enemies
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField]
        private Transform[] wayPoints;

        public int Length => wayPoints.Length;

        // TODO: Exceptions.
        public Transform this[int index]
        {
            get => wayPoints[index];
            set => wayPoints[index] = value;
        }
    }
}
