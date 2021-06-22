import styles from './styles.css';
import React from 'react';


const Task = (props) => {
    const handleClick = () => {
        return (
            console.log('Task ' + props.id + ' completed status: ' + props.completed)
        )
    }

    return (
        <div className={styles.tasks}>
            <div key={props.name} className={styles.name}>{props.name}</div>
            <div key={props.description} className={styles.description}>{props.description}</div>
            <div key={props.completed} className={styles.completed}>Completed: {String(props.completed)}</div>
            <button onClick={handleClick} className={styles.button}>DONE</button>
        </div>
    )
}

class MyTodoList extends React.Component {
    state = {
        tasks: [
            { id: 1, name: 'Task 1', description: 'Description 1', completed: true },
            { id: 2, name: 'Task 2', description: 'Description 2', completed: true },
            { id: 3, name: 'Task 3', description: 'Description 3', completed: false },
            { id: 4, name: 'Task 4', description: 'Description 4', completed: true },
            { id: 5, name: 'Task 5', description: 'Description 5', completed: true },
            { id: 6, name: 'Task 6', description: 'Description 6', completed: false }
        ]
    }

    render() {
        return (
            <div>
                {this.state.tasks.map(i => <Task id={i.id} name={i.name} description={i.description}
                    completed={i.completed} />)}
            </div>
        )
    }
}

const App = () => {
    return (
        <div>
            <MyTodoList />
        </div>
    )
}

export default App;