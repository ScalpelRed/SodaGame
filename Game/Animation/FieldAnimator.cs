using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Game.Animation
{
    public struct FieldAnimator<TTarget, TValue> : IAnimator
    {
        public ITimeScale<TValue> ValueProvider;

        [NotNull]
        private readonly FieldInfo Field;

        [NotNull]
        private readonly TTarget Target;

        public FieldAnimator(string fieldName, TTarget target, ITimeScale<TValue> valueProvider)
        {
            FieldInfo? f = typeof(TTarget).GetField(fieldName)
                ?? throw new ArgumentException($"Type \"{typeof(TTarget).Name}\" has no field \"{fieldName}\"");

            if (!(f.IsPublic && f.IsInitOnly) || f.IsStatic)
                throw new ArgumentException($"Animated field must be public, modifiable and not static." +
                    $" (field \"{fieldName}\" in {typeof(TTarget).Name})");

            if (f.GetType() != typeof(TValue)) throw new ArgumentException($"Field and target types must be the same." +
                    $" (field \"{fieldName}\" in {typeof(TTarget).Name})");

            if (target is null) throw new ArgumentNullException(nameof(target));

            Field = f;
            Target = target;
            ValueProvider = valueProvider;
        }

        public readonly void Apply(float seconds)
        {
            Field.SetValue(Target, ValueProvider.GetValue(seconds));
        }
    }
}
