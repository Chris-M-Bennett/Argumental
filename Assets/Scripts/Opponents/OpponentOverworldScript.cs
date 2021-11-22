﻿using System;
using System.Collections;
using UnityEngine;

namespace Opponents
{
    public class OpponentOverworldScript : MonoBehaviour
    {
        [Header("Mouse over field names for description of what to add")]
        [SerializeField, Tooltip("The prefab used for this opponent in debates")] public GameObject debatePrefab;
        private SpriteRenderer _mainRenderer;
        private Color _mainColour;
        private SpriteRenderer _otherRenderer;
        private Color _otherColour;
        private Vector2 _currentPosition;
        void Start()
        {
            _mainRenderer = GetComponent<SpriteRenderer>();
            _mainColour = _mainRenderer.color;
            _otherRenderer = GetComponentInChildren<SpriteRenderer>();
            _otherColour = _otherRenderer.color;
            int current = 0;
            StartCoroutine(ChangeColour(current));
        }

        private void FixedUpdate()
        {
            _currentPosition = transform.position;
            Collider2D hit = Physics2D.OverlapCircle(_currentPosition, 0.5f);
        }

        IEnumerator ChangeColour(int current)
        {
            if (_otherColour.a == 1)
            {
                _mainColour.a = 1;
                _otherColour.a = 0;
                _mainRenderer.sprite = _otherRenderer.sprite;
                current++;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                _otherColour.a += 0.1f;
                _otherRenderer.color = _otherColour;
            }
        }
    }
}
