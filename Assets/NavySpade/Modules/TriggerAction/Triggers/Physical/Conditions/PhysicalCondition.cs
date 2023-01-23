using System;
using Misc.Conditions;
using UnityEngine;

namespace Utils.TriggerAction.Triggers.Physical.Conditions
{
    [Serializable]
    public abstract class PhysicalCondition : GenericCondition<Collider>
    {
    }
}