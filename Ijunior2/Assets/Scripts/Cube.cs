using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _ratioOfReductions;
    [SerializeField] private int _chanceNewGeneration;
    [SerializeField] private int _minCountChilds = 2;
    [SerializeField] private int _maxCountChilds = 7;
    [SerializeField] private Explosion _exception;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private int _increaseStrengthNewGeneration;

    public event System.Action<int, int> CubePressed;

    private int _minChance = 0;
    private int _maxChance = 100;

    private void OnMouseDown()
    {
        CreateNewGeneration();
        _exception.Explode();
        Destroy(gameObject);
    }

    public void SetStart(Vector3 scale, int chanceNewGeneration, Color color) 
    {
        transform.localScale = scale;
        _chanceNewGeneration = chanceNewGeneration;
        _renderer.material.color = color;
    }

    private void CreateNewGeneration()
    {
        if (_chanceNewGeneration >= Random.Range(_minChance, _maxChance))
        {
            int countOfChilds = Random.Range(_minCountChilds, _maxCountChilds);

            for (int i = 0; i < countOfChilds; i++)
            {
                GameObject child = Instantiate(gameObject, null);
                Cube cubeOfChild = child.GetComponent<Cube>();
                Explosion explosionOfChild = child.GetComponent<Explosion>();

                cubeOfChild.SetStart(transform.localScale / _ratioOfReductions, _chanceNewGeneration / 2, new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f)));
                explosionOfChild.SetStart(_exception.ExplosionForce * _increaseStrengthNewGeneration, _exception.ExplosionRadius * _increaseStrengthNewGeneration);
            }
        }
    }
}
