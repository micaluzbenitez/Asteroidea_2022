using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Walls
{
    public class Wall : MonoBehaviour
    {
        [Header("Wall data")]
        [SerializeField] private SpriteRenderer sprite = null;
        [SerializeField] private float maxPosition = 10.7f;
        [SerializeField] private float resetPosition = -10.7f;

        [Header("Assets")]
        [SerializeField] private Sprite[] commonWalls = null;
        [SerializeField] private Sprite[] lavaWalls = null;
        [SerializeField] private float lavaSpawnRate = 0.07f;

        private void Start()
        {
            RandomAsset();
        }

        private void Update()
        {
            VerticalMovement();

            if (transform.localPosition.y > maxPosition) Reset();
        }

        private void VerticalMovement()
        {
            float speed = Managers.GameManager.VerticalSpeed;
            Vector3 pos = transform.localPosition;
            pos.y += speed * Time.deltaTime;
            transform.localPosition = new Vector3(pos.x, pos.y, pos.z);
        }

        private void Reset()
        {
            transform.localPosition = new Vector3(transform.localPosition .x, resetPosition, transform.localPosition.z);
            RandomAsset();
        }

        private void RandomAsset()
        {
            bool lavaWall = Random.Range(0.0f, 1.0f) < lavaSpawnRate;

            if (lavaWall)
            {
                int index = Random.Range(0, lavaWalls.Length);
                sprite.sprite = lavaWalls[index];
            }
            else
            {
                int index = Random.Range(0, commonWalls.Length);
                sprite.sprite = commonWalls[index];
            }
        }
    }
}