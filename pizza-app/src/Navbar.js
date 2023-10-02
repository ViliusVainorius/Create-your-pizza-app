import { Link } from 'react-router-dom';

const Navbar = () => {
    return ( 
        <nav className="navbar">
            <h1>Pizza App</h1>
            <div className="links">
                <Link to="/">Home</Link>
                <Link to="/orders">Pizza orders</Link>
            </div>
        </nav>
     );
}
 
export default Navbar;