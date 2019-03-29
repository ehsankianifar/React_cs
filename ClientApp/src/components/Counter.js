import React, { Component } from 'react';

export class Counter extends Component {
  displayName = Counter.name

  constructor(props) {
    super(props);
      this.state = { currentCount: "Loading...", currentCount2: "Loading...", currentCount3: "Loading...", currentCount4: "Loading..."};
    this.incrementCounter = this.incrementCounter.bind(this);
    }


   

    incrementCounter() {
        fetch('api/TempData/GetLast')
            .then(response => response.json())
            .then(data => {
                this.setState({ currentCount: data.readingDateTime, currentCount2: data.assetName, currentCount3: data.deviceName, currentCount4: data.readingData });
            })
    }

 

    render() {
        setTimeout(this.incrementCounter, 2000);
    return (
      <div>
        <h1>Live Data</h1>

        <p>Here you can see live data.</p>

            <p>Date and time =  <strong>{this.state.currentCount}</strong></p>
            <p>Raspberry pi  =  <strong>{this.state.currentCount2}</strong></p>
            <p>Sensor Name =  <strong>{this.state.currentCount3}</strong></p>
            <p>Last reading =  <strong>{this.state.currentCount4}</strong></p>
            
      </div>
    );
  }
}
