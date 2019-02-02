using System.Collections.Generic;
using UnityEngine;

namespace CyberInfection.Extension
{
    public class Graph
    {
        protected List<List<int>> _matrix;

        private int _size;

        public Graph(int roomsCount)
        {
            _size = roomsCount;
            InitializeMatrix();
        }

        private void InitializeMatrix()
        {
            _matrix = new List<List<int>>(_size);
            for (var i = 0; i < _size; i++)
            {
                _matrix.Add(new List<int>(_size));
            }
        }

        public void Generate()
        {
            for (var i = 0; i < _size; i++)
            {
                var connectionsCount = Random.Range(1, 3);

                for (var j = 0; j < connectionsCount; j++)
                {
                    var r = Random.Range(0, _size);
                    while (r == i || _matrix[i].Contains(r))
                    {
                        r = Random.Range(0, _size);
                    }
                
                    _matrix[i].Add(r);
                }
            }

            for (var i = 0; i < _size; i++)
            {
                var matrixString = $"{i}: ";
                foreach(var connection in _matrix[i])
                {
                    matrixString += $"{connection} ";
                }
                Debug.Log(matrixString);
            }
        }
    }
}