import Navbar from './Navbar';
import Home from './Home';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import OrderHistory from './OrderHistory';

function App() {
  return (
    <Router>
      <div className="App">
        <Navbar/>
        <div className="content">
          <Switch>
            <Route exact path="/">
              <Home/>
            </Route>
            <Route path="/orders">
              <OrderHistory/>
            </Route>
          </Switch>
        </div>
      </div>
    </Router>
  );
}

export default App;
