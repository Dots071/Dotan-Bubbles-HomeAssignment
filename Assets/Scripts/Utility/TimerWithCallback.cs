
using System.Threading;
using System;
using Cysharp.Threading.Tasks;

namespace Game.Utility
{
    /// <summary>
    /// A timer service that ticks once per second.
    /// Notifies subscribers each second via OnTimeUpdate and invokes a provided callback when the timer expires.
    /// </summary>
    public class TimerWithCallback : IDisposable
    {

            private readonly int _durationInSeconds;
            private readonly Action<int> _onTimerTick;
            private CancellationTokenSource _cts;
            public bool IsRunning { get; private set; }

            public event Action OnTimeFinished;

            public TimerWithCallback(int durationInSeconds, Action<int> onTimerTick)
            {
                _durationInSeconds = durationInSeconds;
                _onTimerTick = onTimerTick;
            }

            public void StartTimer()
            {
                if (IsRunning)
                    return;

                IsRunning = true;
                _cts = new CancellationTokenSource();
                TimerLoop(_cts.Token).Forget();
            }

            public void StopTimer()
            {
                _cts?.Cancel();
                IsRunning = false;
            }

            private async UniTaskVoid TimerLoop(CancellationToken token)
            {
                int remainingTime = _durationInSeconds;
                while (remainingTime > 0 && !token.IsCancellationRequested)
                {
                    _onTimerTick?.Invoke(remainingTime);
                    await UniTask.Delay(TimeSpan.FromSeconds(1), cancellationToken: token);
                    remainingTime--;
                }

                if (!token.IsCancellationRequested)
                {
                    _onTimerTick?.Invoke(0);
                    OnTimeFinished?.Invoke();
                }

                IsRunning = false;
            }

            public void Dispose()
            {
                StopTimer();
                _cts?.Dispose();
            }
        }
}

