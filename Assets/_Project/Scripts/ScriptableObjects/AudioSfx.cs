using UnityEngine;

namespace APROMASTER
{
    [CreateAssetMenu(fileName = "AudioSfx", menuName = "ScriptableObjects/CreateAudioSfx")]
    public class AudioSfx : ScriptableObject
    {
        [System.Serializable]
        public struct AudioParametersStruct
        {
            public AudioClip[] AudioClips;
            public float Volume;
            public float Pitch;
            public bool Loop;
            public float StartDelay;
        }
        public AudioParametersStruct AudioParameters;
        private AudioSource _audioSource;
        GameObject _sourceObject;

        public void PlayAudio()
        {
            if (_sourceObject == null)
            {
                _sourceObject = new GameObject(this.name);
                _audioSource = _sourceObject.AddComponent<AudioSource>();
            }
            _audioSource.clip = AudioParameters.AudioClips[Random.Range(0, AudioParameters.AudioClips.Length)];
            _audioSource.volume = AudioParameters.Volume;
            _audioSource.pitch = AudioParameters.Pitch;
            _audioSource.loop = AudioParameters.Loop;
            _audioSource.PlayDelayed(AudioParameters.StartDelay);
        }

        public void StopAudio()
        {
            if (_audioSource.isPlaying)
                _audioSource.Stop();
        }
    }
}
