using System;
using Plugins.Zenject.OptionalExtras.Scripts.Util;
using Plugins.Zenject.OptionalExtras.Signals.Main;
using UnityEngine;
using Zenject;

namespace Plugins.Zenject.OptionalExtras.Scripts.Misc
{
    public class AudioHandler : IInitializable, IDisposable
    {
        readonly SignalBus _signalBus;
        readonly Settings _settings;
        readonly AudioSource _audioSource;

        public AudioHandler(
            AudioSource audioSource,
            Settings settings,
            SignalBus signalBus)
        {
            _signalBus = signalBus;
            _settings = settings;
            _audioSource = audioSource;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<ShipCrashedSignal>(OnShipCrashed);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<ShipCrashedSignal>(OnShipCrashed);
        }

        void OnShipCrashed()
        {
            _audioSource.PlayOneShot(_settings.CrashSound);
        }

        [Serializable]
        public class Settings
        {
            public AudioClip CrashSound;
        }
    }
}
