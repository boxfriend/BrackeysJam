using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Boxfriend
{
    interface IDestructible
    {

        int Health { get; /*set; */}
        void TakeDamage(int damage);

        IEnumerator Kill();
        
    }
}
