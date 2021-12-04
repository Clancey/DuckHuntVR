using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextField : MonoBehaviour
{
    [Serializable]
    private struct ScoreFormat
    {
        public Color color;
        public string format;

        public void Apply(TextMesh text, int value)
        {
            text.color = color;
            text.text = string.Format(format, value);
        }
    }
    
    [SerializeField]
    private TextMesh _textField;

    [SerializeField]
    private ScoreFormat positiveScore;
    [SerializeField]
    private ScoreFormat negativeScore;
    
    private Vector3 _newPosition;
    private Quaternion _newRotation;

    public void SetValue(int value)
    {
        if (value >= 0)
        {
            positiveScore.Apply(_textField, value);
        } else
        {
            negativeScore.Apply(_textField, value);
        }

        _newRotation = Camera.main.transform.rotation;
        _newRotation.z = transform.rotation.z;

        transform.rotation = _newRotation;
    }

    private void Update()
    {
        _newPosition.y = Time.deltaTime;
        transform.position += _newPosition;
    }
}
