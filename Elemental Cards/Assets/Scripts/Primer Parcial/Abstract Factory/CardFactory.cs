using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFactory : AbstractFactory<Card>
{
    public CardFactory(Card productToProduce): base (productToProduce)
    {

    }

    public override Card CreateProduct(int index)
    {         
        return (Card)product.Clone(index);
    }
}
