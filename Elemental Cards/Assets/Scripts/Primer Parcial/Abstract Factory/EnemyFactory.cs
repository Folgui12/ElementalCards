using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory: AbstractFactory<EnemyManager>
{
    public EnemyFactory(EnemyManager productToProduce) : base(productToProduce)
    {

    }

    public override EnemyManager CreateProduct(int index)
    {
        return (EnemyManager)product.Clone(index);
    }

}
