using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CandyMatch.Controllers
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager instance = null;

        [SerializeField] private List<AudioClip> soundClips;
        [SerializeField] private AudioSource audioSOurce;

        public enum SoundTypes
        {
            CARD_FLIP,
            CARD_MATCH,
            CARD_MISMATCH,
            GAMEOVER
        }

        public void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        public static void PlaySound(SoundTypes soundTypes)
        {
            AudioClip audioClip = instance.soundClips.Find(x => x.name.Equals(instance.GetAudioFileName(soundTypes)));

            if(audioClip != null)
            {
                instance.audioSOurce.PlayOneShot(audioClip);
            }
        }

        private string GetAudioFileName(SoundTypes soundTypes)
        {
            return soundTypes switch
            {
                SoundTypes.CARD_FLIP => "Card_Flip",
                SoundTypes.CARD_MATCH => "Card_Match",
                SoundTypes.CARD_MISMATCH => "Card_Mismatch",
                SoundTypes.GAMEOVER => "GameOver",
                _ => string.Empty,
            };
        }
    }
}

