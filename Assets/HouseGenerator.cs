using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseGenerator : MonoBehaviour
{
    public enum ObjectProcedural
    {
        Cube,
        Pyramid
    }

    public GameObject proceduralObject;
    public Transform parent;

    public int jumlahLantai;
    public float lenght;
    public float width;
    public float height;
    public ObjectProcedural room;
    public ObjectProcedural roof;

    private void Start()
    {
        AddRoom(jumlahLantai, lenght, width, height, room, roof);
    }

    public void AddRoom(int lantai,float _lenght, float _width, float _height, ObjectProcedural _room, ObjectProcedural _roof)
    {
        for (int i = 1; i < lantai + 1; i++)
        {
            if (_room == ObjectProcedural.Cube)
            {
                var tempObject = Instantiate(proceduralObject, parent);
                var tempHeight = _height * i;
                tempObject.GetComponent<Generator>().CreateCube(_lenght,_width,tempHeight, tempHeight - _height);
            }else if (_room == ObjectProcedural.Pyramid)
            {
                var tempObject = Instantiate(proceduralObject, parent);
                var tempHeight = _height * i;
                tempObject.GetComponent<Generator>().CreatePyramid(_lenght, _width, tempHeight, tempHeight - _height);
            }
        }
        if (_roof == ObjectProcedural.Cube)
        {   
            var tempObject = Instantiate(proceduralObject, parent);
            var tempHeight = _height * lantai;
            tempObject.GetComponent<Generator>().CreateCube(_lenght,_width,tempHeight + _height, tempHeight);
        }else if (_roof == ObjectProcedural.Pyramid)
        {
            var tempObject = Instantiate(proceduralObject, parent);
            var tempHeight = _height * lantai;
            tempObject.GetComponent<Generator>().CreatePyramid(_lenght, _width, tempHeight + _height, tempHeight);
        }
    }
}
