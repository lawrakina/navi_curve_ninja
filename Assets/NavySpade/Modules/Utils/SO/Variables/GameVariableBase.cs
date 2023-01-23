using System;
using UnityEngine;

namespace Utils.ScriptableVariables
{
    public abstract class GameVariableBase<T> : ScriptableObject where T: struct, IComparable<T>
    {
        public T Value;
    }
}
