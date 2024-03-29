﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class ParallaxLayer : MonoBehaviour {
	[Range(-10f, 1f)]
	public float ParallaxSpeed = 1f;
	public Transform BaseObject;

	private Transform _transform;
	private Vector3 _offset;
	private Vector3 _newPosition;
	private Vector3 _roundedPosition;

	private void Awake() {
		BaseObject = Camera.main.transform;
		_transform = GetComponent<Transform> ();
		_newPosition = _transform.position;
		_offset = BaseObject.position - _newPosition;
	}

	private void Update() {
		_newPosition.x = _offset.x + BaseObject.position.x * ParallaxSpeed;
		_roundedPosition = _newPosition;
		_roundedPosition.x = _newPosition.x;
		//_roundedPosition.x = Mathf.Floor (_newPosition.x * 32f) / 32f;
		_transform.position = _roundedPosition;
	}
}
