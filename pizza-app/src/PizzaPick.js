import { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";

const PizzaPick = ({pizzaSizes, pizzaToppings}) => {
    const [size, setSize] = useState(pizzaSizes[0].name);
    const [toppings, setToppings] = useState([]);
    const [isPending, setIsPending] = useState(false);
    const [totalPrice, setTotalPrice] = useState(0.0);
    const history = useHistory();


    const handleSubmit = (e) => {
        e.preventDefault();
        setIsPending(true);

        // Creating a Topping object for correct JSON stringify
        const toppingsObject = toppings.map((toppingName) => (
            {
                name: toppingName
            }
        ));

        //Creating a Pizza object
        const pizza = {
            size: size,
            toppings: toppingsObject
        };

        //Posting the pizza object 
        fetch('https://localhost:7261/api/pizzas', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(pizza)
        }).then(res => {
            if(!res.ok){
                throw Error('Could not post the pizza');
            }
            return res.json();
        })
        .then((data) => {
            //console.log(data.totalPrice);
            setTotalPrice(data.totalPrice);
            setIsPending(false);
        })
         .catch((error) => {
            console.error('Error:', error);
         })
    }

    const handleOnChange = (e) => {
        const toppingName = e.target.value;

        if(e.target.checked){
            setToppings([...toppings, toppingName]);
        } else{
            setToppings(toppings.filter((name) => name !== toppingName));
        }
    }

    const handleOnClick = (e) => {
        e.preventDefault();

        // Creating a Topping object for correct JSON stringify
        const toppingsObject = toppings.map((toppingName) => (
            {
                name: toppingName
            }
        ));

        //Creating a Pizza object
        const pizza = {
            size: size,
            toppings: toppingsObject
        };

        //Posting the pizza object 
        fetch('https://localhost:7261/api/pizza-orders', {
            method: 'POST',
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(pizza)
        }).then(res => {
            if(!res.ok){
                throw Error('Could not post the pizza');
            }
            return res.json();
        })
        .then((data) => {
            //history.go(-1);
            history.push('/orders');
            //console.log(data.totalPrice);
            setTotalPrice(data.totalPrice);
            setIsPending(false);
        })
         .catch((error) => {
            console.error('Error:', error);
         })
        
    }

    return ( 
        <div className="create">
            <h2>Create your Pizza!</h2>
            <form onSubmit={handleSubmit}>
                <label> Select pizza size:</label>
                <select 
                    value= {size}
                    onChange={(e) => setSize(e.target.value)}>
                    
                    {pizzaSizes.length > 0 && (
                        pizzaSizes.map((pizzaSize) =>(
                            <option value={pizzaSize.name}>{pizzaSize.name}</option>
                        ))
                    )}
                    
                </select>
                <label> Choose Pizza toppings:</label>
                <ul>
                    {pizzaToppings.length > 0 && (
                        pizzaToppings.map((pizzaTopping) =>(
                        <li key={pizzaTopping.id}>
                            <label>
                                <input 
                                    type="checkbox"
                                    value={pizzaTopping.name}
                                    checked={toppings.includes(pizzaTopping.name)}
                                    onChange={handleOnChange} 
                                />
                                {pizzaTopping.name}
                            </label>
                        </li>
                    )))}
                </ul>
                {!isPending && <button>Create pizza order</button>}
                {isPending && <button disabled>Calculating Pizza price...</button>}
                <p>Total cost of the order is:</p>
                {totalPrice !== 0 && <p>{totalPrice}€</p>}
                {!isPending && totalPrice !== 0 && <p>Do you wish to save this pizza order?</p>}
                {!isPending && totalPrice !== 0 && <button onClick={handleOnClick}>Save pizza order</button>}
                
                {isPending && <button disabled>Save pizza order</button>}
            </form>
        </div>
     );
}
 
export default PizzaPick;