import Task from '../Task/Task'
import TaskAdd from '../TaskAdd/TaskAdd'
import React from 'react';

class TaskList extends React.Component {
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

    handleClick = (id, completed) => {
        this.setState(currentState => {
            let index = [...currentState.tasks].findIndex(e => e.id === id)
            currentState.tasks[index] = { ...currentState.tasks[index], completed: !completed }
            return {
                tasks: currentState.tasks
            }
        })
    }

    addNewTask = (name, description) => {
        this.setState((currentState) => {
            const newTask = {
                id: currentState.tasks.length + 1,
                name: name,
                description: description,
                completed: false
            }
            const newTasks = [newTask, ...currentState.tasks]
            console.log(newTasks)
            return {
                tasks: newTasks,
            }
        })
    }

    render() {
        return (
            <div>
                <TaskAdd addNewTask={this.addNewTask} />
                <div>
                    {this.state.tasks.map(i => <Task key={i.id} name={i.name} description={i.description}
                        completed={i.completed} onClick={() => this.handleClick(i.id, i.completed)} />)}
                </div>
            </div>
        )
    }
}

export default TaskList;