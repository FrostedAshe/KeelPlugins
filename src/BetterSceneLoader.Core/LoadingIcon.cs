﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BetterSceneLoader
{
    public class LoadingIcon : MonoBehaviour
    {
        public static void Init(Image image, float speed)
        {
            var rotator = image.gameObject.AddComponent<LoadingIcon>();
            rotator.image = image;
            rotator.speed = speed;
        }

        public static readonly Dictionary<string, bool> loadingState = new Dictionary<string, bool>();
        private bool rotate;
        private bool prevState;
        private Image image;
        private float speed;

        private void Start()
        {
            image.enabled = false;
            StartCoroutine(LoadingIndicator());
        }

        private IEnumerator LoadingIndicator()
        {
            while(true)
            {
                bool state = loadingState.Values.Contains(true);
                if(state != prevState)
                {
                    prevState = state;
                    rotate = state;

                    if(state)
                    {
                        rotate = true;
                        image.enabled = true;
                    }
                    else
                    {
                        rotate = false;
                        image.enabled = false;
                    }
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        private void Update()
        {
            if(rotate)
            {
                image.rectTransform.rotation *= Quaternion.Euler(0f, 0f, speed);
            }
        }
    }
}
