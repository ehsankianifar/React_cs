import React, { Component } from 'react';

export class Counter extends Component {
  displayName = Counter.name

  constructor(props) {
    super(props);
      this.state = { currentCount: "Loading...", currentCount2: "Loading..." };
    this.incrementCounter = this.incrementCounter.bind(this);
    }


   

    incrementCounter() {
        fetch('api/TempData/GetLast')
            .then(response => response.json())
            .then(data => {
                this.setState({ currentCount: data.temperature, currentCount2 : data.humidity });
            })
    }

 

    render() {
        setTimeout(this.incrementCounter, 2000);
    return (
      <div>
        <h1>Live Data</h1>

        <p>Here you can see live data.</p>

            <p>Temperature =  <strong>{this.state.currentCount}</strong></p>
            <p>Humidity =  <strong>{this.state.currentCount2}</strong></p>
            
      </div>
    );
  }
}
