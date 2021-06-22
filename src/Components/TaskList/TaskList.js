import Task from '../Task/Task'
import TaskAdd from '../TaskAdd/TaskAdd'
import React from 'react';

const TaskList = (props) => {

  return (
    <div>
      {Object.values(props.tasksById).map(i => <Task key={i.id} name={i.name} description={i.description}
        completed={i.completed} onClick={() => props.changeCompletedStatus(i.id)} />)}
    </div>
  )
}

export default TaskList;