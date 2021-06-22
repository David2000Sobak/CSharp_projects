export const TASK_ADD = 'TASK_ADD'
export const TASKADD_CLICK = 'TASKADD_CLICK'
export const CHANGE_STATUS = 'CHANGE_STATUS'

export const handleTaskAdd = (taskId, name, description, projectId) =>
    ({ 
  type: TASK_ADD,
  projectId: projectId,
  name: name,
  description: description
})

export const handleChangeCompletedStatus = (id, completed) =>
    ({
  type: CHANGE_STATUS,
  id: id,
  completed: completed
})