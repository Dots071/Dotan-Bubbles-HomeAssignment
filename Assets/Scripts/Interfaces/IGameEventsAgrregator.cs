
using System;

namespace Game.Interfaces
{
    public interface IGameEventsAgrregator
    {
        event Action<IClickableBall> BallSpawned;

        event Action PlayerMissed;

        void RaiseBallSpawned(IClickableBall ball);


        void RaisePlayerMissed();
    }
}