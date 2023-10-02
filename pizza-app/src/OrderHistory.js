import useFetch from "./useFetch";
import { useState } from "react";

const OrderHistory = () => {
    const { data: pizzaSavedOrders, isPending, error } = useFetch('https://localhost:7261/api/pizza-orders');
    const [isClicked, setIsClicked] = useState(false);

    return ( 
        <div className="home">
            { error && <div>{ error }</div>}
            { isPending  && <div>Loading...</div> }
            {pizzaSavedOrders && pizzaSavedOrders.map((pizza) => (
                <div className="pizza-preview" key={pizza.id}>
                    <h2>Order: { pizza.size } pizza</h2>
                    <p><strong>Size: </strong>{ pizza.size }</p>
                    <p><strong>Total cost: </strong>{ pizza.totalPrice }€</p>
                    <p><strong>Toppings:</strong></p>
                    {pizza.toppings.map((topping) => (
                        <div className="topping-preview" key={topping.id}>
                            <p>{ topping.name }</p>
                        </div>
                    ))}
                </div>
            ))}
        </div>
     );
}
 
export default OrderHistory;