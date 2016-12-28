// Author: Joe Bjorkman
// File: GameController.cs
// Last Updated: 8/15/2016

using UnityEngine;
using System.Collections;

namespace Assets.Scripts
{
    class PlayerMovementNew : MonoBehaviour
    {
        public Camera cam;
        public float xOff;
        public float yOff;
        private Rigidbody2D rb2d;
        private Renderer rnd;
        private float maxHeight;
        private float maxWidth;
        private float startX; // used for hard mode
        private bool hardMode = false; // hard mode disables the user's ability to go forward and back
        private bool enablePlayerMov;

        void Start()
        {
            enablePlayerMov = false; // disable movement at start
            if(cam == null)
            {
                cam = Camera.main;
            }
            rb2d = GetComponent<Rigidbody2D>();
            rnd = GetComponent<Renderer>();
            Vector3 upperCorner = new Vector3(Screen.width, Screen.height);
            Vector3 targetSize = cam.ScreenToWorldPoint(upperCorner);
            float shipHeight = rnd.bounds.extents.y;
            float shipWidth = rnd.bounds.extents.x;
            maxHeight = targetSize.y - shipHeight/2;
            maxWidth = targetSize.x - shipWidth/2;
            startX = -targetSize.x * 4 / 5;
        }

        void FixedUpdate()
        {
            if (enablePlayerMov)
            {
                Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
                Vector3 targetPos;
                if (hardMode)
                {
                    targetPos = new Vector3(startX+xOff, pos.y+yOff);
                }
                else
                {
                    targetPos = new Vector3(pos.x+xOff, pos.y+yOff);
                }

                float targHeight = Mathf.Clamp(targetPos.y, -maxHeight, maxHeight);
                float targWidth = Mathf.Clamp(targetPos.x, -maxWidth, maxWidth);
                targetPos = new Vector3(targWidth, targHeight, targetPos.z);
                rb2d.MovePosition(targetPos);
            }
        }

        public void HardModeOn()
        {
            hardMode = true;
        }

        public void PMovOff()
        {
            enablePlayerMov = false;
        }

        public void PMoveOn()
        {
            enablePlayerMov = true;
        }
    }
}
