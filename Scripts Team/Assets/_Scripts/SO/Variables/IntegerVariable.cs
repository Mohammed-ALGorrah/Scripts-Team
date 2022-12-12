using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Heros.ScritpableObjects.Variables
{
    [CreateAssetMenu(menuName = "Variables/Create Integer Variable")]
    public class IntegerVariable : ScriptableObject
    {

        public int value;
    }

}