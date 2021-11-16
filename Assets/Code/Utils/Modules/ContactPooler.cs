using Code.Interfaces.Controllers;
using UnityEngine;

namespace Code.Utils.Modules
{
    internal sealed class ContactPooler: IUpdate
    {
        private ContactPoint2D[] _contacts = new ContactPoint2D[10];

        private const float _collTreshhold = 3f;
        private int _contactCount;
        private Collider2D _collider2D;
        
        public bool IsGrounded { get; private set; }
        public bool HasLeftContact { get; private set; }
        public bool HasRightContact { get; private set; }

        public ContactPooler(Collider2D collider)
        {
            _collider2D = collider;
        }

        public void Update(float deltaTime)
        {
            IsGrounded = false;
            HasLeftContact = false;
            HasRightContact = false;

            _contactCount = _collider2D.GetContacts(_contacts);

            for (var i = 0; i < _contactCount; i++)
            {
                if (_contacts[i].normal.y > _collTreshhold) IsGrounded = true;
                if (_contacts[i].normal.x > _collTreshhold) HasLeftContact = true;
                if (_contacts[i].normal.x < -_collTreshhold) HasRightContact = true;
            }
        }
    }
}