using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend
{
    interface IDestructable
    {

        int Health { get; set; }
        void TakeDamage(int damage);

        void Kill();
        
    }
}
