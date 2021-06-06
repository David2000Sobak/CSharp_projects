export const projects = [
    {
        id: 1, name: 'Domesticity', tasks: [
            { id: 1, name: 'Task 1', description: 'Description 1', completed: true },
            { id: 2, name: 'Task 2', description: 'Description 2', completed: true },
            { id: 3, name: 'Task 3', description: 'Description 3', completed: false }
        ]
    },
    {
        id: 2, name: 'Uni', tasks: [
            { id: 4, name: 'Task 4', description: 'Description 4', completed: true },
            { id: 5, name: 'Task 5', description: 'Description 5', completed: true },
            { id: 6, name: 'Task 6', description: 'Description 6', completed: false }
        ]
    }
]

export const NormalizeProjects = () => {
    const normalizeBy = key => {
        return (data, item) => {
            data[item[key]] = item
            return data
        }
    }
    const normalizedTasks = projects
        .map(project => project.tasks)
        .flat()
        .reduce(normalizeBy("id"), {})

    const normalizedProjects = projects
        .map(project => ({
            ...project,
            tasks: project.tasks.map(task => task.id),
        }))
        .reduce(normalizeBy("id"), {})

    const normalizedState = {
        projectsById: normalizedProjects,
        tasksById: normalizedTasks
    }

    return normalizedState
}
