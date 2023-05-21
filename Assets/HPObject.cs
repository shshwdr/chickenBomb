using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


    public class HPObject : MonoBehaviour
    {
        public bool canBeKilled = true;
        public float rotateTime = 0.3f;
        public bool isDead;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        public virtual void kill()
        {
            if (!canBeKilled)
            {
                return;
            }
            if (!isDead)
            {
                isDead = true;
                transform.DORotate(new Vector3(0, 0, 180), rotateTime);
            }
        }
    }