import { PROJECT_ADD, PROJECT_TASK_ADD } from '../Actions/projects/projects'
import { projects } from '../Components/NormalizeProjects/NormalizeProjects'
import { NormalizeProjects } from '../Components/NormalizeProjects/NormalizeProjects'

const { projectsById, tasksById } = NormalizeProjects(projects)

const initialState = {
  projects: projectsById,
  tasks: tasksById
}

export const projectsReducer = (state = initialState, action) => {
  switch (action.type) {
    case PROJECT_ADD: {
      const projectId = Object.keys(state.projects).length + 1
      const newProject = {
        id: projectId,
        name: action.name,
        tasks: []
      }
      const newProjects = { ...state.projects }
      newProjects[projectId] = newProject
      return {
        ...state,
        projects: newProjects
      }
    }
    case PROJECT_TASK_ADD: {
      const projectId = action.projectId
      const taskId = action.taskId
      let newProjTasks = { ...state.projects }
      newProjTasks[projectId].tasks = [...newProjTasks[projectId].tasks, taskId]
      return {
        ...state,
        projects: newProjTasks
      }
    }
    default:
      return state;
  }
}
