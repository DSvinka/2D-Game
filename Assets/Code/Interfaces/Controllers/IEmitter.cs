using UnityEngine;

namespace Code.Interfaces.Controllers
{
    internal interface IEmitter
    {
        void Trow(Vector3 position, Vector3 velocity);
    }
}