using System;

namespace VideoPlayer.State
{
    class ReccordingState : TimerState
    {
        public ReccordingState(PlayerStateController playerStateController, VideoSource videoSource, IFrameDisplay frameDisplay) : base(playerStateController, videoSource, frameDisplay)
        {
        }

        public override void DoAction()
        {
            var frame = _videoSource.NextFrame();

            _frameDisplay.UpdateFrame(frame);

            //TODO: Envoy� l'image au reccorder
        }

        public override void Forward()
        {
            _timer.Stop();
            ChangeState(_playerStateController.ForwardingState);
        }

        public override void Rewind()
        {
            _timer.Stop();
            ChangeState(_playerStateController.RewindingState);
        }

        public override void Pause()
        {
            _timer.Stop();
            ChangeState(_playerStateController.PauseReccordingState);
        }

        public override void Record(string _outputFile)
        {
            _timer.Stop();
            ChangeState(_playerStateController.PlayingState);
        }

        public override void Begin()
        {
            _timer.Resolution = 1;
            _timer.Period = (int)(1000 / _videoSource.FrameRate);
            base.Begin();
            _videoSource.Step = 1;
        }

        public override bool IsPlaying
        {
            get
            {
                return true;
            }
        }

        public override bool IsReccording
        {
            get
            {
                return true;
            }
        }
    }
}