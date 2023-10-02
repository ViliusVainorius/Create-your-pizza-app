import { useEffect, useState } from "react";
import PizzaPick from "./PizzaPick";
import useFetch from "./useFetch";

const Home = () => {
    
    const { data: pizzaSizes, isPending, error } = useFetch('https://localhost:7261/api/sizes');
    const { data: pizzaToppings, isPending: toppingsIsPending, error: toppingsIsError } = useFetch('https://localhost:7261/api/toppings');
    

    return ( 
        <div className="home">
            { error && <div>{ error }</div>}
            { toppingsIsError && <div>{ toppingsIsError }</div>}
            { isPending  && toppingsIsPending && <div>Loading...</div> }
            { pizzaSizes && pizzaToppings && <PizzaPick pizzaSizes={pizzaSizes} pizzaToppings={pizzaToppings}/>}
        </div>
     );
}
 
export default Home;