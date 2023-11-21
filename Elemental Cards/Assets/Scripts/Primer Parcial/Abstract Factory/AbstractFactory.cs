using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractFactory<T> where T : IProduct
{
    protected T product;

    public AbstractFactory(T productToProduce)
    {
        product = productToProduce;
    }

    public abstract T CreateProduct(int index);
}
