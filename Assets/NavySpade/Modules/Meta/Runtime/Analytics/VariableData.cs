using System;

namespace Core.Meta.Analytics
{
    public enum TrackingVariableType
    {
        Int,
        Float
    }

    public enum TrackingType
    {
        Max,
        Current,
        Added,
        Reduced
    }
    
    [Serializable]
    public struct VariableData
    {
        public string Key;
        public TrackingVariableType Type;

        public int MaxIntValue;
        public int CurrentIntValue;
        public int AddIntValue;
        public int ReducedIntValue;
        
        public float MaxFloatValue;
        public float CurrentFloatValue;
        public float AddFloatValue;
        public float ReducedFloatValue;

        public float MaxValue
        {
            get
            {
                return Type switch
                {
                    TrackingVariableType.Int => MaxIntValue,
                    TrackingVariableType.Float => MaxFloatValue,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            set
            {
                switch (Type)
                {
                    case TrackingVariableType.Int:
                        MaxIntValue = (int) value;
                        return;
                    case TrackingVariableType.Float:
                        MaxFloatValue = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        public float CurrentValue
        {
            get
            {
                return Type switch
                {
                    TrackingVariableType.Int => CurrentIntValue,
                    TrackingVariableType.Float => CurrentFloatValue,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            set
            {
                switch (Type)
                {
                    case TrackingVariableType.Int:
                        CurrentIntValue = (int) value;
                        return;
                    case TrackingVariableType.Float:
                        CurrentFloatValue = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
        public float AddValue
        {
            get
            {
                return Type switch
                {
                    TrackingVariableType.Int => AddIntValue,
                    TrackingVariableType.Float => AddFloatValue,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            set
            {
                switch (Type)
                {
                    case TrackingVariableType.Int:
                        AddIntValue = (int) value;
                        return;
                    case TrackingVariableType.Float:
                        AddFloatValue = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public float ReducedValue
        {
            get
            {
                return Type switch
                {
                    TrackingVariableType.Int => ReducedIntValue,
                    TrackingVariableType.Float => ReducedFloatValue,
                    _ => throw new ArgumentOutOfRangeException()
                };
            }
            set
            {
                switch (Type)
                {
                    case TrackingVariableType.Int:
                        ReducedIntValue = (int) value;
                        return;
                    case TrackingVariableType.Float:
                        ReducedFloatValue = value;
                        return;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public float GetValue(TrackingType trackingType)
        {
            return trackingType switch
            {
                TrackingType.Max => MaxValue,
                TrackingType.Current => CurrentValue,
                TrackingType.Added => AddValue,
                TrackingType.Reduced => ReducedValue,
                _ => throw new ArgumentOutOfRangeException(nameof(trackingType), trackingType, null)
            };
        }
    }
}