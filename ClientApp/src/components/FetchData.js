import React, { Component } from 'react';

export class FetchData extends Component {
    displayName = FetchData.name

    constructor(props) {
        super(props);
        this.state = { forecasts: [], loading: true };

        fetch('api/TempData/GettempDatas')
            .then(response => response.json())
            .then(data => {
                this.setState({ forecasts: data, loading: false });
            });
    }

    static renderForecastsTable(forecasts) {
        return (
            <table className='table'>
                <thead>
                    <tr>
                        <th>Id</th>
                        <th>Date and time</th>
                        <th>Raspberry pi</th>
                        <th>Sensor Name</th>
                        <th>Reading data</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.id}>
                            <td>{forecast.id}</td>
                            <td>{forecast.readingDateTime}</td>
                            <td>{forecast.assetName}</td>
                            <td>{forecast.deviceName}</td>
                            <td>{forecast.readingData}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderForecastsTable(this.state.forecasts);

        return (
            <div>
                <h1>Temperature and humidity data</h1>
                <p>This page shows you all the data in database.</p>
                {contents}
            </div>
        );
    }
}
