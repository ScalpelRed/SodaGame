using Game.Util;

// AnimationController - контроллирует время анимации
// IAnimator - контроллирует установку значения
//      SetterAnimator - устанавливает значение через лямбда-функцию
//      FieldAnimator - устанавливает значение поля объекта
// ITimeScale - предоставляет зависящее от времени значение
//      FrameScale - рассчитывает значение по ключевым кадрам
//      InterpolationScale - рассчитывает значение по функции интерполяции
// IInterpolation - интерполяция (функция и параметры)


namespace Game.Animation
{
    public class AnimationController
    {
        public readonly ListenableList<IAnimator> Animators = [];


        private DateTime LastTimeUpdate = DateTime.Now;

        private float time = 0;
        public float Time
        {
            get
            {
                UpdateTime();
                return time;
            }
            set
            {
                LastTimeUpdate = DateTime.Now;
                time = value;
            }
        }

        public float Progress
        {
            get => (Time - Duration.Min) / (Duration.Max - Duration.Min);
            set
            {
                LastTimeUpdate = DateTime.Now;
                time = Duration.Min + (Duration.Max - Duration.Min) * value;
            }
        }

        private bool paused = true;
        public bool Paused
        {
            get => paused;
            set
            {
                UpdateTime();
                paused = value;
            }
        }

        private float speed = 1;
        public float Speed
        {
            get => speed;
            set
            {
                UpdateTime();
                speed = value;
            }
        }

        public Range<float> Duration = new(0, float.MaxValue);

        public Action<bool>? Finished;

        public AnimationController(Range<float> duration, params IAnimator[] animators) : this(animators)
        {
            Duration = duration;
        }

        public AnimationController(params IAnimator[] animators)
        {
            Animators.AddRange(animators);
        }

        public void Update()
        {
            if (!paused)
            {
                foreach (IAnimator animator in Animators) animator.Apply(Time);
            }
        }

        private void UpdateTime()
        {
            if (!paused && speed != 0)
            {
                time += (float)(DateTime.Now - LastTimeUpdate).TotalSeconds * speed;
                if (time > Duration)
                {
                    paused = true;
                    time = Duration.Max;
                    Finished?.Invoke(true);
                }
                else if (time < Duration)
                {
                    paused = true;
                    time = Duration.Min;
                    Finished?.Invoke(false);
                }
                LastTimeUpdate = DateTime.Now;
            }
        }
    }
}
