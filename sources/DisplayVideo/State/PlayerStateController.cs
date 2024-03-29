﻿using System;

namespace VideoPlayer.State
{
    class PlayerStateController:IDisposable
    {
        private VideoSource _source;


        public event EventHandler CurrentStateChanged;

        public PlayerStateController(IFrameDisplay frameDisplay)
        {
            _source = new VideoSource();

            CurrentState = InitialState = new InitialState(this, _source, frameDisplay);
            StoppedState = new StoppedState(this, _source, frameDisplay);
            PlayingState = new PlayingState(this, _source, frameDisplay);
            PausedState = new PausedState(this, _source, frameDisplay);
            ReccordingState = new ReccordingState(this, _source, frameDisplay);
            PauseReccordingState = new PauseReccordingState(this, _source, frameDisplay);
            RewindingState = new RewindingState(this, _source, frameDisplay);
            ForwardingState = new ForwardingState(this, _source, frameDisplay);
        }

        public IPlayerState InitialState{ get;private set; }
        public IPlayerState StoppedState{ get;private set; }
        public IPlayerState PlayingState{ get;private set; }
        public IPlayerState PausedState{ get;private set; }
        public IPlayerState ReccordingState{ get;private set; }
        public IPlayerState PauseReccordingState{ get;private set; }
        public IPlayerState RewindingState{ get;private set; }
        public IPlayerState ForwardingState{ get;private set; }


        private IPlayerState _currentState;

        /// <summary>
        /// État courante du lecteur
        /// </summary>
        public IPlayerState CurrentState
        {
            get { return _currentState; }
            set
            {
                _currentState = value;
                InvokeCurrentStateChanged(EventArgs.Empty);
            }
        }

        private void InvokeCurrentStateChanged(EventArgs e)
        {
            EventHandler changed = CurrentStateChanged;
            if (changed != null) changed(this, e);
        }

        /// <summary>
        /// Contraste utilisé pour le traitement de l'image (-255 à 255)
        /// </summary>
        public double Contraste
        {
            get{ return Traitement.Instance.Contraste;}
            set { Traitement.Instance.Contraste = value; }
        }
        /// <summary>
        /// Brillance utilisé pour le traitement de l'image (0.5 à 2)
        /// </summary>
        public int Brillance
        {
            get{ return Traitement.Instance.Brillance;}
            set { Traitement.Instance.Brillance = value; }
        }
      
        /// <summary>
        /// Tableau d'entier représentant la matrice de convolution pour le traitement
        /// </summary>
        public int[] Convolution
        {
            get{ return Traitement.Instance.Convolution;}
            set { Traitement.Instance.Convolution = value; }
        }

        public void Open(string file)
        {
            if(CurrentState.FileOpen)
                CurrentState.Close();

            CurrentState.Open(file);
        }

        public void Close()
        {
            CurrentState.Close();
        }

        public void Play()
        {
            CurrentState.Play();
        }

        public void Pause()
        {
            CurrentState.Pause();
        }

        public void Stop()
        {
            CurrentState.Stop();
        }

        public void Forward()
        {
            CurrentState.Forward();
        }

        public void Rewind()
        {
            CurrentState.Rewind();
        }

        public void Record(string outputFile)
        {
            CurrentState.Record(outputFile);
        }

        public bool IsPlaying
        {
            get { return CurrentState.IsPlaying; }
        }

        public bool IsFastPlaying
        {
            get { return CurrentState.IsFastPlaying; }
        }

        public bool IsPaused
        {
            get{ return CurrentState.IsPaused;}
        }
        public bool IsReccording
        {
            get { return CurrentState.IsReccording; }
        }
        public bool FileOpen
        {
            get{ return CurrentState.FileOpen;}
        }

        public void Dispose()
        {
            if(FileOpen)
                CurrentState.Close();
            
            InitialState.Dispose();
            StoppedState.Dispose();
            PlayingState.Dispose();
            PausedState.Dispose();
            PauseReccordingState.Dispose();
            RewindingState.Dispose();
            ForwardingState.Dispose();
            ReccordingState.Dispose();
        }
    }
}